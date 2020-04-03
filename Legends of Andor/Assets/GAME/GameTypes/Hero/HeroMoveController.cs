using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveController : MonoBehaviourPun
{
    public float radius = 3;

    public float animation_time = 1;

    bool isMoving = false;

    public Hero hero;


    void Start()
    {
        Hero hero = (Hero) photonView.Owner.CustomProperties[K.Player.hero];
        GetComponent<SpriteRenderer>().sprite = hero.ui.GetSprite();

        GameGraph.Instance.PlaceAt(gameObject, this.hero.constants.StartingRegion);

    }

    // Update is called once per frame
    void Update()
    {

        if (!isMoving && photonView.IsMine && TurnManager.CanMove() && Input.GetMouseButtonDown(0))

        if (photonView.IsMine && TurnManager.IsMyTurn() && Input.GetMouseButtonDown(0))

        {
            MoveToClick();
        }
    }

    public void MoveToClick()
    {
        Region current = GameGraph.Instance.FindNearest(transform.position);
        Vector3 position = GameGraph.Instance.CastRay(Input.mousePosition);
        Region clicked = GameGraph.Instance.FindNearest(position);
        bool contained = GameGraph.Instance.AdjacentVertices(current).Contains(clicked);
        if (contained && (clicked.position - position).magnitude <= radius)
        {

            hero.data.regionNumber = clicked.label;
            isMoving = true;
            StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, clicked.position, animation_time, () => {

                //StopAllCoroutines();
                StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, clicked.position, 2f, () => {

                    TurnManager.TriggerEvent_Move(clicked);
                }));           
            }
        }
    }


    public static Region CurrentRegion()
    {
        foreach(HeroMoveController heroMoveController in GameObject.FindObjectsOfType<HeroMoveController>())
        {
            if(heroMoveController.photonView.Owner == PhotonNetwork.LocalPlayer)
            {
                return GameGraph.Instance.FindNearest(heroMoveController.transform.position);
            }
        }
        throw new System.Exception("Not found");
    }

}
