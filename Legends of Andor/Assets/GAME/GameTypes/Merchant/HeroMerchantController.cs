using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroMerchantController : MonoBehaviourPun
{
    Merchant[] merchants;
    public Hero hero;

    // Start is called before the first frame update
    void Start()
    {
        merchants = GameObject.FindObjectsOfType<Merchant>();

        hero = (Hero)photonView.Owner.CustomProperties[K.Player.hero];

    }

    // Update is called once per frame
    void Update()
    {

        Region current = GameGraph.Instance.FindNearest(transform.position);
       

        if (photonView.IsMine && Input.GetMouseButtonDown(0))
        {

           foreach (Merchant m in merchants){

                if (current.label == m.regionLabel)
                {

                    
                    Vector3 click = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                    click = transform.TransformPoint(click);
                
                    BoxCollider2D col = m.gameObject.GetComponent<BoxCollider2D>();

                    if (col.OverlapPoint(click))
                    {
                        //open menu

                   
                        SceneManager.LoadScene(5);
                        //m.openMenu(hero);       

                    }

                }

            }


           
        }
    }
}

