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
        public int NumHours { get; private set; }
        public int HoursConsumed { get; private set; }

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

        //Other
        public int herb;

        //Fight related 
        public int times;
        public int btimes;
        public int attackNum;
        public bool finishedFight;
        public Dice dice;
        public int diceNum;
        public int damage;
        public int blackDice;

        //Helper
        public int wineskinStacked;
        public int NumHoursEffective
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
            default:
                throw new Exception();
        }
    }

    public void Attacked(int damage)
    {
        data.WP -= damage;
    }

    public void HeroRoll()
    {
        if (type == Type.ARCHER)
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
        //TODO: did not consider how black dice is used
        else if(data.times>0)
        {
            data.dice.rollDice(GetDiceNum(), data.blackDice);
            data.diceNum = data.dice.getMax();
            data.times = 0;
        }
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

