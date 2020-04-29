using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using Newtonsoft.Json.Linq;
using Photon.Pun;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLobby_SavedManager : MonoBehaviourPunCallbacks
{
    public GameObject prefab;
    public GameObject parent;

    [SceneName] public string next;
    [SceneName] public string previous;

    public Button nextButton;

    // Start is called before the first frame update
    void Start()
    {
        nextButton.gameObject.SetActive(false);
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (var jToken in Room.Json["heroes"])
            {
                SelectHero selectHero = PhotonNetwork.Instantiate(prefab).GetComponent<SelectHero>();
                selectHero.SetParentRPC(parent);
                selectHero.InitRPC(jToken);
            }
        }
    }

    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            bool everyoneReady = PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount;
            foreach(Player player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                if (player.GetHero() == null) everyoneReady = false;
            }
            nextButton.gameObject.SetActive(everyoneReady);
        }
    }

    public void Click_Next()
    {
        PhotonNetwork.LoadLevel(next);
    }

    public void Click_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(previous);
    }
}
