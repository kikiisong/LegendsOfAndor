using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviourPun, IPunObservable
{
    public float radius = 3;

    // Start is called before the first frame update
    void Start()
    {
        Character character = (Character) photonView.Owner.CustomProperties["character"];
        print(character.type);
        SetUp(character);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && photonView.IsMine)
        {
            MoveToClick();
        }
    }

    public void SetUp(Character character)
    {
        GetComponent<SpriteRenderer>().sprite = character.GetSprite();
        switch (character.type)
        {
            case CharacterType.ARCHER:
                GameGraph.Instance.PlaceAt(gameObject, 53);
                break;
            case CharacterType.WARRIOR:
                GameGraph.Instance.PlaceAt(gameObject, 25);
                break;
            case CharacterType.DWARF:
                GameGraph.Instance.PlaceAt(gameObject, 43);
                break;
            case CharacterType.WIZARD:
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

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
       
    }
}
