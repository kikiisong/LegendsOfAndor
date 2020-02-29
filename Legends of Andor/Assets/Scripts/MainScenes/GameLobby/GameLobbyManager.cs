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

    [Header("Hero Selection")]
    public GameObject characterSelectionPrefab;

    private HeroSelection heroSelection;
    private bool isReady;

    private void Start()
    {
        heroSelection = PhotonNetwork.Instantiate(characterSelectionPrefab.name, Vector3.zero, Quaternion.identity)
            .GetComponent<HeroSelection>();
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
        isReady = !isReady;
        Hashtable hash = new Hashtable
        {
            { K.Player.isReady, isReady }
        };
        if (isReady && !IsTaken(heroSelection.CurrentHero))
        {
            hash.Add(K.Player.hero, heroSelection.CurrentHero);
        }
        else
        {
            hash.Add(K.Player.hero, null);
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    private bool IsTaken(HeroUIData hero)
    {
        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (player != PhotonNetwork.LocalPlayer)
            {
                HeroUIData heroUIData = (HeroUIData)player.CustomProperties[K.Player.hero];
                if (heroUIData != null)
                {
                    return heroUIData.type == hero.type;
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
