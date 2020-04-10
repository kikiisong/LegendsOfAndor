using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class useShield : MonoBehaviourPun
{
    public GameObject eventCardManager;
    public GameObject sorryNoticePanel;

    public void usedShieldAndClosePanel()
    {
        var p = PhotonNetwork.LocalPlayer;
        Hero myhero = PhotonNetwork.LocalPlayer.GetHero();
        int currentEventIndex = eventCardManager.GetComponent<EventCardController>().currentEventIndex;
        if (myhero.HasShield())
        {
            photonView.RPC("decreaseShiled", RpcTarget.AllBuffered, currentEventIndex);
            myhero.data.shield -= 1;
        }
        else
        {
            sorryNoticePanel.SetActive(true);
        }
    }

    [PunRPC]
    public void decreaseShiled(int currentEventIndex)
    {
        eventCardManager.GetComponent<EventCardController>().usedShield(currentEventIndex);
    }
}
