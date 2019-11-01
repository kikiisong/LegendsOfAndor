using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private Text text;

    public Player Player;
    public void SetPlayerInfo(Player player)
    {
        Player = player;
        text.text = player.NickName;
        P.OnKey<bool>(player, "isReady", val =>
        {
            if (val)
            {
                print(text.text);
                text.text += " (Ready)";
                print(text.text);
            }
        });
    }
}

public class P
{
    public static bool OnKey<T>(Player player, string key, Action<T> callback)
    {
        if (player.CustomProperties.ContainsKey(key))
        {
            T value = (T)player.CustomProperties[key];
            callback(value);
        }

        return player.CustomProperties.ContainsKey(false);
    }
}
