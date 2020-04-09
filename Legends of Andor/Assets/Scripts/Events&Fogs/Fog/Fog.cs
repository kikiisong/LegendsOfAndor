using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Fog : MonoBehaviourPun
{
    public int region;
    public int type;
    public EventCardController myEvents;

    // Start is called before the first frame update
    void Start()
    {
        GameGraph.Instance.PlaceAt(gameObject, region);
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void uncover()
    {
        if(type == 1)//SP+1
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();
            hero.data.SP += 1;
        }else if(type == 2)//WP+2
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();
            hero.data.WP += 2;
        }
        else if (type == 3)//WP+2
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();
            hero.data.WP += 3;
        }
        else if (type == 4)//Gold
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();
            hero.data.gold += 1;
        }
        else if (type == 5)//Event
        {
            myEvents.flipped();
        }
        else if (type == 6)//Wineskine
        {
            
        }
        else if (type == 7)//Witch
        {
            PhotonNetwork.Instantiate("Witch", transform.position, transform.rotation);
        }
        else if(type ==8)//Gor
        {
            PhotonNetwork.Instantiate("Gor", transform.position, transform.rotation);
        }
    }

}
