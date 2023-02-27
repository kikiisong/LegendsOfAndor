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
        if (!PhotonNetwork.IsConnected) return;

        var json = Saving.Helper.GetJson(textUI.text);
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = json["room"]["num_players"].ToObject<byte>()
        };
        options.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable() {
            {"difficulty", json["room"]["difficulty"].ToObject<int>() },
            { "json", json.ToString()}
        };

        //PhotonNetwork.CreateRoom(MainLobbyManager.Instance.roomNameUI.text, options, TypedLobby.Default);
        PhotonNetwork.CreateRoom(textUI.text, options, TypedLobby.Default);
    }
}
