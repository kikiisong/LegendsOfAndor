using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameGraph.Instance.PlaceAt(gameObject, 0, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MoveToClick();
        }
    }

    public void SetUp(Character character)
    {
        GetComponent<SpriteRenderer>().sprite = character.GetSprite();
    }

    public void MoveToClick()
    {
        Region current = GameGraph.Instance.FindNearest(transform.position);
        Vector3 position = GameGraph.Instance.CastRay(Input.mousePosition);
        Region clicked = GameGraph.Instance.FindNearest(position);
        bool contained = GameGraph.Instance.AdjacentVertices(current).Contains(clicked);
        if (contained && (clicked.position - position).magnitude <= 2)
        {
            StopAllCoroutines();
            StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, clicked.position, 2f));
        }
    }
}
