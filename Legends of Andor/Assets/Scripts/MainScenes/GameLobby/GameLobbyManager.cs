using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public TextMeshProUGUI roomNameUI;

    [Header("Hero Selection")]
    public GameObject heroSelectionPrefab;
    public GameObject canvasParent;

    private HeroSelection heroSelection;

    bool IsReady
    {
        get
        {
            return PhotonNetwork.LocalPlayer.IsReady();
        }
    }

    private void Start()
    {
        heroSelection = PhotonNetwork.Instantiate(heroSelectionPrefab.name, Vector3.zero, Quaternion.identity)
            .GetComponent<HeroSelection>();
        heroSelection.SetParentRPC(canvasParent);

        roomNameUI.text = PhotonNetwork.CurrentRoom.Name;
        difficulty.text = Room.Difficulty.ToString();
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
        foreach(Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (!player.IsReady()) return false;
        }
        return true;
    }

    //room open false
    public void Click_Start()
    {
        PhotonNetwork.CurrentRoom.SetDifficulty((Difficulty)Enum.Parse(typeof(Difficulty), difficulty.text));
        PhotonNetwork.LoadLevel(nextScene);
    }

    public void Click_Ready()
    {
        if (!IsReady && !IsTaken(heroSelection.CurrentHero))
        {
            PhotonNetwork.LocalPlayer.SetReady(true).SetHero(heroSelection.CurrentHero);
        }
        else
        {            
            PhotonNetwork.LocalPlayer.SetReady(false).SetHero(null);
        }
    }

    private bool IsTaken(Hero hero)
    {
        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (player != PhotonNetwork.LocalPlayer)
            {
                if(player.IsReady() && player.GetHero().type == hero.type)
                {
                    return true;
                }
            }

        }
        return false;
    }

    public void Click_Difficulty()
    {
        if (difficulty.text.Equals(Difficulty.Easy.ToString()))
        {
            difficulty.text = Difficulty.Normal.ToString();
        }
        else
        {
            difficulty.text = Difficulty.Easy.ToString();
        }
    }

    public void Click_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(previousScene);
    }
}
