using Bag;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Card
{
    public class A : LegendCard
    {
        public override Letter Key => Letter.A;

        public GameObject monsterParent;
        public GameObject gorPrefab;
        public GameObject skralPrefab;

        protected override void Event(Difficulty difficulty)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                DistributionManager.Distribute((ItemType.Coin, 5), (ItemType.Wineskin, 2));
            }
            switch (difficulty)
            {

                case Difficulty.Easy:
                    photonView.RPC("MoveHeroes", RpcTarget.All);

                    //Monsters
                    if (PhotonNetwork.IsMasterClient)
                    {
                        foreach (int r in new int[] {8, 20, 21, 26, 48 })
                        {
                            GameObject gor = PhotonNetwork.Instantiate(gorPrefab);
                            gor.GetComponent<MonsterMoveController>().SetParentRPC(monsterParent, buffered:false);
                            GameGraph.Instance.PlaceAt(gor, r);
                        }
                    }

                    GameObject skral = PhotonNetwork.Instantiate(skralPrefab);
                    skral.GetComponent<MonsterMoveController>().SetParentRPC(monsterParent);
                    GameGraph.Instance.PlaceAt(skral, 19);


                    FarmerManager.Instance.SetFarmerRPC();
                    break;

                case Difficulty.Normal:
                    photonView.RPC("MoveHeroes", RpcTarget.All);

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

                    GameObject skraln = PhotonNetwork.Instantiate(skralPrefab);
                    skraln.GetComponent<MonsterMoveController>().SetParentRPC(monsterParent);
                    GameGraph.Instance.PlaceAt(skraln, 19);


                    FarmerManager.Instance.SetFarmerRPCNormal();
                    break;
            }
        }

        [PunRPC]
        public void MoveHeroes()
        {
            //Hero
            HeroMoveController[] controllers = GameObject.FindObjectsOfType<HeroMoveController>();
            foreach (HeroMoveController controller in controllers)
            {
                if (controller.photonView.IsMine)
                {
                    Hero hero = controller.photonView.Owner.GetHero();
                    switch (hero.type)
                    {
                        case Hero.Type.DWARF:
                            GameGraph.Instance.PlaceAt(controller.gameObject, 7);
                            break;
                        case Hero.Type.WARRIOR:
                            GameGraph.Instance.PlaceAt(controller.gameObject, 14);
                            break;
                        case Hero.Type.ARCHER:
                            GameGraph.Instance.PlaceAt(controller.gameObject, 25);
                            break;
                        case Hero.Type.WIZARD:
                            GameGraph.Instance.PlaceAt(controller.gameObject, 34);
                            break;
                    }
                }
            }
        }
    }
}

