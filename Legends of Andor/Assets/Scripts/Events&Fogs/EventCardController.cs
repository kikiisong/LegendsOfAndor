using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class EventCardController : MonoBehaviourPun
{
    public List<int> cards = new List<int>(34);
    private static System.Random rand = new System.Random();

    public List<GameObject> evnetCardList;
    public int currentEventIndex;
    public GameObject currentEventCard;

    Region CurrentRegion
    {
        get
        {
            return PhotonNetwork.LocalPlayer.GetCurrentRegion();
        }
    }

    // store my current hero
    Hero Hero
    {
        get
        {
            return PhotonNetwork.LocalPlayer.GetHero();
        }
    }

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
    }

    public void newEventCard(int a)
    {
        currentEventIndex = a;
        currentEventCard = evnetCardList[a];
        myEventCardButton.GetComponent<eventCardButton>().setEventCard(currentEventCard);
        currentEventCard.SetActive(true);

        if(a == 0)
        {
            photonView.RPC("eventCard2", RpcTarget.AllBuffered);
        }
        else if(a == 1)
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


    // #2 Any hero standing on a space between 0 and 20 looses 3 WP (shiled)
    [PunRPC]
    public void eventCard2()
    {
     //   print("hero's WP was " + myhero.data.WP);
        if(CurrentRegion.label >= 0 && CurrentRegion.label <= 20)
        {
            if (Hero.data.WP <= 2)
            {
                Hero.data.WP = 0;
            }
            else
            {
                Hero.data.WP = Hero.data.WP - 3;
            }
        }
    //    print("hero's WP is " + myhero.data.WP);
    }

    // #4 Any hero standing on a space between 37 and 70 looses 3 WP  (shiled)
    [PunRPC]
    public void eventCard4()
    {
      //  print("hero's WP was " + myhero.data.WP);

        if (CurrentRegion.label >= 37 && CurrentRegion.label <= 70)
        {
            if (Hero.data.WP <= 2)
            {
                Hero.data.WP = 0;
            }
            else
            {
                Hero.data.WP = Hero.data.WP - 3;
            }
        }

      //  print("hero's WP is " + myhero.data.WP);
    }

    // #5 Wizard and Archer get 3 WP
    [PunRPC]
    public void eventCard5()
    {
      //  print("hero's WP was " + myhero.data.WP);

        if (Hero.type == Hero.Type.WIZARD || Hero.type == Hero.Type.ARCHER)
        {
            Hero.data.WP += 3;
        }

      //  print("hero's WP is " + myhero.data.WP);
    }

    // #11 On this day every creature has +1 SP  (shiled)
    [PunRPC]
    public void eventCard11()
    {
        // TODO do not know how to do this
        print("this is event card 11");
    }

    // #13 Every hero with WP < 10 increments WP to 10 
    [PunRPC]
    public void eventCard13()
    {
      //  print("hero's WP was " + myhero.data.WP);

        if (Hero.data.WP < 10)
        {
            Hero.data.WP = 10;
        }


      //  print("hero's WP is " + myhero.data.WP);
    }

    // #14 Dwarf and Warrior receive 3 WP
    [PunRPC]
    public void eventCard14()
    {
      //  print("hero's WP was " + myhero.data.WP);

        if (Hero.type == Hero.Type.DWARF || Hero.type == Hero.Type.WARRIOR)
        {
            Hero.data.WP += 3;
        }

      //  print("hero's WP is " + myhero.data.WP);
    }

    // #17 Every hero with more than 12 WP goes down to 12 WP  (shiled)
    [PunRPC]
    public void eventCard17()
    {
      //  print("hero's WP was " + myhero.data.WP);

        if (Hero.data.WP > 12)
        {
            Hero.data.WP = 12;
        }

      //  print("hero's WP is " + myhero.data.WP);
    }

    // #22 Well token on space 45 is removed from the game  (shiled)
    [PunRPC]
    public void eventCard22()
    {
        List<Well> well = GameGraph.Instance.FindObjectsOnRegion<Well>(GameGraph.Instance.Find(45));
        if (well.Count == 0)
        {
            return;
        }
        else
        {
            well[0].gameObject.SetActive(false);
        }
    }

    // #24 Any hero not on a forest space or in the mine, tavern or castle loses 2 WP 
    [PunRPC]
    public void eventCard24()
    {
      //  print("hero's WP was " + myhero.data.WP);

        if ((CurrentRegion.label >= 47 && CurrentRegion.label <= 63)||
            (CurrentRegion.label >= 22 && CurrentRegion.label <= 25) ||
            (CurrentRegion.label == 71) ||
            (CurrentRegion.label == 72) ||
            (CurrentRegion.label == 0))
        {
            return;
        }
        else
        {
            if(Hero.data.WP <= 2)
            {
                Hero.data.WP = 0;
            }
            else
            {
                Hero.data.WP -= 2;
            }
        }

      //  print("hero's WP is " + myhero.data.WP);
    }

    // #28 Every hero whose time marker is in the sunrise box gains 2 WP
    [PunRPC]
    public void eventCard28()
    {
      //  print("hero's WP was " + myhero.data.WP);

        if (Hero.data.numHours == 0)
        {
            Hero.data.WP += 2;
        }

     //   print("hero's WP is " + myhero.data.WP);
    }

    // #29 Shield appears on space 57
    [PunRPC]
    public void eventCard29()
    {
        print("this is event card 29");
    }

    // #30 Wineskin appears on space 72
    [PunRPC]
    public void eventCard30()
    {
        print("this is event card 30");
    }

    // #31 Any hero not on a forest space or in the mine, tavern or castle loses 2 WP  (shiled)
    [PunRPC]
    public void eventCard31()
    {
        //  print("hero's WP was " + myhero.data.WP);

        var label = CurrentRegion.label;
        if ((label >= 47 && label <= 63) ||
            (label >= 22 && label <= 25) ||
            (label == 71) ||
            (label == 72) ||
            (label == 0))
        {
            return;
        }
        else
        {
            if (Hero.data.WP <= 2)
            {
                Hero.data.WP = 0;
            }
            else
            {
                Hero.data.WP -= 2;
            }
        }

      //  print("hero's WP is " + myhero.data.WP);
    }

    // #32 Every hero whose time marker is in the sunrise box loses 2 WP.  (shiled)
    [PunRPC]
    public void eventCard32()
    {
       //  print("hero's WP was " + myhero.data.WP);

        if (Hero.data.WP <= 2)
        {
            Hero.data.WP = 0;
        }
        else
        {
            Hero.data.WP -= 2;
        }

       // print("hero's WP is " + myhero.data.WP);
    }

}
