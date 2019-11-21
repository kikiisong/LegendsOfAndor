using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

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
}
