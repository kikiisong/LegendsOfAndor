using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using System;

public class Gold : MonoBehaviour, IPunObservable   
{
    public TextMeshProUGUI text;
    public int goldValue;
    public int GV
    { 
        get { return goldValue; }
        set
        {
            goldValue = value;
        }

    }

  
    public void increment()
    {
        goldValue++;
    }
    public void decrement()
    {
        goldValue--;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(goldValue);
        }
        else
        {
            int i = (int)stream.ReceiveNext();
            goldValue = i;
            Display();
        }
    }

    private void Display()
    {
        text.text = "" + goldValue;
    }
}
