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

    internal void flipped()
    {
        //TODO
    }

    public GameObject myEventCardButton;

    // Start is called before the first frame update
    void Start()
    {
        
        
        //if masterClient, create a singleton of a randomized list of eventcards
        //if (PhotonNetwork.IsMasterClient)
       // {
      //for (int i = 1; i < 35; i++)
      //{
      //   cards[i - 1] = i;
      //}
      //Shuffle(cards);
     // int[] orderofEvents = cards.ToArray();
            //then call RPCPun to other clients
           // photonView.RPC("getCards", RpcTarget.AllBuffered, cards);
      //  }

    }

    public void newEventCard(int a)
    {
        currentEventIndex = a;
        currentEventCard = evnetCardList[a];
        myEventCardButton.GetComponent<eventCardButton>().setEventCard(currentEventCard);
        currentEventCard.SetActive(true);
    }


    [PunRPC]
    public void getCards(List<int> c)
    {
        if(!PhotonNetwork.IsMasterClient)
        {
            int n = c.Count;
            for(int i =0; i<n;i++)
            {
                cards[i] = c[i];
            }
        }
    }


}
