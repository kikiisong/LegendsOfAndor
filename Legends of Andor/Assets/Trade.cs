using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviourPun
{
    public GameObject slot;
    public GameObject otherPanel;

    public int bagType;
    public int slotID;

    private byte TRADEITEM = 1;


    public void OnMouseDown()
    {
         
        GameObject image = slot.transform.GetChild(0).gameObject;
        Image img = image.gameObject.GetComponent<Image>();
        string n = img.sprite.name;

        Debug.Log(slot.name);
   
        if (n != "UIMask")
        { 
            object[] content = new object[] { n, bagType, slotID};
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(TRADEITEM, content, raiseEventOptions, sendOptions);
        }
    }
}
