using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveController : MonoBehaviourPun
{
    public float radius = 3;
    public Hero hero;
    public List<HeroMoveController.IOnMove> onMoveListeners;

    void Start()
    {
        HeroUIData heroUIData = (HeroUIData) photonView.Owner.CustomProperties[K.Player.hero];
        GetComponent<SpriteRenderer>().sprite = heroUIData.GetSprite();
        GameGraph.Instance.PlaceAt(gameObject, hero.constants.StartingRegion);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && photonView.IsMine)
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
            StopAllCoroutines();
            StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, clicked.position, 2f));
        }
    }

    public interface IOnMove
    {
        void OnMove();
    }
}
