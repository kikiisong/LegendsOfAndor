using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hero")]
public class Hero : ScriptableObject
{
    public Data data;

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
}
