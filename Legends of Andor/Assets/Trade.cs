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
    //public TradeManager i;
    public int bagType;
    public int slotID;

    private byte TRADEITEM = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {


        GameObject image = slot.transform.GetChild(0).gameObject;
        Image img = image.gameObject.GetComponent<Image>();
        String n = img.sprite.name;

        Debug.Log(slot.name);
   
        if (n != "UIMask")
        { 
            object[] content = new object[] { n, bagType, slotID};
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
            SendOptions sendOptions = new SendOptions { Reliability = true };
            PhotonNetwork.RaiseEvent(TRADEITEM, content, raiseEventOptions, sendOptions);
        }

        //decrItem();
    }

    //private void decrItem()
    //{
    //    GameObject image = slot.transform.GetChild(0).gameObject;
    //    Image img = image.gameObject.GetComponent<Image>();

    //    GameObject text = slot.transform.GetChild(1).gameObject;
    //    Text tx = text.gameObject.GetComponent<Text>();

    //    Sprite uimask = Resources.Load<Sprite>("UIMask");

    //    // usure that clicking on empty icon won't do anything
    //    if (img.sprite.name != uimask.name)
    //    {

    //        if (tx.text == "")
    //        {
    //            img.sprite = uimask;
    //        }
    //        else
    //        { 
    //            int count = int.Parse(tx.text);
    //            count--;
      
    //            //empty slot if dropped item
    //            if (count != 0)
    //            {
    //                tx.text = (count).ToString();
    //            }
    //            else
    //            {
    //                img.sprite = uimask;
    //                tx.text = "";
    //            }
    //        }
    //    }
    //}
   
}
