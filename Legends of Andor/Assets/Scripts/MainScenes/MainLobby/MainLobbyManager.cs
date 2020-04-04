using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Custom;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainLobbyManager : MonoBehaviourPunCallbacks
{
    [SceneName]
    public string previous;

    public GameObject createRoom;
    public Text roomNameUI;

    public static MainLobbyManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        createRoom.SetActive(false);
        Connect();
    }

    void Connect()
    {
        print("Connecting to server");
        PhotonNetwork.NickName = PlayerPrefs.GetString(Preferences.USERNAME);
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        CustomTypes.Register();
    }

    public void Click_Disconnect()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(previous);
    }  

    //Callbacks
    public override void OnConnectedToMaster()
    {
        print("Connected to server");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("Joined lobby " + PhotonNetwork.CurrentLobby.Name);
        createRoom.SetActive(true);
        print(PhotonNetwork.LocalPlayer.IsReady());

    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason " + cause.ToString());
    }
}
