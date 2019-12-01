using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        bool b = Heroes.Instance.characters[0].female;
        print(b);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject);
    }
}
