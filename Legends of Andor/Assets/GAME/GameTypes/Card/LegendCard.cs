using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public abstract class LegendCard : MonoBehaviourPun
    {
        public abstract Name CardName { get; }

        public LegendCard()
        {
            LegendCardManager.Cards[CardName] = this;
        }

        public abstract void Event();

        public enum Name
        {
            A1, A2, A3, A4, B1, B2, B3
        }
    }
}
