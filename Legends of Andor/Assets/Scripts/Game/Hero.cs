using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Region nearest = GameGraph.Instance.FindNearest(Input.mousePosition);
            StopAllCoroutines();
            StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, nearest.position, 2f));
        }
    }

    public void SetUp(Character character)
    {
        GetComponent<SpriteRenderer>().sprite = character.GetSprite();
    }
}
