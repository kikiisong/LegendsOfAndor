﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class EventCardController : MonoBehaviourPun
{
   // public List<int> cards = new List<int>(34);
    private static System.Random rand = new System.Random();

    public List<GameObject> evnetCardList;
    public int currentEventIndex;
    public GameObject currentEventCard;
    public GameObject useShieldOptionPanel;

    //public GameObject wellBackUp45;
    public GameObject wellPrefab;

    public GameObject narrator;

    public bool usedShieldFor3;
    // store the current region of this player's hero
    Region currentRegion;

    // store my current hero
    Hero myhero;

    // store the previous WP before the sheild has been used
    int[] perviousWP;

    internal void flipped()
    {
        //TODO
    }

    public GameObject myEventCardButton;

    // Start is called before the first frame update
    void Start()
    {
        //then call RPCPun to other clients
        // photonView.RPC("getCards", RpcTarget.AllBuffered, cards);
        myhero = PhotonNetwork.LocalPlayer.GetHero();
       // myhero.data.shield += 1;
        perviousWP = new int[PhotonNetwork.PlayerList.Length];
    }

    public void newEventCard(int a)
    {
        currentEventIndex = a;
        currentEventCard = evnetCardList[a];
        myEventCardButton.GetComponent<eventCardButton>().setEventCard(currentEventCard);
        currentEventCard.SetActive(true);

        if (PhotonNetwork.IsMasterClient)
        {
            if (a == 0)
            {
                photonView.RPC("eventCard2", RpcTarget.AllBuffered);
            }
            else if (a == 1)
            {
                photonView.RPC("eventCard4", RpcTarget.AllBuffered);
            }
            else if (a == 2)
            {
                photonView.RPC("eventCard5", RpcTarget.AllBuffered);
            }
            else if (a == 3)
            {
                photonView.RPC("eventCard11", RpcTarget.AllBuffered);
            }
            else if (a == 4)
            {
                photonView.RPC("eventCard13", RpcTarget.AllBuffered);
            }
            else if (a == 5)
            {
                photonView.RPC("eventCard14", RpcTarget.AllBuffered);
            }
            else if (a == 6)
            {
                photonView.RPC("eventCard17", RpcTarget.AllBuffered);
            }
            else if (a == 7)
            {
                photonView.RPC("eventCard22", RpcTarget.AllBuffered);
            }
            else if (a == 8)
            {
                photonView.RPC("eventCard24", RpcTarget.AllBuffered);
            }
            else if (a == 9)
            {
                photonView.RPC("eventCard28", RpcTarget.AllBuffered);
            }
            else if (a == 10)
            {
                photonView.RPC("eventCard29", RpcTarget.AllBuffered);
            }
            else if (a == 11)
            {
                photonView.RPC("eventCard30", RpcTarget.AllBuffered);
            }
            else if (a == 12)
            {
                photonView.RPC("eventCard31", RpcTarget.AllBuffered);
            }
            else if (a == 13)
            {
                photonView.RPC("eventCard32", RpcTarget.AllBuffered);
            }
        }
    }


    private Region findCurrentRegion()
    {
        //Extract current player's region
        foreach (HeroMoveController c in GameObject.FindObjectsOfType<HeroMoveController>())
        {
            if (c.photonView.Owner == PhotonNetwork.LocalPlayer)
            {
                return GameGraph.Instance.FindNearest(c.transform.position);
            }
        }
        throw new System.Exception("No current region");
    }

    // #2 Any hero standing on a space between 0 and 20 looses 3 WP (shiled)
    [PunRPC]
    public void eventCard2()
    {
        useShieldOptionPanel.SetActive(true);
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
           
            Hero hero = (Hero)players[i].GetHero();
            perviousWP[i] = hero.data.WP;
            if (players[i].GetCurrentRegion().label >= 0 && players[i].GetCurrentRegion().label <= 20)
            {
                if (hero.data.WP <= 2)
                {
                    hero.data.WP = 0;
                }
                else
                {
                    hero.data.WP = hero.data.WP - 3;
                }
            }
        }

    }

    // #4 Any hero standing on a space between 37 and 70 looses 3 WP  (shiled)
    [PunRPC]
    public void eventCard4()
    {
        useShieldOptionPanel.SetActive(true);
        //  print("hero's WP was " + myhero.data.WP);
        Player[] players = PhotonNetwork.PlayerList;


        for (int i = 0; i < players.Length; i++)
        {

            Hero hero = (Hero)players[i].GetHero();
            perviousWP[i] = hero.data.WP;
            if (players[i].GetCurrentRegion().label >= 37 && players[i].GetCurrentRegion().label <= 70)
            {
                if (hero.data.WP <= 2)
                {
                    hero.data.WP = 0;
                }
                else
                {
                    hero.data.WP = hero.data.WP - 3;
                }
            }
        }

    }

    // #5 Wizard and Archer get 3 WP
    [PunRPC]
    public void eventCard5()
    {
      //  print("hero's WP was " + myhero.data.WP);

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {

            Hero hero = (Hero)players[i].GetHero();
            if (hero.type == Hero.Type.WIZARD || hero.type == Hero.Type.ARCHER)
            {
                hero.data.WP += 3;
            }
        }

    }

    // #11 On this day every creature has +1 SP  (shiled)
    [PunRPC]
    public void eventCard11()
    {
       // print("Event card 3 has beeen called here at eventCard11");

        useShieldOptionPanel.SetActive(true);

        foreach (MonsterMoveController monster in FindObjectsOfType<MonsterMoveController>())
        {
           // print("Event card 3 has beeen called here at inside foreach");
            monster.data.sp += 1;
        }
    }

    // #13 Every hero with WP < 10 increments WP to 10 
    [PunRPC]
    public void eventCard13()
    {
      //  print("hero's WP was " + myhero.data.WP);

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {

            Hero hero = (Hero)players[i].GetHero();
            perviousWP[i] = hero.data.WP;

            if (hero.data.WP < 10)
            {
                hero.data.WP = 10;
            }
        }

    }

    // #14 Dwarf and Warrior receive 3 WP
    [PunRPC]
    public void eventCard14()
    {
      //  print("hero's WP was " + myhero.data.WP);
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {

            Hero hero = (Hero)players[i].GetHero();
            if (hero.type == Hero.Type.DWARF || hero.type == Hero.Type.WARRIOR)
            {
                hero.data.WP += 3;
            }
        }
    }

    // #17 Every hero with more than 12 WP goes down to 12 WP  (shield)
    [PunRPC]
    public void eventCard17()
    {
        useShieldOptionPanel.SetActive(true);
        //  print("hero's WP was " + myhero.data.WP)
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            Hero hero = (Hero)players[i].GetHero();
            perviousWP[i] = hero.data.WP;
            if (hero.data.WP > 12)
            {
                hero.data.WP = 12;
            }
        }
    }

    // #22 Well token on space 45 is removed from the game  (shiled)
    [PunRPC]
    public void eventCard22()
    {
        useShieldOptionPanel.SetActive(true);
        List<Well> well = GameGraph.Instance.FindObjectsOnRegion<Well>(GameGraph.Instance.Find(45));
        if (well.Count == 0)
        {
            return;
        }
        else
        {
            Destroy(well[0].gameObject);
        }

    }

    // #24 Any hero not on a forest space or in the mine, tavern or castle loses 2 WP (shield)
    [PunRPC]
    public void eventCard24()
    {
        //  print("hero's WP was " + myhero.data.WP);
        useShieldOptionPanel.SetActive(true);
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {

            Hero hero = (Hero)players[i].GetHero();
            perviousWP[i] = hero.data.WP;
            if ((players[i].GetCurrentRegion().label >= 47 && players[i].GetCurrentRegion().label <= 63)||
                (players[i].GetCurrentRegion().label >= 22 && players[i].GetCurrentRegion().label <= 25) ||
                (players[i].GetCurrentRegion().label == 71) ||
                (players[i].GetCurrentRegion().label == 72) ||
                (players[i].GetCurrentRegion().label == 0))
            {
                return;
            }
            else
            {
                if (hero.data.WP <= 2)
                {
                    hero.data.WP = 0;
                }
                else
                {
                    hero.data.WP = hero.data.WP - 2;
                }
            }
        }
    }

    // #28 Every hero whose time marker is in the sunrise box gains 2 WP
    [PunRPC]
    public void eventCard28()
    {
        //  print("hero's WP was " + myhero.data.WP);

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {

            Hero hero = (Hero)players[i].GetHero();
            if (hero.data.NumHours == 0)
            {
                hero.data.WP += 2;
            }
        }

    }

    // #29 Shield appears on space 57
    [PunRPC]
    public void eventCard29()
    {
        print("this is event card 29");

        //place shield icon on region 57
        GameObject g = PhotonNetwork.Instantiate(Resources.Load<GameObject>("ShieldPrefab"));
        GameGraph.Instance.PlaceAt(g.gameObject, 57);

        //update region stats
        Region r = GameGraph.Instance.Find(57);
        r.data.numOfItems++;
        r.data.sheild++;
    }

    // #30 Wineskin appears on space 72
    [PunRPC]
    public void eventCard30()
    {
        print("this is event card 30");

        //place wineskin icon on region 72
        GameObject g = PhotonNetwork.Instantiate(Resources.Load<GameObject>("WineskinPrefab"));
        GameGraph.Instance.PlaceAt(g.gameObject, 72);

        //update region stats
        Region r = GameGraph.Instance.Find(72);
        r.data.numOfItems++;
        r.data.numWineskin++;
    }


    // #31 Any hero not on a forest space or in the mine, tavern or castle loses 2 WP  (shield)
    [PunRPC]
    public void eventCard31()
    {
        //  print("hero's WP was " + myhero.data.WP);
        useShieldOptionPanel.SetActive(true);
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {

            Hero hero = (Hero)players[i].GetHero();
            perviousWP[i] = hero.data.WP;
            if ((players[i].GetCurrentRegion().label >= 47 && players[i].GetCurrentRegion().label <= 63) ||
                (players[i].GetCurrentRegion().label >= 22 && players[i].GetCurrentRegion().label <= 25) ||
                (players[i].GetCurrentRegion().label == 71) ||
                (players[i].GetCurrentRegion().label == 72) ||
                (players[i].GetCurrentRegion().label == 0))
            {
                return;
            }
            else
            {
                if (hero.data.WP <= 2)
                {
                    hero.data.WP = 0;
                }
                else
                {
                    hero.data.WP = hero.data.WP - 2;
                }
            }
        }

    }

    // #32 Every hero whose time marker is in the sunrise box loses 2 WP.  (shield)
    [PunRPC]
    public void eventCard32()
    {
        //  print("hero's WP was " + myhero.data.WP);
        useShieldOptionPanel.SetActive(true);
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {

            Hero hero = (Hero)players[i].GetHero();
            perviousWP[i] = hero.data.WP;
            if (hero.data.NumHours == 0)
            {
                if (hero.data.WP <= 2)
                {
                    hero.data.WP = 0;
                }
                else
                {
                    hero.data.WP = hero.data.WP - 2;
                }
            }
        }

    }

    // If any user used the shield, then every players wp goes back to normal.
    public void usedShield(int eventNumber)
    {
        if(eventNumber == 0 || eventNumber == 1 || eventNumber == 6 || eventNumber == 12 || eventNumber == 13)
        {
            Player[] players = PhotonNetwork.PlayerList;
            for (int i = 0; i < players.Length; i++)
            {

                Hero hero = (Hero)players[i].GetHero();

                hero.data.WP = perviousWP[i];
            }
        }

        if(eventNumber == 7)
        {
            print("Entered the usedShield Function ");
            Instantiate(wellPrefab, GameGraph.Instance.Find(45).position, Quaternion.identity);
        }

        if(eventNumber == 3)
        {
            usedShieldFor3 = true;
            narrator.GetComponent<Card.Narrator>().updatedMonsters = false;
            foreach (MonsterMoveController monster in FindObjectsOfType<MonsterMoveController>())
            {
                if (monster.data.sp > 0)
                {
                    monster.data.sp -= 1;
                }
            }
        }

        Player[] player = PhotonNetwork.PlayerList;
        for (int i = 0; i < player.Length; i++)
        {
            Hero h = (Hero)player[i].GetHero();
            Debug.Log("used shields" + h.type + " " + h.data.SP + " " + h.data.WP + " " + h.data.gold);

        }
    }

}
