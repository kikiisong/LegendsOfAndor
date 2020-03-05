using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class A3 : LegendCard
    {
        public override Name CardName => Name.A3;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public override void Event()
        {
            HeroMoveController[] controllers = GameObject.FindObjectsOfType<HeroMoveController>();
            foreach(HeroMoveController controller in controllers)
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
        }
    }
}

