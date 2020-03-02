using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMoveController : MonoBehaviourPun
{
    public float radius = 3;

    void Start()
    {
        HeroUIData heroUIData = (HeroUIData) photonView.Owner.CustomProperties[K.Player.hero];
        SetUp(heroUIData);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && photonView.IsMine)
        {
            MoveToClick();
        }
    }

    public void SetUp(HeroUIData character)
    {
        GetComponent<SpriteRenderer>().sprite = character.GetSprite();
        switch (character.type)
        {
            case HeroType.ARCHER:
                GameGraph.Instance.PlaceAt(gameObject, 53);
                break;
            case HeroType.WARRIOR:
                GameGraph.Instance.PlaceAt(gameObject, 25);
                break;
            case HeroType.DWARF:
                GameGraph.Instance.PlaceAt(gameObject, 43);
                break;
            case HeroType.WIZARD:
                GameGraph.Instance.PlaceAt(gameObject, 9);
                break;
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
}
