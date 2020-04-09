using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SavingSystem
{
    public static class Helper
    {
        //Values
        public static readonly string FOLDER_NAME = "SavedGames";

        //Init
        static Helper()
        {
            Directory.CreateDirectory(Path_Directory);
        }


        //Helper get
        public static string Path
        {
            get
            {
                return Application.isEditor ? Application.dataPath : Application.persistentDataPath;
            }
        }

        public static string Path_Directory
        {
            get
            {
                return Path + "/" + FOLDER_NAME + "/";
            }
        }

        //Helper methods
        public static string GetPath(string file_name, string file_extension="json")
        {
            return Path_Directory + file_name + "." + file_extension;
        }

        public static JObject GetJson(string file_name, string file_extension = "json")
        {
            return JObject.Parse(File.ReadAllText(GetPath(file_name, file_extension: file_extension)));
        }
    }

    public static class Saving {

        public static void SaveGameState(string file_name)
        { 
            JObject jObject = new JObject(
                new JProperty("array", new JArray(
                    from gameObject in GameObject.FindGameObjectsWithTag("MyObject")
                    select new JObject
                    {
                        { "position", JObject.FromObject(gameObject.transform.position) },
                        { "name", gameObject.name }
                    })));
                        
            File.WriteAllText(Helper.GetPath(file_name), jObject.ToString());
            RefreshEditor();
        }

        private static void RefreshEditor()
        {
            #if UNITY_EDITOR
            AssetDatabase.Refresh();
            #endif
        }
    }
}
