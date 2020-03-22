using System;
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
        public int blackDice;
        /// <summary>
        /// Strength points
        /// </summary>
        public int SP;

        public int numHours;

        public int gold;
        

        public int regionNumber;
        // number of carried farmers

        public int numFarmers; // 0 or 1 or 2

        //Fight related 
        public int times;
        public int btimes;
        public int attackNum;
        public bool finishedFight;

        //TODO:somebody handle one can maximize have one item
        //TODO:prob who do trade

        //object related?
        //small item
        public int numWineskin;
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
        public Material color;

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

    public static Hero FindInResources(Type type)
    {
        Resources.LoadAll<Hero>("Hero_SO");
        foreach (Hero hero in Resources.FindObjectsOfTypeAll<Hero>())
        {
            Debug.Log(hero.type);
            if (hero.type == type) return Instantiate(hero);
        }
        throw new Exception("Hero not found in Resources");
    }

    public static List<Hero> FindAllInResources()
    {
        List<Hero> heroes = new List<Hero>();
        foreach(Type type in Enum.GetValues(typeof(Type))){
            heroes.Add(FindInResources(type));
        }
        return heroes;
    }
    
    public int getDiceNum()
    {
        switch (type)
        {
            case (Type.ARCHER):
                if (data.WP > 13)
                {
                    return 5;
                }
                else if (data.WP > 6)
                {
                    return 4;
                }
                else
                {
                    return 3;
                }
            case (Type.DWARF):
                if (data.WP > 13)
                {
                    return 3;
                }
                else if (data.WP > 6)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            case (Type.WARRIOR):
                if (data.WP > 13)
                {
                    return 4;
                }
                else if (data.WP > 6)
                {
                    return 3;
                }
                else
                {
                    return 2;
                }
            default:
                return 1;
        }
    }


    public void Attacked(int damage)
    {
        data.WP -= damage;
    }

    public bool getMagic() {
        if (type == Type.WIZARD) return true;
        return false;
    }

    public bool getWineSkin()
    {
        if (data.numWineskin > 0) return true;
        return false;
    }

    public bool getBrew()
    {
        if (data.brew > 0) return true;
        return false;
    }

    public bool getherb()
    {
        if (data.herb > 0) return true;
        return false;
    }

    public bool getSheild()
    {
        if (data.sheild > 0) return true;
        return false;
    }

    public bool getHelm()
    {
        if (data.helm > 0) return true;
        return false;
    }

    public bool getBow()
    {
        if (data.bow > 0) return true;
        return false;
    }

    public void heroRoll()
    {

        if (type == Hero.Type.ARCHER)
        {
            if (data.btimes > 0)
            {
                data.diceNum = data.dice.getOne(true);
                data.btimes -= 1;
            }
            else if (data.times > 0)
            {
                data.diceNum = data.dice.getOne(false);
                data.times -= 1;
            }
        }
        else
        {
            data.dice.rollDice(getDiceNum(), data.blackDice);
            data.diceNum = data.dice.getMax();
        }
    }

}
