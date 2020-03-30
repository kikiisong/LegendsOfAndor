using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class EventCardController : MonoBehaviour
{
    public List<int> cards = new List<int>(34);
    private static System.Random rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        
        
        //if masterClient, create a singleton of a randomized list of eventcards
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 1; i < 35; i++)
            {
                cards[i - 1] = i;
            }
            Shuffle(cards);
            //then call RPCPun to other clients
            //photonView.RPC("getCards", RpcTarget.AllBuffered, cards);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //imitate when a card is flipped
    void flipped()
    {
        //TODO
    }

    void Shuffle(List<int> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            int temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
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
