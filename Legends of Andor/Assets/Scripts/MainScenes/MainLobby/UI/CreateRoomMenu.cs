using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using System;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;
    [SceneName]
    public string nextScene;

    int attemps = 0;

    public void Click_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;

        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4
        };
        
        if(!PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default))
        {
            TryCreate();
        }
    }

    void TryCreate()
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4
        };
        PhotonNetwork.CreateRoom("Room" + attemps, options, TypedLobby.Default);
        attemps++;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully. ", this);
        //roomsCanvases.CurrentRoomCanvas.Show();
        PhotonNetwork.LoadLevel(nextScene);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message, this);
        if (attemps > 0) TryCreate();
    }
}
