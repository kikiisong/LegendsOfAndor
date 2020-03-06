using Photon.Pun;
using Photon.Realtime;
using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveController : MonoBehaviourPun
{
    public float radius = 3;
    public Hero hero;

    bool isMoving = false;

    void Start()
    {
        Hero hero = (Hero) photonView.Owner.CustomProperties[K.Player.hero];
        GetComponent<SpriteRenderer>().sprite = hero.ui.GetSprite();
        GameGraph.Instance.PlaceAt(gameObject, this.hero.constants.StartingRegion);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && photonView.IsMine && TurnManager.IsMyTurn() && Input.GetMouseButtonDown(0))
        {
            isMoving = true;
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
            StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, clicked.position, 2f, () => {
                TurnManager.TriggerEvent_Move(clicked);
                isMoving = false;
            }));           
        }
    }
}
