using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prince : MonoBehaviour
{
    public bool inFight = false;
    private bool alive = true;
    public int regionlable;
    public static Prince Instance
    {
        get
        {
          var prince = FindObjectOfType<Prince>();
            return prince;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getAlive() {
    //maybe this is to check if prince still alive?
    }
}
