using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        public static string GetPath(string file_name, string file_extension="txt")
        {
            return Path_Directory + file_name + "." + file_extension;
        }
        public static StreamWriter Create(string game_name)
        {
            return File.CreateText(GetPath(game_name));
        }
    }

    public static class Saving {

        public static void SaveGameState()
        {

        }
    }
}
