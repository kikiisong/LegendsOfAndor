using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Saving
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
                return Application.isEditor ? Directory.GetParent(Application.dataPath).FullName : Application.persistentDataPath;
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
        public static string GetPath(string file_name, string file_extension = "json")
        {
            return Path_Directory + file_name + "." + file_extension;
        }

        public static JObject GetJson(string file_name, string file_extension = "json")
        {
            return JObject.Parse(File.ReadAllText(GetPath(file_name, file_extension: file_extension)));
        }
    }
}

