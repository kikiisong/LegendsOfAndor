using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;

    private RoomsCanvases _roomsCanvases;

    public void FirstInitialize(RoomsCanvases roomsCanvases)
    {
        _roomsCanvases = roomsCanvases;
    }

    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;

        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4
        };
        PhotonNetwork.JoinOrCreateRoom("basic", options, TypedLobby.Default);
    }
    
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully" + this);
        _roomsCanvases.CurrentRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed: " + message + this);
    }
}
