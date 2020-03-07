using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Scenes")] [SceneName]
    public string nextScene;
    [SceneName]
    public string previousScene;

    [Header("UI")]
    public GameObject startGame;
    public Text difficulty;
    public Button ready;

    [Header("Hero Selection")]
    public GameObject heroSelectionPrefab;
    public GameObject canvasParent;

    private HeroSelection heroSelection;
    private bool isReady;

    private void Start()
    {
        heroSelection = PhotonNetwork.Instantiate(heroSelectionPrefab.name, Vector3.zero, Quaternion.identity)
            .GetComponent<HeroSelection>();
        heroSelection.SetParentRPC(canvasParent);
        
    }

    // Update is called once per frame
    void Update()
    {
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
            if (player.CustomProperties.ContainsKey(K.Player.isReady))
            {
                bool ready = (bool) player.CustomProperties[K.Player.isReady];
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
        if (!isReady && !IsTaken(heroSelection.CurrentHero))
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
            {
                { K.Player.isReady, true },
                { K.Player.hero, heroSelection.CurrentHero }
            });
            isReady = true;
        }
        else if (isReady)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable
            {
                { K.Player.isReady, false }
            });
            isReady = false;
        }
        else
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable());
        }
    }

    private bool IsTaken(Hero hero)
    {
        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (player != PhotonNetwork.LocalPlayer)
            {
                Hero h = (Hero)player.CustomProperties[K.Player.hero];
                if (h != null)
                {
                    return h.type == hero.type;
                }
            }

        }
        return false;
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
            {K.Room.difficulty, difficulty.text}
        };
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
    }
}
