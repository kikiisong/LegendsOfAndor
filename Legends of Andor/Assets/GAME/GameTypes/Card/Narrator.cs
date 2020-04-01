using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class Narrator : MonoBehaviour
    {
        public static Narrator Instance;

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
            //TODO: move from region 80 to A
            LegendCard.Cards[LegendCard.Letter.A].Event();
        }

        // Update is called once per frame
        void Update()
        {

        }

        //call once, affects everyone
        public static void Advance()
        {
            //Instance.Move
        }
    }
}
