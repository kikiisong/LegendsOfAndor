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
                        
                        Debug.Log("???");
                        ready.GetComponent<Button>().onClick.RemoveAllListeners();
                        ready.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            start.SetActive(false);
                            Debug.Log(photonView.IsMine);
                            if (photonView.IsMine)
                            {
                                SceneManager.LoadScene(nextScene);
                            }
                            else {
                                print("CO-OP");
                            }
                        });
                    });

                }
                else
                {
                    start.SetActive(false);
                }
            }
        }

    }


    private bool EveryoneAsked()
    {
        foreach (KeyValuePair<int, Player> pair in PhotonNetwork.CurrentRoom.Players)
        {
            Player player = pair.Value;
            if (player.CustomProperties.ContainsKey(K.Player.isAsked))
            {
                bool ready = (bool)player.CustomProperties[K.Player.isAsked];
                if (!ready)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
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


    public void Click_Start()
    {
        PhotonNetwork.LoadLevel(nextScene);
    }

    public void OnJoinClick()
    {
        Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        if (!isFight)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
            {
                { K.Player.isFight, true }
            });
            isFight = true;
        }
    }
}
