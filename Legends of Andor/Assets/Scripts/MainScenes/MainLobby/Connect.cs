using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;
using UnityEngine.SceneManagement;


//Connects to server
public class Connect : MonoBehaviourPunCallbacks
{
    [SceneName]
    public string previous;

    // Start is called before the first frame update
    void Start()
    {
        print("Connecting to server");
        PhotonNetwork.NickName = PlayerPrefs.GetString(K.USERNAME);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        CustomTypes.Register();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print("Joined lobby " + PhotonNetwork.CurrentLobby.Name);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason " + cause.ToString());
    }

    public void Click_Disconnect()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(previous);
    }
}
