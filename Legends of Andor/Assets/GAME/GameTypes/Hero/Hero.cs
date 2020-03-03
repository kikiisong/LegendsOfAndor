using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hero")]
public class Hero : ScriptableObject
{
    public Type type;
    public Data data;
    public Constants constants;
    public UI ui;

    //Easy to serialize and sync accross all players
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

        public int numHours; //0 or 1?
    }
    
    //Values that won't change
    [System.Serializable]
    public struct Constants
    {
        public int StartingRegion;
        //name, description, ...
    }

    [System.Serializable]
    public class UI
    {
        private bool gender; // how to implement gender?

        public Sprite female;
        public Sprite male;

        public Sprite GetSprite()
        {
            return gender ? male : female;
        }

        public void ToggleGender()
        {
            gender = !gender;
        }

    }


    public enum Type
    {
        ARCHER, WARRIOR, WIZARD, DWARF
    }
}
