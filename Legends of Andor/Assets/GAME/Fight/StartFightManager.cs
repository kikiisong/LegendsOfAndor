using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartFightManager : MonoBehaviourPun, TurnManager.IOnMove
{
    // Start is called before the first frame update
    [Header("Scenes")]
    [SceneName]
    public string nextScene;


    [Header("UI")]
    public GameObject ready;
    public GameObject fight;
    public GameObject start;

    public Region storedRegion;

    private bool isFight;

    public void Start()
    {
        fight = GameObject.Find("FightButton");
        fight.SetActive(false);
        ready = GameObject.Find("JoinFight");
        ready.SetActive(false);
        start = GameObject.Find("StartButton");
        start.SetActive(false);

        TurnManager.Register(this);
    }

    public void OnMove(Player player, Region currentRegion)
    {
        fight.SetActive(false);
        if (PhotonNetwork.LocalPlayer == player)
        {

            Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];//photonView.Owner is the Sce
            
            List<MonsterMoveController> MonsterOnMap = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(currentRegion);

            if (MonsterOnMap.Count > 0)
            {

                if (hero.data.numHours < 10)
                {
                    fight.SetActive(true);

                    print("Invite other to join in ");

                    fight.GetComponent<Button>().onClick.RemoveAllListeners();
                    fight.GetComponent<Button>().onClick.AddListener(() =>

                    {

                        photonView.RPC("changeMonsterTofight", RpcTarget.All, MonsterOnMap);

                        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
                        {
                            { K.Player.isFight, true }
                        });


                        isFight = true;
                        //LightUpJoin();
                        photonView.RPC("LightUpJoin", RpcTarget.Others);
                        start.SetActive(true);


                    });

                }
                else
                {
                    fight.SetActive(false);
                }
            }
            else if (hero.type == Hero.Type.ARCHER || hero.data.bow > 0)
            {
                List<Region> AdjacentRegions = GameGraph.Instance.AdjacentRegions(currentRegion);

                List<MonsterMoveController> choicesOfJoin = new List<MonsterMoveController>();

                foreach (Region r in AdjacentRegions)
                {
                    List<MonsterMoveController> MonsterOnAdjacent = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(r);
                    Debug.Log(MonsterOnAdjacent.Count);
                    if (MonsterOnAdjacent.Count > 0)
                    {
                        choicesOfJoin.Add(MonsterOnAdjacent[0]);
                    }

                }

                //if (choicesOfJoin.Count > 1)
                //{
                //TODO: have to choose one mosnter
                //}
                 if (choicesOfJoin.Count >= 1) {

                    if (hero.data.numHours < 10)
                    {
                        fight.SetActive(true);

                        print("Invite other to join in ");

                        fight.GetComponent<Button>().onClick.RemoveAllListeners();
                        fight.GetComponent<Button>().onClick.AddListener(() =>

                        {

                            photonView.RPC("changeMonsterTofight", RpcTarget.All, choicesOfJoin);
                            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
                        {
                            { K.Player.isFight, true }
                        });


                            isFight = true;
                            //LightUpJoin();
                            photonView.RPC("LightUpJoin", RpcTarget.Others);
                            start.SetActive(true);


                        });

                    }
                }
            }
        }

    }
    [PunRPC]
    public void changeMonsterTofight(List<MonsterMoveController> ms) {
        MonsterMoveController monster = ms[0];
        monster.m.isFighted = true;
    }


    public void Click_Ready()
    {

        print("join the fight");
        isFight = true;
        Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        List<Monster> MonsterOnMap = GameGraph.Instance.FindObjectsOnRegion<Monster>(hero.data.regionNumber);

        if (MonsterOnMap.Count > 0)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
                        {
                            { K.Player.isFight, true }
                        });

        }
        else if (hero.type == Hero.Type.ARCHER || hero.data.bow > 0)
        {

            Region test = GameGraph.Instance.Find(hero.data.regionNumber);
            Debug.Log(test);
            List<Region> AdjacentRegions = GameGraph.Instance.AdjacentRegions(test);

            foreach (Region r in AdjacentRegions)
            {
                List<MonsterMoveController> MonsterOnAdjacent = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(r);
                Debug.Log(MonsterOnAdjacent.Count);
                if (MonsterOnAdjacent.Count > 0)
                {
                    PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
                        {
                            { K.Player.isFight, true }
                        });
                    break;
                }
            }

        }
    }


    public void Click_Start()
    {
        print("switchScene");

        
        photonView.RPC("SwitchScene", RpcTarget.All);
    }

    [PunRPC]
    public void LightUpJoin() {
        print("lishgtUpJoin");
        fight.SetActive(false);
        ready.SetActive(true);
    }

    [PunRPC]
    public void SwitchScene()
    {
        print("Load for" + PhotonNetwork.LocalPlayer.NickName);
        if(isFight){
            fight.SetActive(false);
            ready.SetActive(false);
            start.SetActive(false);
            if (PhotonNetwork.IsConnected)
                //PhotonNetwork.LoadLevel(nextScene);
                SceneManager.LoadSceneAsync(5, LoadSceneMode.Additive);
        }

    }

}
