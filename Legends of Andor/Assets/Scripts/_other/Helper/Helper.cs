using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static int Mod(int x, int n)
    {
        int r = x % n;
        return r < 0 ? r + n : r;
    }

    public static float Constrain(float f, float min, float max)
    {
        if (f > min && f < max) return f;
        else if (f <= min) return min;
        else return max;
    }

    public static Hero FindInResources(Hero.Type type)
    {
        Resources.LoadAll<Hero>("Hero_SO");
        foreach (Hero hero in Resources.FindObjectsOfTypeAll<Hero>())
        {
            if (hero.type == type) return Hero.Instantiate(hero);
        }
        throw new Exception("Hero not found in Resources");
    }

    public static List<Hero> FindAllInResources()
    {
        List<Hero> heroes = new List<Hero>();
        foreach (Hero.Type type in Enum.GetValues(typeof(Hero.Type)))
        {
            heroes.Add(FindInResources(type));
        }
        return heroes;
    }
}
