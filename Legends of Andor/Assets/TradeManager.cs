using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//TO do:
//intialize players expand to when there are more than 2 ppl on the same region
public class TradeManager : MonoBehaviourPun
{
    public GameObject panelOne;
    public GameObject panelTwo;

    private Player player1;
    private Player player2;

    private int emptySlotBag1;
    private int emptySlotBag2;
    private int emptySlot;

    private byte TRADEITEM = 1;
    private byte OPENWIND = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Open()
    {   
        initPlayers();

        if (player1 != null && player2 != null) {
        //if (player1 != null )
        {
            object[] content = new object[] { true };

            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { TargetActors = new int[] { player1.ActorNumber, player2.ActorNumber } };
            //RaiseEventOptions raiseEventOptions = new RaiseEventOptions { TargetActors = new int[] { player1.ActorNumber } };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(OPENWIND, content, raiseEventOptions, sendOptions);
        }

    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }
    private void NetworkingClient_EventReceived(EventData obj)
    {
        //if coming form current player bag then
        if (obj.Code == TRADEITEM)
        {
            object[] data = (object[])obj.CustomData;
            string name = (string)data[0];
            int bagType = (int)data[1];
            Debug.Log(bagType);

            GameObject panel = (bagType == 1) ? panelTwo : panelOne; //selectign panel to increment
            incrItem(name, panel);

            if (bagType == 1) {
                updateHeroStatsRPC(name, player1, -1);
                updateHeroStatsRPC(name, player2, 1);
            }
            else
            {
                updateHeroStatsRPC(name, player1, 1);
                updateHeroStatsRPC(name, player2, -1);
            }
        }

        //open window only to two players
        if (obj.Code == OPENWIND)
        {
            if (panelOne != null && panelTwo != null)
            {
                bool isActive1 = panelOne.activeSelf;
                bool isActive2 = panelTwo.activeSelf;
                panelOne.SetActive(!isActive1);
                panelTwo.SetActive(!isActive2);


                populateBag(player1, panelOne);
                populateBag(player2, panelTwo);
            }
        }
    }
    public void updateHeroStatsRPC(string spriteName, Player player, int updateUnit)
    {
        photonView.RPC("updateHeroStats", RpcTarget.All, player, spriteName, updateUnit);
    }

    [PunRPC]
    public void updateHeroStats(Player player, string spriteName, int updateUnit)
    {
        Hero hero = player.GetHero();
        if (spriteName == "coin") hero.data.gold += updateUnit;
        if (spriteName == "brew") hero.data.brew += updateUnit;
        if (spriteName == "wineskin") hero.data.wineskin += updateUnit;
        if (spriteName == "herb") hero.data.herb += updateUnit;
        if (spriteName == "shield") hero.data.shield += updateUnit;
        if (spriteName == "helm") hero.data.helm += updateUnit;
        if (spriteName == "bow") hero.data.bow += updateUnit;
        if (spriteName == "falcon") hero.data.falcon += updateUnit;
    }

    //bag - to which we should increase the element
    private void incrItem(String spriteName, GameObject bag)
    {
        int empty = containsElement(spriteName, bag);

        Debug.Log("empty space is " + empty);
        Sprite spriteToLoad = Resources.Load<Sprite>(spriteName);

        //if we already have the element we just need to update its number
        if (empty != -1)
        {
            GameObject text = bag.gameObject.transform.GetChild(1).GetChild(empty).GetChild(1).gameObject;
            Text tx = text.gameObject.GetComponent<Text>();

            if (tx.text == "")
            {
                tx.text = "2";
            }
            else
            {
                int v = int.Parse(tx.text);
                v++;
                tx.text = (v).ToString();
            }
        }
        else // new element to add
        {
            GameObject image = bag.gameObject.transform.GetChild(1).GetChild(emptySlot).GetChild(0).gameObject;
            Image img = image.gameObject.GetComponent<Image>();
            img.sprite = spriteToLoad;
        }
    }



    //displays image of a certain item in the bag
    public void fillBag(int slotNumber, string spriteName, int parameter, GameObject bag)
    {
        GameObject itemsList = bag.gameObject.transform.GetChild(1).gameObject;
        Sprite spriteToLoad;
        if (spriteName == "UIMask")
        {
            spriteToLoad = Resources.Load<Sprite>("UIMask");
        }
        else
        {
            spriteToLoad = Resources.Load<Sprite>(spriteName);
        }

        GameObject image = itemsList.gameObject.transform.GetChild(slotNumber).GetChild(0).gameObject;
        GameObject text = itemsList.gameObject.transform.GetChild(slotNumber).GetChild(1).gameObject;

        Image img = image.gameObject.GetComponent<Image>();
        Text tx = text.gameObject.GetComponent<Text>();

        img.sprite = spriteToLoad;
        if (parameter > 1)
        {
            tx.text = parameter.ToString();
        }
    }


    private void populateBag(Player player, GameObject bag)
    {
        Hero hero = player.GetHero();
        int emptySlot = 0;

        if (hero.data.wineskin > 0)
        {
        fillBag(emptySlot, "wineskin", hero.data.wineskin, bag);
            emptySlot++;
        }
        if (hero.data.gold > 0)
        {
            fillBag(emptySlot, "coin", hero.data.gold, bag);
            emptySlot++;
        }
        if (hero.data.brew > 0)
        {
            fillBag(emptySlot, "brew", hero.data.brew, bag);
            emptySlot++;
        }
        if (hero.data.herb > 0)
        {
            fillBag(emptySlot, "herb", hero.data.herb, bag);
            emptySlot++;
        }
        if (hero.data.shield > 0)
        {
            fillBag(emptySlot, "shield", hero.data.shield, bag);
            emptySlot++;
        }
        if (hero.data.helm > 0)
        {
            fillBag(emptySlot, "helm", hero.data.helm, bag);
            emptySlot++;
        }
        if (hero.data.bow > 0)
        {
            fillBag(emptySlot, "bow", hero.data.bow, bag);
            emptySlot++;
        }
        if (hero.data.falcon > 0)
        {
            fillBag(emptySlot, "falcon", hero.data.falcon, bag);
            emptySlot++;
        }
        
    }

    //works only if there are two ppl on the same region
    private void initPlayers()
    {
        player1 = PhotonNetwork.LocalPlayer;

        Player[] playerList = PhotonNetwork.PlayerList;
        for (int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i].GetCurrentRegion() == player1.GetCurrentRegion() && playerList[i] != player1)
            {
                player2 = playerList[i];
            }
        }
    }

    //returns -1 if no such elements exists
    // returns its slot if it was found
    //sets emptySlot of a bag to the first empty slot
    public int containsElement(string name, GameObject bag)
    {
        Debug.Log("inside contains elements. Sprite name " +  name);
      //  emptySlotBag = 0;
        for (int i = 5; i >= 0; i--)
        {
            GameObject image = bag.gameObject.transform.GetChild(1).GetChild(i).GetChild(0).gameObject;
            Image img = image.gameObject.GetComponent<Image>();

            if (img.sprite.name == name)
            {
                return i;
            }
            else if (img.sprite.name == "UIMask")
            {
                emptySlot= i;
            }
        }

        Debug.Log("emptyslotbag " + emptySlot);
        return -1;
    }

}
