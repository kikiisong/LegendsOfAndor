using Photon.Pun;
using Photon.Realtime;
using Routines;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HeroMoveController : MonoBehaviourPun
{
    public float radius = 3;
    public float animation_time = 1;

    bool isMoving = false;

    void Start()
    {
        Hero hero = photonView.Owner.GetHero();
        GetComponent<SpriteRenderer>().sprite = hero.ui.GetSprite();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && photonView.IsMine && TurnManager.CanMove() && Input.GetMouseButtonDown(0))
        {
            MoveToClick();
        }
    }

    public void MoveToClick()
    {
        try
        {
            Hero hero = photonView.Owner.GetHero();
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
}
