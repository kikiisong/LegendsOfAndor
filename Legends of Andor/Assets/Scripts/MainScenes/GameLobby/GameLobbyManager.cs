using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Scenes")]
    [SceneName]
    public string nextScene;
    [SceneName]
    public string previousScene;

    [Header("UI")]
    public GameObject startGame;
    public Text difficulty;
    public Button ready;

    [Header("Resources")]
    public GameObject characterSelectionPrefab;

    private HeroSelection characterSelection;
    private bool isReady;

    private void Start()
    {
        characterSelection = PhotonNetwork.Instantiate(characterSelectionPrefab.name, Vector3.zero, Quaternion.identity)
            .GetComponent<HeroSelection>();
    }

    // Update is called once per frame
    void Update()
    {
        //Use RPC later
        if (PhotonNetwork.LocalPlayer.IsMasterClient && EveryoneReady())
        {
            startGame.gameObject.SetActive(true);
        }
        else
        {
            startGame.gameObject.SetActive(false);
        }
    }

    private bool EveryoneReady()
    {
        foreach(KeyValuePair<int, Player> pair in PhotonNetwork.CurrentRoom.Players)
        {
            Player player = pair.Value;
            if (player.CustomProperties.ContainsKey("isReady"))
            {
                bool ready = (bool) player.CustomProperties["isReady"];
                if (!ready)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    //room open false
    public void Click_Start()
    {
        PhotonNetwork.LoadLevel(nextScene);
    }

    public void Click_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(previousScene);
    }

    public void Click_Ready()
    {
        isReady = !isReady;
        Hashtable hash = new Hashtable
        {
            { "isReady", isReady }
        };
        if (isReady)
        {
            hash.Add("character", characterSelection.CurrentCharacter);
        }
        else
        {
            hash.Add("character", null);
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public void Click_Difficulty()
    {
        if (difficulty.text.Equals("Easy"))
        {
            difficulty.text = "Normal";
        }
        else
        {
            difficulty.text = "Easy";
        }

        Hashtable hashtable = new Hashtable()
        {
            {"difficulty", difficulty.text}
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }
}
