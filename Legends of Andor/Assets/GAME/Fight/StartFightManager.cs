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
        if (PhotonNetwork.LocalPlayer == player)
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];//photonView.Owner is the Sce
            
            List<Monster> MonsterOnMap = GameGraph.Instance.FindObjectsOnRegion<Monster>(currentRegion);

            if (MonsterOnMap.Count > 0)
            {

                if (hero.data.numHours < 7)
                {
                    fight.SetActive(true);
                    
                    print("Invite other to join in ");

                    fight.GetComponent<Button>().onClick.RemoveAllListeners();
                    fight.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
                        {
                            { K.Player.isFight, true }
                        });
                       
                        print("Am I here? ");
                        isFight = true;
                        //LightUpJoin();
                        photonView.RPC("LightUpJoin", RpcTarget.All);
                        start.SetActive(true);


                    });
                }
                else
                {
                    fight.SetActive(false);
                }
            
   			}
   		}
    }


    public void Click_Ready()
    {

        print("join the fight");
        Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        List<Monster> MonsterOnMap = GameGraph.Instance.FindObjectsOnRegion<Monster>(hero.data.regionNumber);

        if (MonsterOnMap.Count > 0)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
                        {
                            { K.Player.isFight, true }
                        });

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
        if(isFight){
            if (PhotonNetwork.IsConnected)
                //PhotonNetwork.LoadLevel(nextScene);
                SceneManager.LoadSceneAsync(5, LoadSceneMode.Additive);
        }
    }

}
