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

        // number of carried farmers
        public int numFarmers; // 0 or 1 or 2

<<<<<<< Updated upstream
        public int gold;
=======
        //Fight related 
        public int times;
        public int btimes;
        public int attackNum;
        public bool finishedFight;

        //TODO:somebody handle one can maximize have one item
        //TODO:prob who do trade

        //object related?
        //small item
        public int numWineskin; //did
        public int brew;
            //TODO:state full half empty..

        //number of herb can be used in two way
        public int herb;

        //big item
        public int sheild;
        public int helm;
            //2 means full, 1 means half, 0 means nothing
        public int bow;
        public int falcon;
        //1 means processes and 0 means does not process
        //TODO: more
        public Dice dice;
        public int diceNum;
        public int damage;

>>>>>>> Stashed changes
    }
    
    //Values that won't change
    [System.Serializable]
    public struct Constants
    {
        public int StartingRegion;
        public int rank;
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
