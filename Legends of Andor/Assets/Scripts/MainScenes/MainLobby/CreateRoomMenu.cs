using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using System;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SceneName]
    public string nextScene;

    int attempts = 0;

    public void Click_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;

        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4
        };
       
        if(!PhotonNetwork.JoinOrCreateRoom(MainLobbyManager.Instance.roomNameUI.text, options, TypedLobby.Default)){
            TryCreate();
        }
        
    }

    void TryCreate()
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4,

        };
        PhotonNetwork.CreateRoom("Room" + attempts, options, TypedLobby.Default);
        attempts++;
    }

    public override void OnCreatedRoom()
    {
        if (Room.IsSaved) return;
        print("Created room successfully.");
        PhotonNetwork.LoadLevel(nextScene);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        if (Room.IsSaved) return;
        Debug.Log("Room creation failed: " + message, this);
        if (attempts > 0) TryCreate();
    }
}
