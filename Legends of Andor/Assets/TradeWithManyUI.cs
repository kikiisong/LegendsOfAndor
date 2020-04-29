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
    public GameObject panelErrMsg;

    private bool falconTrade = false;
    public string playerInput;

    // Start is called before the first frame update
    void Awake()
    {

        for (int i = 0; i < 3; i++)
        {
            Debug.Log("tryin got add buttons buttons");
            buttons[i] = panel.gameObject.transform.GetChild(i).gameObject;
            Debug.Log("buttons");
            Debug.Log(buttons[i].name);
        }
    }

    //populate buttons and open the panel
    public void Open(Player player, bool falconTr)
    {
        falconTrade = falconTr;
        nameButtons(player);
        panel.SetActive(true);
    }

    public void nameButtons(Player player)
    {

        for (int i = 0; i < 3; i++)
        {
            buttons[i] = panel.gameObject.transform.GetChild(i).gameObject;
            Debug.Log(buttons[i].name);
        }

        Debug.Log("this is falcon trade " + falconTrade);
        int emptySlot = 0;
        Player[] playerList = PhotonNetwork.PlayerList;
        for (int i = 0; i < playerList.Length; i++)
        {
            if (!falconTrade)
            {
                if (playerList[i] != player && playerList[i].GetCurrentRegion() == player.GetCurrentRegion())
                {
                    buttons[emptySlot].GetComponentInChildren<Text>().text = playerList[i].NickName;
                    emptySlot++;
                }
            }
            else
            {
                if (playerList[i] != player )
                {
                    buttons[emptySlot].GetComponentInChildren<Text>().text = playerList[i].NickName;
                    emptySlot++;
                }
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
            object[] content = new object[] { PhotonNetwork.LocalPlayer.ActorNumber, player2.ActorNumber, falconTrade };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { TargetActors = new int[] { player1.ActorNumber, player2.ActorNumber } };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(3, content, raiseEventOptions, sendOptions);

        }
    }
}
