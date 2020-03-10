using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMerchantController : MonoBehaviourPun
{
    Merchant[] merchants;

    Hero hero;
    // Start is called before the first frame update
    void Start()
    {
        merchants = GameObject.FindObjectsOfType<Merchant>();

        hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];

    }

    // Update is called once per frame
    void Update()
    {

        Region current = GameGraph.Instance.FindNearest(transform.position);
        if (Input.GetMouseButtonDown(0))
        {
            if (current.label == merchants[0].regionLabel)
            {


                Vector3 click = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                BoxCollider2D col = merchants[0].gameObject.GetComponent<BoxCollider2D>();

                if (col.OverlapPoint(click))
                {
                    //open menu
                    merchants[0].openMenu(hero);

                }

            }

            else if (current.label == merchants[1].regionLabel)
            {
                Vector3 click = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                BoxCollider2D col = merchants[1].gameObject.GetComponent<BoxCollider2D>();

                if (col.OverlapPoint(click))
                {
                    //open menu
                    merchants[1].openMenu(hero);
                }
            }
        }
    }
}