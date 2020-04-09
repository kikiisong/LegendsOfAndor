using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMapManager : MonoBehaviourPun
{
    public static GameMapManager Instance;

    [Header("TimeMarkers")]
    public List<Transform> timeMarkerUpdatePositions;
    public List<Transform> timeMarkerInitialPositions;

    [SceneName] public string leaveScene;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Not singleton");
        }

        Music.Stop();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickLeaveRPC()
    {
        photonView.RPC("LeaveRoom", RpcTarget.AllViaServer);
    }

    [PunRPC]
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(leaveScene);
    }
}
