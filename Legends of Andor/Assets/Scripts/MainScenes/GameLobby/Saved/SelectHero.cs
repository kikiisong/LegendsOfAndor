using ExitGames.Client.Photon;
using Newtonsoft.Json.Linq;
using Photon.Pun;
using Photon.Realtime;
using Saving;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectHero : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI text;

    Hero hero;

    bool IsSelected{
        get
        {
            foreach(Player player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                if(player.GetHero()?.type == hero.type)
                {
                    return true;
                }
            }
            return false;
        }
    }

    bool DidISelect
    {
        get
        {
            return PhotonNetwork.LocalPlayer.GetHero() != null;
        }
    }

    public void InitRPC(JToken jToken)
    {
        photonView.RPC("Init", RpcTarget.AllBuffered, jToken.ToString());
    }

    [PunRPC]
    public void Init(string json)
    {
        Hero hero = J.ToHero(JObject.Parse(json));
        this.hero = hero;
        GetComponent<Image>().sprite = hero.ui.GetSprite();
    }

    public void Click_Select()
    {
        if (DidISelect)
        {
            PhotonNetwork.LocalPlayer.SetHero(null);
        }

        if (!IsSelected)
        {
            PhotonNetwork.LocalPlayer.SetHero(hero);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (IsSelected)
        {
            text.text = "Taken";
        }
        else
        {
            text.text = "Select";
        }
    }
}
