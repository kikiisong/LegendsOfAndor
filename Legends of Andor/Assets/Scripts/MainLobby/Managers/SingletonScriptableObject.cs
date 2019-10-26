using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScriptableObject<T> : ScriptableObject where T:ScriptableObject
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if(results.Length == 0)
                {
                    Debug.Log("SingletonScriptableObject -> Instance -> results length is 0 for " + typeof(T).ToString() + ".");
                    return null;
                }
                else if(results.Length > 1)
                {
                    Debug.Log("SingletonScriptableObject -> Instance -> results length is greater than 1 for " + typeof(T).ToString() + ".");
                    return null;
                
                }
                Debug.Log("Set SingletonScriptableObject");
                _instance = results[0];
            }
            return _instance;
        }
    }
}
