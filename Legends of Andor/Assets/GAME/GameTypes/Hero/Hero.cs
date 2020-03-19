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
        public int numWineskin;

        public int regionNumber;
        // number of carried farmers

        public int numFarmers; // 0 or 1 or 2

        public bool magic, herbS, brew, helm, sheild, herbW, bow;
        public int times;
        public int btimes;
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
}
