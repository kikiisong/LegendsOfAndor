using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//TO do:
//intialize players expand to when there are more than 2 ppl on the same region
public class TradeManager : MonoBehaviourPun
{
    public GameObject panelOne;
    public GameObject panelTwo;
    public GameObject panelErrMsg;

    public TradeWithManyUI tradeMany;

    private Player player1;
    private Player player2;

    private int emptySlotBag1;
    private int emptySlotBag2;
    private int emptySlot;
    private bool falconTrade;

    private byte TRADEITEM = 1;
    private byte OPENWIND = 2;
    private byte OPENWINDWHENMANY = 3;
    private byte CLOSEWIND = 4;




    public void Open()
    {


        string name = EventSystem.current.currentSelectedGameObject.name;

        if (name == "FalconTrade")
        {

            // falconTrade = true;
            int falcon = PhotonNetwork.LocalPlayer.GetHero().data.falcon;

            //if both panels were open, close them then
            if (panelOne.activeSelf && panelTwo.activeSelf)
            {
                object[] content = new object[] { };

                RaiseEventOptions raiseEventOptions = new RaiseEventOptions { TargetActors = new int[] { player1.ActorNumber, player2.ActorNumber } };
                SendOptions sendOptions = new SendOptions { Reliability = true };
                PhotonNetwork.RaiseEvent(CLOSEWIND, content, raiseEventOptions, sendOptions);
            }
            //check whether local player has falcon to trade 
            else if (falcon == 0)
            {
                Text t = panelErrMsg.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
                t.text = "You need to purchase a falcon before be able to trade with it.";
                panelErrMsg.SetActive(true);
            }
            else if (PhotonNetwork.LocalPlayer.GetHero().data.usedFalcon)
            {
                Text t = panelErrMsg.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
                t.text = "You have already used falcon today. You can use it only once a day";
                panelErrMsg.SetActive(true);
            }
            else
            {
                Debug.Log("falcon trade");
                tradeMany.Open(PhotonNetwork.LocalPlayer, true);
            }
        }
        else
        {
            //If more than 2 players on the same region, display panel allowing choosing a player you want to trade with

            int playersOnRegion = 0;
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                if (PhotonNetwork.PlayerList[i].GetCurrentRegion() == PhotonNetwork.LocalPlayer.GetCurrentRegion()) playersOnRegion++;
            }
            //if more there are more than 2 person on the same region and we wish to trade,
            // we will display a panel allowing player to select a player they wish to trade with
            if (playersOnRegion > 2)
            {
                Debug.Log("trade with many");
                tradeMany.Open(PhotonNetwork.LocalPlayer, false);
            }
            if (playersOnRegion == 2)
            {
                // if only two players are on the same region, then we will trigger this function 
                initPlayers();
                if (player1 != null && player2 != null)
                {

                    object[] content = new object[] { player1.ActorNumber, player2.ActorNumber, false };

                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { TargetActors = new int[] { player1.ActorNumber, player2.ActorNumber } };
                    SendOptions sendOptions = new SendOptions { Reliability = true };
                    PhotonNetwork.RaiseEvent(OPENWIND, content, raiseEventOptions, sendOptions);
                }
            }
            else // display error message
            {
                Text t = panelErrMsg.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
                t.text = "You need to be on the same region as the person you want to trade with. Otherwise, you can use falcon if you have it.";
                panelErrMsg.SetActive(true);
            }
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
            int slotID = (int)data[2];

            bool canTrade = true;
            //check whethear the item can be trade if we're using falcon
            if (falconTrade) canTrade = canTradeFalcon(name);

            if (canTrade)
            {

                GameObject panelReceive = (bagType == 1) ? panelTwo : panelOne; //selectign panel to increment
                GameObject panelSend = (bagType == 2) ? panelTwo : panelOne; //selectign panel to increment

                decrItem(panelSend, slotID);
                incrItem(name, panelReceive);


                Debug.Log(panelOne.name + " - panel one");
                Debug.Log(panelTwo.name + " - panel two");
                Debug.Log(player1.NickName + " - player 1 nickname");
                Debug.Log(player2.NickName + " - player 2 nickname");
                if (bagType == 1)
                {
                    updateHeroStats(player1, name, (-1));
                    updateHeroStats(player2, name, 1);
                }
                else
                {
                    updateHeroStats(player1, name, 1);
                    updateHeroStats(player2, name, -1);
                }
            }
        }

        //open window only to two players
        if (obj.Code == OPENWIND || obj.Code == OPENWINDWHENMANY)
        {
            object[] data = (object[])obj.CustomData;
            int player1ID = (int)data[0];
            int player2ID = (int)data[1];
            bool falconTrade = (bool)data[2];


            //update hero's parameter's 
          //  if (falconTrade) PhotonNetwork.CurrentRoom.GetPlayer(player1ID).GetHero().data.usedFalcon = true;

            if (obj.Code == OPENWIND) initPlayers();
            else // when there are multiple player's on the same region
            {
                // in this case scenario we know for a fact who is who, so shouldn't be an issue when it comes to players' intialization
                player1 = PhotonNetwork.CurrentRoom.GetPlayer(player1ID);
                player2 = PhotonNetwork.CurrentRoom.GetPlayer(player2ID);
            }

            if (panelOne != null && panelTwo != null)
            {
                bool isActive1 = panelOne.activeSelf;
                bool isActive2 = panelTwo.activeSelf;
                
                panelOne.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = player1.NickName +"'s Bag";
                panelTwo.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = player2.NickName + "'s Bag";
                panelOne.SetActive(!isActive1);
                panelTwo.SetActive(!isActive2);

                cleanBag(panelOne);
                cleanBag(panelTwo);


                populateBag(player1, panelOne);
                populateBag(player2, panelTwo);
            }
        }

        if(obj.Code == CLOSEWIND)
        {


            player1.GetHero().data.usedFalcon = true;
            //player2.Get

            panelOne.SetActive(false);
            panelTwo.SetActive(false);
        }

    }


    private void decrItem(GameObject bag, int slotID)
    {
        GameObject slot = bag.gameObject.transform.GetChild(0).GetChild(slotID).gameObject; //to change later

        GameObject image = slot.transform.GetChild(0).gameObject;
        Image img = image.gameObject.GetComponent<Image>();

        GameObject text = slot.transform.GetChild(1).gameObject;
        Text tx = text.gameObject.GetComponent<Text>();

        Sprite uimask = Resources.Load<Sprite>("UIMask");

        //usure that clicking on empty icon won't do anything
        if (img.sprite.name != uimask.name)
        {

            if (tx.text == "")
            {
                img.sprite = uimask;
            }
            else
            {
                int count = int.Parse(tx.text);
                count--;

                //empty slot if dropped item
                if (count != 0)
                {
                    tx.text = (count).ToString();
                }
                else
                {
                    img.sprite = uimask;
                    tx.text = "";
                }
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

        Sprite spriteToLoad = Resources.Load<Sprite>(spriteName);

        //if we already have the element we just need to update its number
        if (empty != -1)
        {
            GameObject text = bag.gameObject.transform.GetChild(0).GetChild(empty).GetChild(1).gameObject;
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
            GameObject image = bag.gameObject.transform.GetChild(0).GetChild(emptySlot).GetChild(0).gameObject;
            Image img = image.gameObject.GetComponent<Image>();
            img.sprite = spriteToLoad;
        }
    }


    //displays image of a certain item in the bag
    public void fillBag(int slotNumber, string spriteName, int parameter, GameObject bag)
    {
        GameObject itemsList = bag.gameObject.transform.GetChild(0).gameObject;
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
        if (spriteName == "UIMask" && parameter == -12)
        {
            tx.text = "";
        }
    }

    private void cleanBag(GameObject bag)
    {
        for (int i = 0; i < 6; i++)
        {
            fillBag(i, "UIMask", -12, bag);
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

        //name panel's
       // bag.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "'s Bag";
    }

    //works only if there are two ppl on the same region
    private void initPlayers()
    {
        Region region = PhotonNetwork.LocalPlayer.GetCurrentRegion();
        int r = 0;

        Player[] playerList = PhotonNetwork.PlayerList;
        for (int i = 0; i < playerList.Length; i++)
        {
            if (playerList[i].GetCurrentRegion() == region && r == 0)
            {
                player1 = playerList[i];
                r++;
            }
            else if (playerList[i].GetCurrentRegion() == region && r == 1)
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
        //  emptySlotBag = 0;
        for (int i = 5; i >= 0; i--)
        {
            GameObject image = bag.gameObject.transform.GetChild(0).GetChild(i).GetChild(0).gameObject;
            Image img = image.gameObject.GetComponent<Image>();

            if (img.sprite.name == name)
            {
                return i;
            }
            else if (img.sprite.name == "UIMask")
            {
                emptySlot = i;
            }
        }
        return -1;
    }

    //check whethear the item can be trade using falcon
    private bool canTradeFalcon(string itemName)
    {
        Debug.Log("inside can trade falcon");
        if (itemName != "shield" || itemName != "bow") return true;
        else
        {
            Text t = panelErrMsg.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
            t.text = "Cannot trade " + itemName + " using falcon trade";
            panelErrMsg.SetActive(true);
            return false;
        } 
    }

}
