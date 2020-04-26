using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TradeWithManyUI : MonoBehaviour
{

    private GameObject[] buttons = new GameObject[4];
    public GameObject panel;

    public string playerInput;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 4; i++)
        {
            buttons[i - 1] = panel.gameObject.transform.GetChild(i).gameObject;
            Debug.Log(buttons[i - 1].name);
        }
    }

    //populate buttons and open the panel
    public void Open(Player player)
    {
        nameButtons(player);
        panel.SetActive(true);
    }

    public void nameButtons(Player player)
    {
        int emptySlot = 0;
        Player[] playerList = PhotonNetwork.PlayerList;
        for (int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i] != player && playerList[i].GetCurrentRegion() == player.GetCurrentRegion())
            {
                buttons[emptySlot].GetComponentInChildren<Text>().text = playerList[i].NickName;
                emptySlot++;
            }
        }
    }

    public void savePlayerAndClose()
    {
        // get player's name from the clicked button
        string name = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.transform.GetComponent<Text>().text;
        Player player1 = PhotonNetwork.LocalPlayer;
        Player player2 = null;

        //get player 2
        Player[] playerList = PhotonNetwork.PlayerList;
        for (int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i].NickName == name) player2 = playerList[i];
        }

        if (name != "")
        {
            panel.SetActive(false);
            //raise an event for trade with multiple ppl
            object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, player2.ActorNumber };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { TargetActors = new int[] { player1.ActorNumber, player2.ActorNumber } };
            //RaiseEventOptions raiseEventOptions = new RaiseEventOptions { TargetActors = new int[] { player1.ActorNumber } };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(3, content, raiseEventOptions, sendOptions);

        }
    }
}
