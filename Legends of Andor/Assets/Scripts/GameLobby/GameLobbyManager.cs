using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobbyManager : MonoBehaviour
{
    [SceneName]
    public string nextScene;
    [SceneName]
    public string previousScene;

    public Button startGame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.LocalPlayer.IsMasterClient && EveryoneReady())
        {
            startGame.gameObject.SetActive(true);
        }
    }

    private bool EveryoneReady()
    {
        return true;
    }

    public void Click_Start()
    {
        PhotonNetwork.LoadLevel(nextScene);
    }

    public void Click_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(previousScene);
    }
}
