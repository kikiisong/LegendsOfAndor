using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : MonoBehaviour
{
    //public Renderer herbIcon;
    //public bool found = true;
    // Start is called before the first frame update
    void Start()
    {
        HerbHandler herbManager = GameObject.FindGameObjectWithTag("manager").GetComponent<HerbHandler>();
        herbManager.myHerb = gameObject;
        Region r = GameGraph.Instance.FindNearest(gameObject);
        r.data.herb += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getRegion()
    {
        return GameGraph.Instance.FindNearest(gameObject).label;
    }
}
