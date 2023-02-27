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
using Saving;

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
            return J.ToHero(jObject);
        }

        private static byte[] Serialize(object customObject)
        {
            Hero hero = (Hero)customObject;
            JObject jObject = J.FromHero(hero);
            return Encoding.UTF8.GetBytes(jObject.ToString());
        }
    }
}