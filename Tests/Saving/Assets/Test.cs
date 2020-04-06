using Newtonsoft.Json.Linq;
using SavingSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif  
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("currentPath:" + Directory.GetCurrentDirectory());
        print("consoleLogPath: " + Application.consoleLogPath);
        print("dataPath: " + Application.dataPath);
        print("persistentDataPath: " + Application.persistentDataPath);
        print("streamingAssetsPath: " + Application.streamingAssetsPath);
        print("temporaryCachePath: " + Application.temporaryCachePath);
        SceneManager.LoadScene(1);
    }

    

}
