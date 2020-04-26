using Bag;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Card
{
    public class C : LegendCard
    {
        public override Letter Key => Letter.C;

        public GameObject monsterParent;
        public GameObject gorPerfab;
        public GameObject skralPerfab;
        public GameObject skralWithTowerPrefab;
        public GameObject towerInfo;
        public GameObject towerDice;
        public GameObject princePrefab;
        public GameObject movePrinceButton;

        MonsterMoveController linkedMonster;

        protected override void Event(Difficulty difficulty)
        {
            //TODO difficulty
            switch (difficulty)
            {
                case Difficulty.Easy:
                    //Farmer
                    FarmerManager.Instance.SetFamerRPCAtRegion28();


                    //Monsters
                    if (PhotonNetwork.IsMasterClient)
                    {
                        photonView.RPC("PlaceTowerMonster", RpcTarget.AllBuffered);

                        foreach (int r in new int[] { 27, 31 })
                        {
                            GameObject gor = PhotonNetwork.Instantiate(gorPerfab);
                            gor.GetComponent<MonsterMoveController>().SetParentRPC(monsterParent);
                            GameGraph.Instance.PlaceAt(gor, r);

                        }

                        GameObject skral = PhotonNetwork.Instantiate(skralPerfab);
                        skral.GetComponent<MonsterMoveController>().SetParentRPC(monsterParent);
                        GameGraph.Instance.PlaceAt(skral, 29);

                        GameObject prince = PhotonNetwork.Instantiate(princePrefab);
                        prince.transform.SetParent(GameObject.Find("Map").transform);
                        GameGraph.Instance.PlaceAt(prince, 72);

                        movePrinceButton = GameObject.Find("MovePrince");
                        movePrinceButton.SetActive(true);
                        

                    }


                    break;

                case Difficulty.Normal:
                    //Farmer
                    FarmerManager.Instance.SetFamerRPCAtRegion28();


                    //Monsters
                    if (PhotonNetwork.IsMasterClient) 
                    {
                        photonView.RPC("PlaceTowerMonster", RpcTarget.AllBuffered);

                        foreach (int r in new int[] { 27, 31 })
                        {
                            GameObject gor = PhotonNetwork.Instantiate(gorPerfab);
                            gor.GetComponent<MonsterMoveController>().SetParentRPC(monsterParent);
                            GameGraph.Instance.PlaceAt(gor, r);

                        }

                        GameObject skral = PhotonNetwork.Instantiate(skralPerfab);
                        skral.GetComponent<MonsterMoveController>().SetParentRPC(monsterParent);
                        GameGraph.Instance.PlaceAt(skral, 29);

                        GameObject prince = PhotonNetwork.Instantiate(princePrefab);
                        movePrinceButton = GameObject.Find("MovePrince");
                        movePrinceButton.SetActive(true);
                        prince.transform.SetParent(GameObject.Find("Map").transform);
                        GameGraph.Instance.PlaceAt(prince, 72);
                    }


                    break;
            }
        }

        [PunRPC]
        public void PlaceTowerMonster()
        {
            Text t = towerInfo.transform.GetChild(1).GetComponent<Text>();
            t.text = "The room owner will roll the dice to locate the tower.";
            towerInfo.SetActive(true);
            towerInfo.transform.GetChild(2).gameObject.SetActive(false);

            towerDice.SetActive(true);
            towerDice.transform.GetChild(0).gameObject.SetActive(false);
            towerDice.transform.GetChild(2).gameObject.SetActive(false);

            if (PhotonNetwork.IsMasterClient)
            {
                towerDice.transform.GetChild(0).gameObject.SetActive(true);
                GameObject rollButton = towerDice.transform.GetChild(0).gameObject;
                rollButton.GetComponent<Button>().onClick.RemoveAllListeners();
                rollButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    rollDice();
                });

            }
        }

        public void rollDice()
        {
            towerDice.transform.GetChild(0).gameObject.SetActive(false);
            System.Random rand = new System.Random();
            int rInt = rand.Next(1, 7);
            photonView.RPC("updateDice", RpcTarget.AllBuffered, rInt);
        }

        [PunRPC]
        public void updateDice(int dice)
        {
            Text t = towerDice.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>();
            t.text = dice.ToString();

            int herbAt = 50 + dice;

            Text t2 = towerInfo.transform.GetChild(1).GetComponent<Text>();
            t2.text = "The tower and a skral has located at " + herbAt + " region.";

            towerInfo.transform.GetChild(2).gameObject.SetActive(true);
            towerDice.transform.GetChild(2).gameObject.SetActive(true);

            SetSkral(herbAt);
        }

        public void SetSkral(int herbAt)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                var go = PhotonNetwork.Instantiate(skralWithTowerPrefab);
                go.GetComponent<MonsterMoveController>().SetParentRPC(monsterParent);
                GameGraph.Instance.PlaceAt(go, herbAt);
            }

            print("set the skral cannot move");

            linkedMonster = skralWithTowerPrefab.GetComponent<MonsterMoveController>();
            linkedMonster.canMove = false;

            Player[] players = PhotonNetwork.PlayerList;
            int numberOfPlayer = players.Length;

            // TODO due to the difficulty of the room, the skral will have different sp
            if(Room.Difficulty == Difficulty.Easy)
            {
                if (numberOfPlayer == 2)
                {
                    linkedMonster.data.sp = 10;
                }
                else if (numberOfPlayer == 3)
                {
                    linkedMonster.data.sp = 20;
                }
                else if (numberOfPlayer == 4)
                {
                    linkedMonster.data.sp = 30;
                }
            }
            else
            {
                if (numberOfPlayer == 2)
                {
                    linkedMonster.data.sp = 20;
                }
                else if (numberOfPlayer == 3)
                {
                    linkedMonster.data.sp = 30;
                }
                else if (numberOfPlayer == 4)
                {
                    linkedMonster.data.sp = 40;
                }
            }
        }
    }
}
