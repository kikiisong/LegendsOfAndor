using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Card
{
    public class Narrator : MonoBehaviourPun, TurnManager.IOnSunrise
    {
        public static Narrator Instance;
        public List<Transform> narratorPositions;
        public int currentLoc;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Debug.LogWarning("Not singleton");
        }

        //TODO list of GameObject, but with field "cardLetter", if null then no legends are activated, use that letter in Cards[letter].Event()
        // or you can use a "red star"
        // Start is called before the first frame update
        void Start()
        {
            TurnManager.Register(this);
            //TODO: move from region 80 to A
            //  LegendCard.Cards[LegendCard.Letter.A].Event();

        }

        //call once, affects everyone
        public void Advance()
        {
            currentLoc = currentLoc + 1;

            // need to decide if we win or lose
            if (currentLoc >= 14)
            {
                print("you have reached the end of the game");
                return;
            }
            else
            {
                transform.position = narratorPositions[currentLoc].position;
            }

            handLegendCard();
            releaseNewRventCard();
        }

        public void releaseNewRventCard()
        {
            throw new NotImplementedException();
        }

        private void handLegendCard()
        {
            if(currentLoc == 2)
            {
                print("time to release Legend C");
            }
            else if(currentLoc == 6)
            {
                print("time to release Legend G");
            }
            else if(currentLoc == 13)
            {
                print("time to release Legend N");
            }
        }


        public void MoveToLetter(int letter)
        {
            transform.position = narratorPositions[letter].position;
        }

        public void OnSunrise()
        {
            print("the sunrise has been called");

            Advance();

            // new event card will show up;
        }


    }
}
