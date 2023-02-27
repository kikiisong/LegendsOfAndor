using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prince : MonoBehaviourPun
{
    public bool inFight = false;
    private bool alive = true;
    public static Prince Instance
    {
        get
        {
            return FindObjectOfType<Prince>();
        }
    }

    public Region CurrentRegion
    {
        get
        {
            return GameGraph.Instance.FindNearest(transform.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject movePrinceButton = GameObject.Find("Actions").transform.Find("MovePrince").gameObject;
        movePrinceButton.SetActive(true);

    }

    void getAlive() {
    //maybe this is to check if prince still alive?
    }
}
