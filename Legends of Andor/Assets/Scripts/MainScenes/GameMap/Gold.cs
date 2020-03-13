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
    [SerializeField] TextMeshProUGUI textUI;

    public int Amount
    {
        get;
        private set;
    } = 0;
  
    public void Increment()
    {
        photonView.RequestOwnership();
        Amount++;
        Display();
    }

    public void Decrement()
    {
        photonView.RequestOwnership();
        Amount--;
        Display();
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
