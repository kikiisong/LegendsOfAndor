using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class A3 : LegendCard
    {
        public override Name CardName => Name.A3;

        public GameObject monsterParent;
        public GameObject gorPrefab;
        public GameObject skralPrefab;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
           
        }

        public override void Event()
        {
            //Hero
            HeroMoveController[] controllers = GameObject.FindObjectsOfType<HeroMoveController>();
            foreach (HeroMoveController controller in controllers)
            {
                Hero hero = controller.hero;
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
        }
    }
}

