using Newtonsoft.Json.Linq;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keep track of all the keys used.
/// </summary>
public static class K
{
    /// <summary>
    /// Local preferences
    /// </summary>
    public static class Preferences
    {
        public static string USERNAME = "username";
        public static string PASSWORD = "password";
    }

    /// <summary>
    /// Photon player custom properties
    /// </summary>
    public static class Player
    {
        public static readonly string isReady = "isReady"; //to remove
        public static readonly string isFight = "isFight";
        public static readonly string isAsked = "isAsked";
        public static readonly string hero = "currentHero";
    }

    /// <summary>
    /// Photon room custom preperties
    /// </summary>
    public static class Room
    {
        public static readonly string difficulty = "difficulty";
        public static readonly string json = "json";
    }
}

public static class Room{

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
            return (Difficulty)PhotonNetwork.CurrentRoom.CustomProperties[K.Room.difficulty];
        }
    }

    public static JObject Json
    {
        get
        {
            return !(PhotonNetwork.CurrentRoom?.CustomProperties[K.Room.json] is string s) ? null : JObject.Parse(s);
        }
    }
   
}

public enum Difficulty
{
    Easy, Normal
}

