using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keep track of all the keys used.
/// </summary>
public class K
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
        public static readonly string isReady = "isReady";
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
