using ExitGames.Client.Photon;
using Photon.Pun;
using System;
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
            PhotonPeer.RegisterType(typeof(HeroUIData), 0, Serialize, Deserialize);
        }

        private static object Deserialize(byte[] serializedCustomObject)
        {
            int position = BitConverter.ToInt32(serializedCustomObject, 0);
            return HeroSelection.Characters[position];
        }

        private static byte[] Serialize(object customObject)
        {
            HeroUIData character = (HeroUIData)customObject;
            int position = HeroSelection.Characters.FindIndex(c => c.type == character.type);
            return BitConverter.GetBytes(position);
        }

        /*public static object Deserialize(byte[] data)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            AddSurrogates(binaryFormatter);
            MemoryStream memoryStream = new MemoryStream(data);
            return binaryFormatter.Deserialize(memoryStream);
        }

        public static byte[] Serialize(object customType)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            AddSurrogates(binaryFormatter);
            MemoryStream memoryStream = new MemoryStream();
            binaryFormatter.Serialize(memoryStream, customType);
            return memoryStream.GetBuffer();
        }

        private static void AddSurrogates(BinaryFormatter binaryFormatter)
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