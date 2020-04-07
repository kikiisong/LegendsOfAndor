using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

namespace Card
{
    public class Narrator : MonoBehaviourPun, TurnManager.IOnSunrise
    {
        public static Narrator Instance;
        public List<Transform> narratorPositions;
        public int currentLoc;

        public int[] orderofEvents;
        private static System.Random rand = new System.Random();
        public int currentEventIndex;

        public GameObject myEventCardController;

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
            int[] temp = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            orderofEvents = temp;
            shuffleArray(orderofEvents);
            currentEventIndex = 0;
            releaseNewRventCard();
            //printlist();
        }

        // shuffle the int array
        public static void shuffleArray(int[] a)
        {
            int n = a.Length;
            System.Random rand = new System.Random();

            for(int i = 0; i < n; i++)
            {
                swap(a, i, i + rand.Next(n - i));
            }
        }

        // helper method for the shuffle function
        public static void swap(int[] arr, int a, int b)
        {
            int temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }

        public void printlist()
        {
            print("you should have arrived here.");
            for(int i = 0; i < orderofEvents.Length; i++)
            {
                print(orderofEvents[i]);
            }
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
            // should send orderofEvents[currentEventIndex]
            myEventCardController.GetComponent<EventCardController>().newEventCard(orderofEvents[currentEventIndex]);
            // increase the event card index, next time will pick another one
            currentEventIndex += 1;
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
