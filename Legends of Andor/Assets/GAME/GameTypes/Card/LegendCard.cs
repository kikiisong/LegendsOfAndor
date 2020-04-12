using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public abstract class LegendCard : MonoBehaviourPun
    {
        public static Dictionary<Letter, LegendCard> Cards = new Dictionary<Letter, LegendCard>();

        public abstract Letter Key { get; }
    
        public LegendCard()
        {
            Cards[Key] = this;
        }

        public void Event()
        {
            Event(Room.Difficulty);
        }

        protected abstract void Event(Difficulty difficulty);

        public enum Letter
        {
            A, B, C, N, G
        }
    }
}
