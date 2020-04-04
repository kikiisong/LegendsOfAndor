using ExitGames.Client.Photon;
using Newtonsoft.Json.Linq;
using Photon.Pun;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Custom
{
    public class CustomTypes
    {
        public static void Register()
        {
            PhotonPeer.RegisterType(typeof(Hero), 0, Serialize, Deserialize);
        }

        private static object Deserialize(byte[] bytes)
        {
            JObject jObject = JObject.Parse(Encoding.UTF8.GetString(bytes));
            Hero.Type type = jObject["type"].ToObject<Hero.Type>();
            Hero hero = Hero.FindInResources(type);
            hero.ui.gender = jObject["gender"].ToObject<bool>();
            return hero;
        }

        private static byte[] Serialize(object customObject)
        {
            Hero hero = (Hero)customObject;
            JObject jObject = new JObject
            {
                {"type", (int) hero.type },
                {"gender", hero.ui.gender }
            };
            return Encoding.UTF8.GetBytes(jObject.ToString());
        }

        public static object Deserialize_BF(byte[] data)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream(data);
            return binaryFormatter.Deserialize(memoryStream);
        }

        public static byte[] Serialize_BF(object obj)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, obj);
            return memoryStream.GetBuffer();
        }

        /*private static void AddSurrogates(BinaryFormatter binaryFormatter)
        {
            SurrogateSelector surrogateSelector = new SurrogateSelector();
            surrogateSelector.AddSurrogate(typeof(Sprite), new StreamingContext(StreamingContextStates.All),
                new SpriteSerializationSurrogate());
            binaryFormatter.SurrogateSelector = surrogateSelector;
        }*/
    }


    /*public class SpriteSerializationSurrogate : ISerializationSurrogate
    {
        //Serialize
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            Sprite sprite = (Sprite)obj;
            info.AddValue("path", AssetDatabase.GetAssetPath(sprite));
        }

        //Deserialize
        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            string path = (string)info.GetValue("path", typeof(string));
            return AssetDatabase.LoadAssetAtPath<Sprite>(path);
        }
    }*/
}