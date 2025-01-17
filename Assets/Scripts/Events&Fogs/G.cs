﻿using Bag;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class G : LegendCard
    {
        public override Letter Key => Letter.G;

        public GameObject monsterParent;
        public GameObject wardraksPrefab;
        public GameObject movePrinceButton;


        protected override void Event(Difficulty difficulty)
        {
            //TODO difficulty
            switch (difficulty)
            {

                default:
                    //Monsters
                    if (PhotonNetwork.IsMasterClient)
                    {
                        /*foreach (int r in new int[] { 26, 27 })
                        {
                            GameObject gor = PhotonNetwork.Instantiate(wardraksPrefab);
                            gor.GetComponent<MonsterMoveController>().SetParentRPC(monsterParent);
                            GameGraph.Instance.PlaceAt(gor, r);
                        }*/

                        photonView.RPC("RemovePrince", RpcTarget.All);
                    }

                break;
            }
        }

        [PunRPC]
        public void RemovePrince()
        {
            movePrinceButton = GameObject.Find("MovePrince");
            movePrinceButton.SetActive(false);
            Prince.Instance.gameObject.SetActive(false);
        }
    }
}
