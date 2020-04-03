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
}
