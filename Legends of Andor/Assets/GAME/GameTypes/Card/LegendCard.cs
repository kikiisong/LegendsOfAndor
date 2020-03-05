using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public abstract class LegendCard : MonoBehaviour
    {
        public abstract Name CardName { get; }

        public abstract void Event();

        protected virtual void Start()
        {
            LegendCardManager.Cards[CardName] = this;
        }

        public enum Name
        {
            A1, A2, A3, A4, B1, B2, B3
        }
    }
}
