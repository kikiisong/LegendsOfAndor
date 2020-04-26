using Photon.Pun;
using Photon.Realtime;
using Routines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class HeroMoveController : MonoBehaviourPun
{
    public float radius = 3;
    public float animation_time = 1;

    bool isMoving = false;
    bool isControllingPrince = false;
    public int princeMoveCounter = 0;

    public Button movePrinceButton;


    

    public void activateControllingPrince()
    {
       
        if (Prince.Instance != null) isControllingPrince = true;
    }


    public int PrinceMoveCounter
    {
        set
        {
            princeMoveCounter = value;
        }
        get
        {
            return princeMoveCounter;
        }
    }




    public bool IsControllingPrince
    {
        set
        {
            isControllingPrince = value;
        }
        get
        {
            return Prince.Instance != null && isControllingPrince;
        }
    }



    void Start()
    {
        Hero hero = photonView.Owner.GetHero();
        GetComponent<SpriteRenderer>().sprite = hero.ui.GetSprite();     
        movePrinceButton = GameObject.Find("MovePrince").GetComponent<Button>();
        movePrinceButton.onClick.AddListener(activateControllingPrince);
        movePrinceButton.gameObject.SetActive(false);

    }


    // Update is called once per frame
    void Update()
    {

        if (!isMoving && photonView.IsMine && TurnManager.CanMove() && Input.GetMouseButtonDown(0))
        {
           if (!IsControllingPrince)
           {
                MoveToClick();
           }
           else
           {
               MovePrince();
           }
        }
    }

    void MoveToClick()
    {
        try
        {    
            Region heroCurrent = GameGraph.Instance.FindNearest(transform.position); //hero current region
            Vector3 position = GameGraph.Instance.CastRay(Input.mousePosition);
            Region clicked = GameGraph.Instance.FindNearest(position);//clicked region

            bool contained = GameGraph.Instance.AdjacentVertices(heroCurrent).Contains(clicked);

            if (heroCurrent.label != clicked.label && contained && (clicked.position - position).magnitude <= radius)
            {
                isMoving = true;
                StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, clicked.position, animation_time, () =>
                {
                    TurnManager.TriggerEvent_Move(clicked);
                    isMoving = false;
                }));
            }

        }
        catch (Exception)
        {
            //click missed
        }
    }



    void MovePrince()
    {
        try
        {
            Vector3 mousePos = GameGraph.Instance.CastRay(Input.mousePosition);

            Region current = GameGraph.Instance.FindNearest(Prince.Instance.transform.position);
            Region clicked = GameGraph.Instance.FindNearest(mousePos);
            bool contained = GameGraph.Instance.AdjacentVertices(current).Contains(clicked);
            print(contained + " "+ (current.label != clicked.label));
            print((current.position - clicked.position).magnitude);
            if (current.label != clicked.label && contained && (clicked.position - mousePos).magnitude <= radius)
            {
                isMoving = true;
                StartCoroutine(CommonRoutines.MoveTo(Prince.Instance.gameObject.transform, clicked.position, animation_time, () =>
                {
                    //prince move counter
                    princeMoveCounter++;
                    if (princeMoveCounter == 4)//or end turn clicked
                    {
                        TurnManager.TriggerEvent_Move(clicked);
                        princeMoveCounter = 0;
                    }

                    isMoving = false;
                }));

            }
        }
        catch (Exception)
        {
            //missed
        }
    }
}
