using UnityEngine;
using UnityEditor;
using System;

public class Utils : ScriptableObject
{
    [MenuItem("Tools/MyTool/Do It in C#")]
    static void DoIt()
    {
        EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
    
    }

    public static bool Contains<E>(E[] array, E element)
    {
        return Array.IndexOf(array, element) != -1;
    }

    public static bool Contains<E>(E[] array, params E[] elements)
    {
        foreach(E element in elements)
        {
            if (!Contains<E>(array, element)) return false;
        }
        return true;
    }
}