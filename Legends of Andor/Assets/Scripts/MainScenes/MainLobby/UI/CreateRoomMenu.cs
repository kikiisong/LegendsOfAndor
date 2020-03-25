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


    public void Click_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;

        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4
        };
        if(_roomName.text != "")
        {
            PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
        }
        else
        {
            TryCreate(0);
        }
    }

    void TryCreate(int i)
    {
        try
        {
            RoomOptions options = new RoomOptions
            {
                MaxPlayers = 4
            };
            PhotonNetwork.JoinOrCreateRoom("Room " + i, options, TypedLobby.Default);
        }
        catch (Exception e)
        {
            print(e);
            TryCreate(i + 1);
        }
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
    }
}
