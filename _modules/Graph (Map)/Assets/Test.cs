using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Tests
        Region nearest = GameGraph.Instance.FindNearest(gameObject.transform.position);
        GameGraph.Instance.PlaceAt(gameObject, nearest, 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
