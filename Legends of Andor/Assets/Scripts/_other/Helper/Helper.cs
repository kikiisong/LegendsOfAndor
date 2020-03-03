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
}
