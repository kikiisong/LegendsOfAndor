using Newtonsoft.Json.Linq;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Saving
{
    public class SaveManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SaveGameState(string file_name)
        {
            JObject jObject = new JObject(
                new JProperty("heroes", JHeroes())                
                );

            File.WriteAllText(Helper.GetPath(file_name), jObject.ToString());
            Helper.RefreshEditor();
        }

        public void Click_Save()
        {
            SaveGameState(PhotonNetwork.CurrentRoom.Name);
        }


        //Heroes
        private JArray JHeroes()
        {
            return new JArray(
                from player in PhotonNetwork.CurrentRoom.Players.Values
                let hero = (Hero)player.CustomProperties[K.Player.hero]
                select new JObject
                {
                    {"type", new JValue(hero.type) },
                    {"data", JObject.FromObject(hero.data) },
                    {"region", HeroMoveController.CurrentRegion(player).label },
                    {"gender", hero.ui.gender }
                });
        }
    }
}
