using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class useShield : MonoBehaviourPun
{
    public GameObject eventCardManager;
    public GameObject sorryNoticePanel;

    public void usedShieldAndClosePanel()
    {
        Hero myhero = PhotonNetwork.LocalPlayer.GetHero();
        int currentEventIndex = eventCardManager.GetComponent<EventCardController>().currentEventIndex;
        if (myhero.HasShield())
        {
            photonView.RPC("decreaseShield", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer ,currentEventIndex);
        }
        else
        {
            sorryNoticePanel.SetActive(true);
        }
    }

    [PunRPC]
    public void decreaseShield(Player player, int currentEventIndex)
    {
        eventCardManager.GetComponent<EventCardController>().useShieldOptionPanel.SetActive(false);
        Hero hero = player.GetHero();
        hero.data.shield -= 1;
        eventCardManager.GetComponent<EventCardController>().usedShield(currentEventIndex);
    }
}
