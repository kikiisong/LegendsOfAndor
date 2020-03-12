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
    public GameObject start;

    private bool isFight;

    public void Start()
    {
        start = GameObject.Find("FightButton");
        start.SetActive(false);
        ready = GameObject.Find("JoinFight");
        //ready.SetActive(false);
        TurnManager.Register(this);
    }
    void Update()
    {
        //if (PhotonNetwork.LocalPlayer.IsMasterClient)
        //{
        //    start.gameObject.SetActive(true);
        //}
        //else
        //{
        //    ready.gameObject.SetActive(false);
        //}
    }
    public void OnMove(Player player, Region currentRegion)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];//photonView.Owner is the Sce
            //int current = hero.data.regionNumber;
            List<Monster> MonsterOnMap = GameGraph.Instance.FindObjectsOnRegion<Monster>(currentRegion);

            if (MonsterOnMap.Count > 0)
            {

                if (hero.data.numHours < 7)
                {
                    start.SetActive(true);
                    print("Invite other to join in ");

                    start.GetComponent<Button>().onClick.RemoveAllListeners();
                    start.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        
                        ready.SetActive(true);
                        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
                        {
                            { K.Player.isFight, true },
                            { K.Player.isAsked, true }
                        });

                        start.SetActive(false);
                        isFight = true;
                        //Debug.Log("???");
                        //ready.GetComponent<Button>().onClick.RemoveAllListeners();
                        //ready.GetComponent<Button>().onClick.AddListener(() =>
                        //{
                        //    start.SetActive(false);
                        //    SceneManager.LoadSceneAsync(nextScene);

                        //});

                });
                }
                else
                {
                    start.SetActive(false);
                }
            
   			}
   		}
    }


    public void Click_Ready()
    {

        //TODO:check same region

            print("join the fight");
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
            {
                { K.Player.isFight, true }
            });
            isFight = true;
        
    }

    [PunRPC]
    public void Click_Start()
    {
        if(isFight){
            if (PhotonNetwork.IsConnected)
                //PhotonNetwork.LoadLevel(nextScene);
                SceneManager.LoadSceneAsync(5, LoadSceneMode.Additive);
        }
    }

}
