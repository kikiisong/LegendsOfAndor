using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ResourceDistributeUI : MonoBehaviourPun
{
    public TextMeshProUGUI text;
    public int amount;

    public Resource resource;


    // Start is called before the first frame update
    void Start()
    {
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
        Display();
    }
}
