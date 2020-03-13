using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System;

public class ResourceDistributeUI : MonoBehaviourPun
{
    public TextMeshProUGUI text;

    int amount;
    int HeroAmount
    {
        get
        {
            Hero hero = (Hero)photonView.Owner.CustomProperties[K.Player.hero];
            switch (resource.type)
            {
                case Resource.Type.GoldCoint:
                    return hero.data.gold;
                case Resource.Type.Wineskin:
                    return hero.data.numWineskin;
                default:
                    throw new Exception("No such resource");
            }
        }
        set
        {
            Hero hero = (Hero)photonView.Owner.CustomProperties[K.Player.hero];
            switch (resource.type)
            {
                case Resource.Type.GoldCoint:
                    hero.data.gold = value;
                    break;
                case Resource.Type.Wineskin:
                    hero.data.numWineskin = value;
                    break;
                default:
                    throw new Exception("No such resource");
            }
        }
    }

    public Resource resource;


    // Start is called before the first frame update
    void Start()
    {
        amount = HeroAmount;
        Display();
    }

    void Display()
    {
        text.text = amount.ToString();
    }

    public void Click_Plus()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("Plus", RpcTarget.All);
        }
    }

    [PunRPC]
    public void Plus()
    {
        resource.Take(ref amount);
        HeroAmount = amount;
        Display();
    }

    public void Click_Minus()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("Minus", RpcTarget.All);
        }
    }

    [PunRPC]
    public void Minus()
    {
        resource.GiveBack(ref amount);
        HeroAmount = amount;
        Display();
    }
}
