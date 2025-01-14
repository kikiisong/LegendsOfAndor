﻿using Card;
using Monsters;
using Newtonsoft.Json.Linq;
using Photon.Pun;
using Photon.Realtime;
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
        public GameObject fogPrefab;
        public GameObject witchPrefab;
        public GameObject wellPrefab;
        public GameObject princePrefab;
        public GameObject herbPrefab;
        [Header("Monsters")]
        public MonsterMoveController gor;
        public MonsterMoveController skral;
        public MonsterMoveController wardrak;
        public MonsterMoveController gorHerb;
        public MonsterMoveController skralOnTower;




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
                foreach (var j in Room.Json["monsters"])
                {
                    MonsterType type = j["type"].ToObject<MonsterType>();
                    int label = j["region"].ToObject<int>();
                    bool hasHerb = j["herb"].ToObject<bool>();
                    if(hasHerb)
                    {
                        var go = PhotonNetwork.Instantiate(gorHerb.name, GameGraph.Instance.Find(label).position, Quaternion.identity);
                        go.GetComponent<MonsterMoveController>().InitRPC(j["data"]);
                    }
                    else
                    {
                        switch (type)
                        {
                            case MonsterType.Gor:
                                var go = PhotonNetwork.Instantiate(gor.name, GameGraph.Instance.Find(label).position, Quaternion.identity);
                                go.GetComponent<MonsterMoveController>().InitRPC(j["data"]);
                                break;
                            case MonsterType.Skral:
                                if (j["isSkralOnTower"].ToObject<bool>() == false)
                                {
                                    go = PhotonNetwork.Instantiate(skralOnTower.name, GameGraph.Instance.Find(label).position, Quaternion.identity);
                                    go.GetComponent<MonsterMoveController>().InitRPC(j["data"]);
                                    go.GetComponent<MonsterMoveController>().canMove = false;

                                    Player[] players = PhotonNetwork.PlayerList;
                                    int numberOfPlayer = players.Length;

                                    // TODO due to the difficulty of the room, the skral will have different sp
                                    if (Room.Difficulty == Difficulty.Easy)
                                    {
                                        if (numberOfPlayer == 2)
                                        {
                                            go.GetComponent<MonsterMoveController>().data.sp = 10;
                                        }
                                        else if (numberOfPlayer == 3)
                                        {
                                            go.GetComponent<MonsterMoveController>().data.sp = 20;
                                        }
                                        else if (numberOfPlayer == 4)
                                        {
                                            go.GetComponent<MonsterMoveController>().data.sp = 30;
                                        }
                                    }
                                    else
                                    {
                                        if (numberOfPlayer == 2)
                                        {
                                            go.GetComponent<MonsterMoveController>().data.sp = 20;
                                        }
                                        else if (numberOfPlayer == 3)
                                        {
                                            go.GetComponent<MonsterMoveController>().data.sp = 30;
                                        }
                                        else if (numberOfPlayer == 4)
                                        {
                                            go.GetComponent<MonsterMoveController>().data.sp = 40;
                                        }
                                    }
                                }
                                else
                                {
                                    go = PhotonNetwork.Instantiate(skral.name, GameGraph.Instance.Find(label).position, Quaternion.identity);
                                    go.GetComponent<MonsterMoveController>().InitRPC(j["data"]);
                                }
                                break;
                            case MonsterType.Wardrak:
                                go = PhotonNetwork.Instantiate(wardrak.name, GameGraph.Instance.Find(label).position, Quaternion.identity);
                                go.GetComponent<MonsterMoveController>().InitRPC(j["data"]);
                                break;
                        }
                    }
                    
                }



                //Goldpots 
                foreach (var j in Room.Json["goldpots"])
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

            //print("It is correct until here !!!!!");
            // set the farmer number on each region to the original one
            foreach (var j in Room.Json["farmers"])
            {
                int label = j["region"].ToObject<int>();
                Region reg = GameGraph.Instance.Find(label);
                List<Farmer> temp = GameGraph.Instance.FindObjectsOnRegion<Farmer>(reg.label);
                int numOfFar = j["numberOfFarmer"].ToObject<int>();
                if (temp.Count > 0)
                {
                    temp[0].setNumOfFarmer(numOfFar);
                    /*
                    if(numOfFar == 1)
                    {
                        print("the farmer number is 1 and this region is region " + reg.label);
                    }

                    print("The farmer number on region " + reg.label + " is " + temp[0].numberOfFarmer);
                    */
                }

            }

            // set the narrator position and the current event card
            // but the effect of this card won't be taken
            var Narrator = Room.Json["narrator"];
            int currentLocation = Narrator["currentLoc"].ToObject<int>();
            GameObject.FindObjectOfType<Card.Narrator>().transform.position = GameObject.FindObjectOfType<Card.Narrator>().GetComponent<Card.Narrator>().narratorPositions[currentLocation].position;
            GameObject.FindObjectOfType<Card.Narrator>().GetComponent<Card.Narrator>().currentLoc = currentLocation;
            List<GameObject> currentEventCardList = GameObject.FindObjectOfType<EventCardController>().GetComponent<EventCardController>().evnetCardList;
            int currentEventIndex = Narrator["currentEventCard"].ToObject<int>();
            GameObject.FindObjectOfType<eventCardButton>().GetComponent<eventCardButton>().setEventCard(currentEventCardList[currentEventIndex]);


            // set the Castle's shield back to the old value
            var castle = Room.Json["castle"];
            int shieldNum = castle["currentNumOfShield"].ToObject<int>();
            GameObject.FindObjectOfType<Castle>().GetComponent<Castle>().setShieldNum(shieldNum);


            foreach (var j in Room.Json["fogs"])
            {
                int label = j["region"].ToObject<int>();
                GameObject myFog = Instantiate(fogPrefab, GameGraph.Instance.Find(label).position, Quaternion.identity);
                Fog f = myFog.GetComponent<Fog>();
                f.region = label;
                f.type = j["type"].ToObject<FogType>();
            }

            foreach (var j in Room.Json["witch"])
            {
                int label = j["region"].ToObject<int>();
                GameObject myWitch = Instantiate(witchPrefab, GameGraph.Instance.Find(label).position, Quaternion.identity);
                Witch w = myWitch.GetComponent<Witch>();
                w.region = label;
                w.left = j["left"].ToObject<int>();
               
            }

            foreach (var j in Room.Json["wells"])
            {
                int label = j["region"].ToObject<int>();
                bool filled = j["filled"].ToObject<bool>();

                GameObject myWell = Instantiate(wellPrefab, GameGraph.Instance.Find(label).position, Quaternion.identity);
                Well curr = myWell.GetComponent<Well>();

                
                if (!filled)
                {
                    curr.emptied();
                }

            }

            foreach (var j in Room.Json["herb"])
            {
                int label = j["region"].ToObject<int>();

                Instantiate(herbPrefab, GameGraph.Instance.Find(label).position, Quaternion.identity);

            }





            //load prince if exists
            var jprince = Room.Json["prince"];
            if (jprince != null)
            {
                print("load prince");
                int r = jprince["r"].ToObject<int>();
                var prince = PhotonNetwork.Instantiate(princePrefab).GetComponent<Prince>();
                GameGraph.Instance.PlaceAt(prince.gameObject, r);
                prince.GetComponent<Prince>().inFight = jprince["princeInFight"].ToObject<bool>();
            }
        }
    }
}
