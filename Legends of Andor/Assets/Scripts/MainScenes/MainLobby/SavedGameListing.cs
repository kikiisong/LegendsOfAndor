using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class SavedGameListing : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI textUI;

    public void Init(string name)
    {
        textUI.text = name;
    }

    public void Click_Create()
    {
        MainLobbyManager.IsSaved = true;
        if (!PhotonNetwork.IsConnected) return;

        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 4 //TODO change
        };

        PhotonNetwork.CreateRoom(MainLobbyManager.Instance.roomNameUI.text, options, TypedLobby.Default);
    } 
}
