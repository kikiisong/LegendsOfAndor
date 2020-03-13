using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using System;

public class Gold : MonoBehaviourPun, IPunObservable   
{
    public TextMeshProUGUI textUI;
    public int Amount
    {
        get;
        private set;
    } = 0;
  
    public void Increment()
    {
        Amount++;
    }

    public void Decrement()
    {
        Amount--;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Amount);
        }
        else
        {
            int i = (int)stream.ReceiveNext();
            Amount = i;
            Display();
        }
    }

    private void Display()
    {
        textUI.text = "" + Amount;
    }
}
