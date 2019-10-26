using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    private Player player;
    public void SetPlayerInfo(Player player)
    {
        this.player = player;
        _text.text = player.NickName;
    }
}
