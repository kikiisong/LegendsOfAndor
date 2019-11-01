using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobbyManager : MonoBehaviourPunCallbacks
{
    [SceneName]
    public string nextScene;
    [SceneName]
    public string previousScene;

    public Button startGame;
    public Button ready;

    [Header("Resources")]
    public GameObject prefab;

    private void Start()
    {        
        GameObject select = PhotonNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        //Use RPC later
        if (PhotonNetwork.LocalPlayer.IsMasterClient && EveryoneReady())
        {
            startGame.gameObject.SetActive(true);
        }
        else
        {
            startGame.gameObject.SetActive(false);
        }
    }

    private bool EveryoneReady()
    {
        foreach(KeyValuePair<int, Player> pair in PhotonNetwork.CurrentRoom.Players)
        {
            Player player = pair.Value;
            if (player.CustomProperties.ContainsKey("isReady"))
            {
                bool ready = (bool) player.CustomProperties["isReady"];
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

    //room open false
    public void Click_Start()
    {
        PhotonNetwork.LoadLevel(nextScene);
    }

    public void Click_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(previousScene);
    }

    public void Click_Ready()
    {
        bool isReady = true;
        Hashtable hash = new Hashtable
        {
            { "isReady", isReady }
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
}
