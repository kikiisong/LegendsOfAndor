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


    // store the current region of this player's hero
    Region currentRegion;

    // store my current hero
    Hero myhero;

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
     //   print("hero's WP was " + myhero.data.WP);
        currentRegion = findCurrentRegion();
        if(currentRegion.label >= 0 && currentRegion.label <= 20)
        {
            if (myhero.data.WP <= 2)
            {
                myhero.data.WP = 0;
            }
            else
            {
                myhero.data.WP = myhero.data.WP - 3;
            }
        }
    //    print("hero's WP is " + myhero.data.WP);
    }

    // #4 Any hero standing on a space between 37 and 70 looses 3 WP  (shiled)
    [PunRPC]
    public void eventCard4()
    {
      //  print("hero's WP was " + myhero.data.WP);

        currentRegion = findCurrentRegion();
        if (currentRegion.label >= 37 && currentRegion.label <= 70)
        {
            if (myhero.data.WP <= 2)
            {
                myhero.data.WP = 0;
            }
            else
            {
                myhero.data.WP = myhero.data.WP - 3;
            }
        }

      //  print("hero's WP is " + myhero.data.WP);
    }

    // #5 Wizard and Archer get 3 WP
    [PunRPC]
    public void eventCard5()
    {
      //  print("hero's WP was " + myhero.data.WP);

        if (myhero.type == Hero.Type.WIZARD || myhero.type == Hero.Type.ARCHER)
        {
            myhero.data.WP += 3;
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

        if (myhero.data.WP < 10)
        {
            myhero.data.WP = 10;
        }


      //  print("hero's WP is " + myhero.data.WP);
    }

    // #14 Dwarf and Warrior receive 3 WP
    [PunRPC]
    public void eventCard14()
    {
      //  print("hero's WP was " + myhero.data.WP);

        if (myhero.type == Hero.Type.DWARF || myhero.type == Hero.Type.WARRIOR)
        {
            myhero.data.WP += 3;
        }

      //  print("hero's WP is " + myhero.data.WP);
    }

    // #17 Every hero with more than 12 WP goes down to 12 WP  (shiled)
    [PunRPC]
    public void eventCard17()
    {
      //  print("hero's WP was " + myhero.data.WP);

        if (myhero.data.WP > 12)
        {
            myhero.data.WP = 12;
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

        currentRegion = findCurrentRegion();
        if ((currentRegion.label >= 47 && currentRegion.label <= 63)||
            (currentRegion.label >= 22 && currentRegion.label <= 25) ||
            (currentRegion.label == 71) ||
            (currentRegion.label == 72) ||
            (currentRegion.label == 0))
        {
            return;
        }
        else
        {
            if(myhero.data.WP <= 2)
            {
                myhero.data.WP = 0;
            }
            else
            {
                myhero.data.WP -= 2;
            }
        }

      //  print("hero's WP is " + myhero.data.WP);
    }

    // #28 Every hero whose time marker is in the sunrise box gains 2 WP
    [PunRPC]
    public void eventCard28()
    {
      //  print("hero's WP was " + myhero.data.WP);

        if (myhero.data.numHours == 0)
        {
            myhero.data.WP += 2;
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

        currentRegion = findCurrentRegion();
        if ((currentRegion.label >= 47 && currentRegion.label <= 63) ||
            (currentRegion.label >= 22 && currentRegion.label <= 25) ||
            (currentRegion.label == 71) ||
            (currentRegion.label == 72) ||
            (currentRegion.label == 0))
        {
            return;
        }
        else
        {
            if (myhero.data.WP <= 2)
            {
                myhero.data.WP = 0;
            }
            else
            {
                myhero.data.WP -= 2;
            }
        }

      //  print("hero's WP is " + myhero.data.WP);
    }

    // #32 Every hero whose time marker is in the sunrise box loses 2 WP.  (shiled)
    [PunRPC]
    public void eventCard32()
    {
       //  print("hero's WP was " + myhero.data.WP);

        if (myhero.data.WP <= 2)
        {
            myhero.data.WP = 0;
        }
        else
        {
            myhero.data.WP -= 2;
        }

       // print("hero's WP is " + myhero.data.WP);
    }

}
