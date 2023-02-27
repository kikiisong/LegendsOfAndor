using Bag;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyAssets/Hero")]
public class Hero : ScriptableObject
{
    public Type type;
    public Data data;
    public Constants constants;
    public UI ui;

    //Easy to serialize and sync accross all players
    [Serializable]
    public struct Data
    { 
        //Main
        public int WP;
        public int SP;
        [JsonProperty] public int NumHours { get; private set; }
        [JsonProperty] public int HoursConsumed { get; private set; }

        public void ConsumeHour()
        {
            NumHours++;
            HoursConsumed++;
        }

        public void ResetNumHours()
        {
            NumHours = 0;
            HoursConsumed = 0;
        }

        public void ResetHoursConsumed()
        {
            HoursConsumed = 0;
        }

        public int gold;
        public int numFarmers; // 0 or 1 or 2

        //Small item
        public int wineskin;
        public int brew;
        public int telescope;
        public int helm;

        //Big item
        public int shield;
        public int bow; //0 or 1
        public int falcon;
        public bool usedFalcon; // true if used today, flase otherwise
        //Other
        public int herb;

        //Fight related 
        public int times;
        public int btimes;
        public int attackNum;
        public bool finishedFight;
        //public Dice dice;
        public int diceNum;
        public int damage;
        public int blackDice;
        public int rollResult;

        public bool useShiled;

        //Helper
        public int wineskinStacked;
        [JsonIgnore] public int NumHoursEffective
        {
            get
            {
                return NumHours - wineskinStacked;
            }
        }
    }

    //Values that won't change
    [Serializable]
    public struct Constants
    {
        public int rank;
        //name, description, ...
    }

    [Serializable]
    public class UI
    {
        public bool gender;

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
    
    //Fight
    public int GetDiceNum()
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
            case (Type.WIZARD): {
                    return 1;
                }
            default:
                throw new Exception();
        }
    }

    public void Attacked(int damage)
    {
        data.WP -= damage;
    }

    

    //Static
    public static Hero FindInResources(Type type)
    {
        Resources.LoadAll<Hero>("Hero_SO");
        foreach (Hero hero in Resources.FindObjectsOfTypeAll<Hero>())
        {
            if (hero.type == type) return UnityEngine.Object.Instantiate(hero);
        }
        throw new Exception("Hero not found in Resources");
    }

    public static List<Hero> FindAllInResources()
    {
        List<Hero> heroes = new List<Hero>();
        foreach (Type type in Enum.GetValues(typeof(Type)))
        {
            heroes.Add(FindInResources(type));
        }
        return heroes;
    }
}

public static class FightHelper
{
    public static bool GetMagic(this Hero h)
    {
        return h.type == Hero.Type.WIZARD;
    }
    
    public static bool HasWineSkin(this Hero h)
    {
        return h.data.wineskin > 0;
    }

    public static bool HasBrew(this Hero h)
    {
        return h.data.brew > 0;
    }

    public static bool HasHerb(this Hero h)
    {
        return h.data.herb > 0;
    }

    public static bool HasShield(this Hero h)
    {
        return h.data.shield > 0;
    }

    public static bool HasHelm(this Hero h)
    {
        return h.data.helm > 0;
    }
}

