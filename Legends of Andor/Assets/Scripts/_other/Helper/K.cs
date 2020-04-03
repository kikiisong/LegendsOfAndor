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
        public static readonly string hero = "currentHero";
    }

    /// <summary>
    /// Photon room custom preperties
    /// </summary>
    public static class Room
    {
        public static readonly string difficulty = "difficulty";
    }
}

public static class Room{

    public static Difficulty Difficulty
    {
        get
        {
            Enum.TryParse((string)PhotonNetwork.CurrentRoom.CustomProperties[K.Room.difficulty], out Difficulty result);
            return result;
        }
    }
   
}

public enum Difficulty
{
    Easy, Normal
}

