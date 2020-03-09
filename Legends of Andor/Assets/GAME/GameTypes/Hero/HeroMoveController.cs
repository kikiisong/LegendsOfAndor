using Photon.Pun;
using Photon.Realtime;
using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveController : MonoBehaviourPun
{
    public float radius = 3;

    bool isMoving = false;

    void Start()
    {
        Hero hero = (Hero) photonView.Owner.CustomProperties[K.Player.hero];
        GetComponent<SpriteRenderer>().sprite = hero.ui.GetSprite();
        GameGraph.Instance.PlaceAt(gameObject, hero.constants.StartingRegion);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && photonView.IsMine && TurnManager.IsMyTurn() && Input.GetMouseButtonDown(0))
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
        if (current.label != clicked.label && contained && (clicked.position - position).magnitude <= radius)
        {
            isMoving = true;
            StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, clicked.position, 2f, () => {
                TurnManager.TriggerEvent_Move(clicked);
                isMoving = false;
            }));           
        }
    }


    public void OnClickMonster(Monster aMonster)
    {
        Region current = GameGraph.Instance.FindNearest(transform.position);
        if (!isMoving && photonView.IsMine && TurnManager.IsMyTurn()&& current.label == aMonster.regionLabel) { 
            // add them to a list passed into fight seen
                //-how to use Photon to passed
            //invite the area around it also to fight
                // how to get surronding area
                // how to ask surronding hero
                    //-give them a button and preesed means join in
                    //-join them into the list as well
               

        }
    }
}
