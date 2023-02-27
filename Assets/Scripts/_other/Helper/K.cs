using Newtonsoft.Json.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;

/// <summary>
/// Local preferences
/// </summary>
public static class Preferences
{
    public static string USERNAME = "username";
    public static string PASSWORD = "password";
}

    
public static class P
{
    public static class K
    {
        public static readonly string isReady = "isReady";
        public static readonly string hero = "hero";
        public static readonly string isFight = "isFight";
        public static readonly string isAsked = "isAsked";
    }

    public static Hero GetHero(this Player p)
    {
        return (Hero)p.CustomProperties[K.hero];
    }

    public static Player SetHero(this Player p, Hero hero)
    {
        p.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            {K.hero, hero }
        });
        return p;
    }

    public static bool IsReady(this Player p)
    {
        return (bool)(p.CustomProperties[K.isReady] ?? false);
    }

    public static Player SetReady(this Player p, bool ready)
    {
        p.SetCustomProperties(new ExitGames.Client.Photon.Hashtable
        {
            {K.isReady, ready }
        });
        return p;
    }

    public static void Reset(this Player p)
    {
        p.CustomProperties = new ExitGames.Client.Photon.Hashtable();
    }

    public static Region GetCurrentRegion(this Player player)
    {
        foreach (HeroMoveController heroMoveController in UnityEngine.Object.FindObjectsOfType<HeroMoveController>())
        {
            if (heroMoveController.photonView.Owner == player)
            {
                return GameGraph.Instance.FindNearest(heroMoveController.transform.position);
            }
        }
        throw new Exception("Not found");
    }

    public static Region GetCurrentRegion(this Hero hero)
    {
        foreach(var p in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if(p.GetHero().type == hero.type)
            {
                return GetCurrentRegion(p);
            }
        }
        throw new Exception("Not found");
    }

    public static bool HasConsumedHour(this Player player)
    {
        return player.GetHero().data.HoursConsumed > 0;
    }
}

public static class Room
{
    private static class K
    {
        public static readonly string difficulty = "difficulty";
        public static readonly string json = "json";
    }   

    public static bool IsSaved
    {
        get
        {
            return Json != null;
        }
    }

    public static Difficulty Difficulty
    {
        get
        {
            return (Difficulty)(PhotonNetwork.CurrentRoom.CustomProperties[K.difficulty]??Difficulty.Easy);
        }
    }

    public static void SetDifficulty(this Photon.Realtime.Room r, Difficulty difficulty)
    {
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable()
        {
            {K.difficulty, (int)difficulty}
        });
    }

    public static JObject Json
    {
        get
        {
            return !(PhotonNetwork.CurrentRoom?.CustomProperties[K.json] is string s) ? null : JObject.Parse(s);
        }
    }
}


public enum Difficulty
{
    Easy, Normal
}

