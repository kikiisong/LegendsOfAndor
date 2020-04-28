using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartFightManager : MonoBehaviourPun,TurnManager.IOnMove
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
    public static StartFightManager Instance;
    private bool isFight;
    private float update;
    public bool fightStart = false;
    public void Start()
    {
        fight.SetActive(false);
        ready.SetActive(false);
        start.SetActive(false);
        Instance = this;
        TurnManager.Register(this);
    }

    public void Update()
    {
        Player player = PhotonNetwork.LocalPlayer;
        Region currentRegion = player.GetCurrentRegion();
        update += Time.deltaTime;
        if (update > 2.0f && !fightStart)
        {
            update = 0.0f;
            Debug.Log("Update");
            if (!player.HasConsumedHour()&&TurnManager.IsMyTurn()) {
                
                displayFight(player, currentRegion);
            }
        }
    }

    public void OnMove(Player player, Region currentRegion)
    {
        fight.SetActive(false);
    }

    public void displayFight(Player player, Region currentRegion) {
        

        if (PhotonNetwork.LocalPlayer == player)
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();//photonView.Owner is the Sce

            List<MonsterMoveController> MonsterOnMap = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(currentRegion);

            if (MonsterOnMap.Count > 0)
            {

                if (hero.data.NumHours < 10)
                {
                    fight.SetActive(true);


                    print("Invite other to join in ");

                    fight.GetComponent<Button>().onClick.RemoveAllListeners();
                    fight.GetComponent<Button>().onClick.AddListener(() =>

                    {
                        Region MonsterRegion = MonsterOnMap[0].CurrentRegion;
                        print("Monster lable" + MonsterRegion.label);
                        List<Region> AdjacentRegions = GameGraph.Instance.AdjacentRegions(MonsterRegion);
                        AdjacentRegions.Add(MonsterRegion);
                        foreach (Region r in AdjacentRegions){
                            
                            if (Prince.Instance != null && Prince.Instance.CurrentRegion.label == r.label)
                            {
                                Prince.Instance.inFight = true;
                            }
                        }
                        
                        
                        //photonView.RPC("changeMonsterTofight", RpcTarget.All,hero.data.regionNumber);
                        MonsterMoveController monster = MonsterOnMap[0];
                        monster.isFighted = true;
                        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
                        {
                            { P.K.isFight, true }
                        });
                        print(PhotonNetwork.LocalPlayer.NickName + " start a fight ");

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
                

                if (choicesOfJoin.Count >= 1)
                {
                    if (choicesOfJoin.Count > 1) {
                        Debug.Log(choicesOfJoin[0]);
                    }
                    Region MonsterRegion = choicesOfJoin[0].CurrentRegion;
                    print("Monster lable" + MonsterRegion.label);
                    List<Region> AdjacentMonsterRegions = GameGraph.Instance.AdjacentRegions(MonsterRegion);
                    AdjacentMonsterRegions.Add(MonsterRegion);
                    foreach (Region r in AdjacentMonsterRegions)
                    {
                        if (Prince.Instance != null && Prince.Instance.CurrentRegion.label == r.label)
                        {
                            Prince.Instance.inFight = true;
                        }
                    }
                    if (hero.data.NumHours < 10)
                    {
                        fight.SetActive(true);

                        print("Invite other to join in ");

                        fight.GetComponent<Button>().onClick.RemoveAllListeners();
                        fight.GetComponent<Button>().onClick.AddListener(() =>

                        {
                            MonsterMoveController monster = choicesOfJoin[0];
                            monster.isFighted = true;
                            //photonView.RPC("changeMonsterTofight", RpcTarget.All,choicesOfJoin[0].regionlabel);
                            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
                        {
                        {P.K.isFight, true }
                        });
                            print(PhotonNetwork.LocalPlayer.NickName + " join in a fight ");

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
    public void changeMonsterTofight(int regionl)
    {
        MonsterMoveController monster = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(regionl)[0];
        monster.isFighted = true;
    }


    public void Click_Ready()
    {
        print("Am I able to join?");
        
        var p = PhotonNetwork.LocalPlayer;
        var hero = p.GetHero();
        print(hero.GetCurrentRegion().label);
        List<MonsterMoveController> MonsterOnMap = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(hero.GetCurrentRegion().label);

        if (MonsterOnMap.Count > 0)
        {
            isFight = true;
            print("join the fight");
            MonsterMoveController monster = MonsterOnMap[0];
            monster.isFighted = true;
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
                        {
                            { P.K.isFight, true }
                        });
            print(PhotonNetwork.LocalPlayer.NickName + " join in a fight ");

        }
        else if (hero.type == Hero.Type.ARCHER || hero.data.bow > 0)
        {
            isFight = true;
            Region test = p.GetCurrentRegion();
            Debug.Log(test);
            List<Region> AdjacentRegions = GameGraph.Instance.AdjacentRegions(test);

            foreach (Region r in AdjacentRegions)
            {
                print("join the fight");
                List<MonsterMoveController> MonsterOnAdjacent = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(r);
                Debug.Log(MonsterOnAdjacent.Count);
                if (MonsterOnAdjacent.Count > 0)
                {
                    MonsterMoveController monster = MonsterOnAdjacent[0];
                    monster.isFighted = true;
                    PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
                        {
                            { P.K.isFight, true }
                        });
                    print(PhotonNetwork.LocalPlayer.NickName + " join in a fight ");
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
        fightStart = true;
        print("Load for" + PhotonNetwork.LocalPlayer.NickName);
        if(isFight){
            fight.SetActive(false);
            ready.SetActive(false);
            start.SetActive(false);
            if (PhotonNetwork.IsConnected)
                //PhotonNetwork.LoadLevel(nextScene);
                SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Additive);
        }

    }

}
