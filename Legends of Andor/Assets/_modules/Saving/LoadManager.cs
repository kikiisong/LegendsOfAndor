using Card;
using Monsters;
using Newtonsoft.Json.Linq;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Saving
{
    public class LoadManager : MonoBehaviour
    {
        [Header("Instantiate")]
        public GameObject heroPrefab;
        public GameObject timeMarkerPrefab;
        public GameObject goldpotPrefab;
        [Header("Monsters")]
        public MonsterMoveController gor;
        public MonsterMoveController skral;
        public MonsterMoveController wardrak;

 

        //Getters
        Region InitialRegion
        {
            get
            {
                foreach(JObject jObject in Room.Json["heroes"])
                {
                    var type = jObject["type"].ToObject<Hero.Type>();
                    if(PhotonNetwork.LocalPlayer.GetHero().type == type)
                    {
                        int label = jObject["region"].ToObject<int>();
                        return GameGraph.Instance.Find(label);
                    }
                }
                throw new System.Exception();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            //After everyone
            if (!Room.IsSaved)
            {
                LoadDefault();
            }
            else
            {
                LoadPreviousState();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }


        void LoadDefault()
        {
            PhotonNetwork.Instantiate(heroPrefab); //each player creates a hero
            PhotonNetwork.Instantiate(timeMarkerPrefab);
           // if (PhotonNetwork.IsMasterClient)
           // {
          //      LegendCard.Cards[LegendCard.Letter.A].Event();
           // }
        }

        void LoadPreviousState()
        {
            //Hero
            PhotonNetwork.Instantiate(heroPrefab.name, InitialRegion.position, Quaternion.identity); //your hero
            PhotonNetwork.Instantiate(timeMarkerPrefab);
            //Monsters
            if (PhotonNetwork.IsMasterClient)
            {
                foreach(var j in Room.Json["monsters"])
                {
                    MonsterType type = j["type"].ToObject<MonsterType>();
                    int label = j["region"].ToObject<int>();
                    switch (type)
                    {
                        case MonsterType.Gor:
                            var go = PhotonNetwork.Instantiate(gor.name, GameGraph.Instance.Find(label).position, Quaternion.identity);
                            go.GetComponent<MonsterMoveController>().InitRPC(j["data"]);
                            break;
                        case MonsterType.Skral:
                            go = PhotonNetwork.Instantiate(skral.name, GameGraph.Instance.Find(label).position, Quaternion.identity);
                            go.GetComponent<MonsterMoveController>().InitRPC(j["data"]);
                            break;
                        case MonsterType.Wardrak:
                            go = PhotonNetwork.Instantiate(wardrak.name, GameGraph.Instance.Find(label).position, Quaternion.identity);
                            go.GetComponent<MonsterMoveController>().InitRPC(j["data"]);
                            break;
                    }
                }

                //Goldpots 
                foreach(var j in Room.Json["goldpots"])
                {
                    int label = j["region"].ToObject<int>();
                    PhotonNetwork.Instantiate(goldpotPrefab.name, GameGraph.Instance.Find(label).position, Quaternion.identity);
                    Region reg = GameGraph.Instance.Find(label);
                    reg.data.numOfItems = j["numOfItems"].ToObject<int>();
                    reg.data.gold = j["gold"].ToObject<int>();
                    reg.data.numWineskin = j["numWineskin"].ToObject<int>();
                    reg.data.brew = j["brew"].ToObject<int>();
                    reg.data.herb = j["herb"].ToObject<int>();
                    reg.data.sheild = j["sheild"].ToObject<int>();
                    reg.data.helm = j["helm"].ToObject<int>();
                    reg.data.bow = j["bow"].ToObject<int>();
                    reg.data.falcon = j["falcon"].ToObject<int>();
                }
            }
        }
    }
}
