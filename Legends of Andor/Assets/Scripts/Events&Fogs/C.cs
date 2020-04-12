using Bag;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class C : LegendCard
    {
        public override Letter Key => Letter.C;

        public GameObject monsterParent;
        public GameObject gorPrefab;
        public GameObject skralPrefab;
        public GameObject towerPrefab;

        protected override void Event(Difficulty difficulty)
        {
            //TODO difficulty
            switch (difficulty)
            {
                default:
                    //Monsters
                    if (PhotonNetwork.IsMasterClient)
                    {
                        foreach (int r in new int[] { 8, 20, 21, 26, 48 })
                        {
                            GameObject gor = PhotonNetwork.Instantiate(gorPrefab);
                            gor.GetComponent<MonsterMoveController>().SetParentRPC(monsterParent);
                            GameGraph.Instance.PlaceAt(gor, r);
                        }
                    }

                    GameObject skral = PhotonNetwork.Instantiate(skralPrefab);
                    skral.GetComponent<MonsterMoveController>().SetParentRPC(monsterParent);
                    GameGraph.Instance.PlaceAt(skral, 19);


                    FarmerManager.Instance.SetFarmerRPC();
                    break;


            }
        }
    }
}
