using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prince : MonoBehaviour
{
    public bool inFight = false;
    private bool alive = true;
    public Region r;
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
        GameObject movePrinceButton = GameObject.Find("Actions").transform.Find("MovePrince").gameObject;
        movePrinceButton.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        r = GameGraph.Instance.FindNearest(this.transform.position);
        //print("Prince" +r.label);
    }

    

    void getAlive() {
    //maybe this is to check if prince still alive?
    }
}
