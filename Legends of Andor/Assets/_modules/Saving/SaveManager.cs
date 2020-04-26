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
                new JProperty("room", JRoom()),
                new JProperty("heroes", JHeroes()),                
                new JProperty("monsters", JMonsters()),
                new JProperty("goldpots", JGoldpot()),
                new JProperty("farmers",JFarmers()),
                new JProperty("narrator", JNarrator()));

            File.WriteAllText(Helper.GetPath(file_name), jObject.ToString());
        }

        public void Click_Save()
        {
            SaveGameState(PhotonNetwork.CurrentRoom.Name);
        }

        //Room
        private JObject JRoom()
        {
            return new JObject
            {
                {"num_players", PhotonNetwork.CurrentRoom.PlayerCount },
                {"difficulty", (int) Room.Difficulty }
            };
        }

        //Heroes
        private JArray JHeroes()
        {
            return new JArray(
                from player in PhotonNetwork.CurrentRoom.Players.Values
                select J.FromHero(player.GetHero(), player.GetCurrentRegion(). label));
        }

        private JArray JMonsters()
        {
            return new JArray(
                from monster in FindObjectsOfType<MonsterMoveController>()
                select new JObject
                {
                    {"type", new JValue(monster.type)},
                    {"region", monster.CurrentRegion.label},
                    {"data",  JObject.FromObject(monster.data)}
                });
        }

        private JArray JGoldpot()
        {
            return new JArray(
                from bagRegion in FindObjectsOfType<Region>()
                where bagRegion.data.numOfItems > 0
                select new JObject
                {
                    {"region", bagRegion.label },
                    {"numOfItems", bagRegion.data.numOfItems },
                    {"gold", bagRegion.data.gold },
                    {"numWineskin", bagRegion.data.numWineskin },
                    {"brew", bagRegion.data.brew },
                    {"herb", bagRegion.data.herb },
                    {"sheild", bagRegion.data.sheild },
                    {"helm", bagRegion.data.helm },
                    {"bow", bagRegion.data.bow },
                    {"falcon", bagRegion.data.falcon }
                }
            );
        }

        // save the farmer number on each region.
        private JArray JFarmers()
        {
            return new JArray(
                from farmer in FindObjectsOfType<Farmer>()
                select new JObject
                {
                    { "region", farmer.region},
                    { "numberOfFarmer", farmer.numberOfFarmer}
                }
            );
        }

        private JObject JNarrator()
        {
            return new JObject { 
                { "currentLoc", GameObject.FindObjectOfType<Card.Narrator>().GetComponent<Card.Narrator>().currentLoc},
                { "currentEventCard", GameObject.FindObjectOfType<EventCardController>().GetComponent<EventCardController>().currentEventIndex }
            };
        }
    }
}