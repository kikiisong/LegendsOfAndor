﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConnect : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        print("Connecting to server.");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server.");
        print(PhotonNetwork.LocalPlayer.NickName);

        if (!PhotonNetwork.InLobby) PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason : " + cause.ToString());
    }

    public override void OnJoinedLobby()
    {
        print("Joined Lobby: " + PhotonNetwork.CurrentLobby.ToString());
        print("Version: " + PhotonNetwork.GameVersion);
    }
}
