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


    public GameObject princeButton;


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

    void activateMovePrince()
    {
        isControllingPrince=true;

    }

    void Start()
    {
        Hero hero = photonView.Owner.GetHero();
        GetComponent<SpriteRenderer>().sprite = hero.ui.GetSprite();
        princeButton.GetComponent<Button>().onClick.AddListener(() => activateMovePrince());
    }

    // Update is called once per frame
    void Update()
    {
        if (Prince.Instance != null)
        {
            princeButton.SetActive(true);
        }
        else
        {
            princeButton.SetActive(false);
        }

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
            Region current = GameGraph.Instance.FindNearest(transform.position);
            Vector3 position = GameGraph.Instance.CastRay(Input.mousePosition);
            Region clicked = GameGraph.Instance.FindNearest(position);
            bool contained = GameGraph.Instance.AdjacentVertices(current).Contains(clicked);
            if (current.label != clicked.label && contained && (clicked.position - position).magnitude <= radius)
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
                    //TurnManager.TriggerEvent_Move(clicked);
                    isMoving = false;
                }));

                isControllingPrince = false;
            }
        }
        catch (Exception)
        {
            //missed
        }
    }
}
