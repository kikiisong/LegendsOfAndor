using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Fog : MonoBehaviourPun
{
    public int region;
    public FogType type;
    public EventCardController myEvents;
    public Renderer fogIcon;

    // Start is called before the first frame update
    void Start()
    {
        fogIcon = GetComponent<Renderer>();
        type = FogType.SP;
        GameGraph.Instance.PlaceAt(gameObject, region);
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    /*public void uncover()
    {
        if (photonView.IsMine)
        {
            if (type == FogType.SP)//SP+1
            {
                photonView.RPC("Encounter", RpcTarget.AllBuffered, currentRegion.label);
                Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();
                hero.data.SP += 1;
            }
            else if (type == FogType.TwoWP)//WP+2
            {
                Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();
                hero.data.WP += 2;
            }
            else if (type == FogType.ThreeWP)//WP+3
            {
                Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();
                hero.data.WP += 3;
            }
            else if (type == FogType.Gold)//Gold
            {
                Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();
                hero.data.gold += 1;
            }
            else if (type == FogType.Event)//Event
            {
                myEvents.flipped();
            }
            else if (type == FogType.Wineskin)//Wineskin
            {

            }
            else if (type == FogType.Witch)//Witch
            {
                PhotonNetwork.Instantiate("Witch", transform.position, transform.rotation);
            }
            else if (type == FogType.Monster)//Gor
            {
                PhotonNetwork.Instantiate("Gor", transform.position, transform.rotation);
            }
            fogIcon.enabled = false;
        }
    }*/

    

}
