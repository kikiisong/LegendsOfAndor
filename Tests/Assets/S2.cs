using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2 : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(prefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
