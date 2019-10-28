using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaiseEventExample : MonoBehaviourPun
{
    public Image image;

    private const byte COLOR_CHANGE_EVENT = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if(obj.Code == COLOR_CHANGE_EVENT)
        {
            object[] data = (object[]) obj.CustomData;
            float r = (float) data[0];
            float g = (float)data[1];
            float b = (float)data[2];

            image.color = new Color(r, g, b, 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        image.color = new Color(r, g, b, 1f);

        object[] data = new object[] { r, g, b };
        PhotonNetwork.RaiseEvent(COLOR_CHANGE_EVENT, data, RaiseEventOptions.Default, SendOptions.SendReliable);
    }
}
