//using Newtonsoft.Json.Linq;
using Saving;
using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif  
using UnityEngine;


public class Test : MonoBehaviour
{
    public Test test;
    public int a;
    // Start is called before the first frame update
    void Start()
    {
        print("consoleLogPath: " + Application.consoleLogPath);
        print("dataPath: " + Application.dataPath);
        print("persistentDataPath: " + Application.persistentDataPath);
        print("streamingAssetsPath: " + Application.streamingAssetsPath);
        print("temporaryCachePath: " + Application.temporaryCachePath);
        WriteFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void WriteFile()
    {
        using (var sw = Helper.Create("test"))
        {
            sw.WriteLine("Hello world");
            sw.WriteLine("Bye world");
            sw.WriteLine(DateTime.Now.ToString());
            sw.WriteLine(JsonUtility.ToJson(this));
        }

        #if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif

        //JObject jObject = new JObject();
        //JToken.FromObject(new List<> );
    }

}
