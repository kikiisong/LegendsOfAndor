using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hero")]
public class Hero : ScriptableObject
{
    public Data data;
    public Constants constants;

    [System.Serializable]
    public struct Data
    {
        /// <summary>
        /// Willpower points
        /// </summary>
        public int WP;

        /// <summary>
        /// Strength points
        /// </summary>
        public int SP;
    }
    
    [System.Serializable]
    public struct Constants
    {
        public int StartingRegion;
        //name, description, ...
    }
    
}
