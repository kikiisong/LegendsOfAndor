using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card{

    public class LegendCardManager : MonoBehaviour
    {
        public static Dictionary<LegendCard.Name, LegendCard> Cards = new Dictionary<LegendCard.Name, LegendCard>();

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Cards[LegendCard.Name.A3].Event();
            }
        }
    }
}

