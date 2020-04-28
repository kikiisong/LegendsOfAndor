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
            var prince = FindObjectsOfType<Prince>();
            if (prince != null) { 
            if (prince.Length > 1) Debug.LogWarning("More than one prince " + prince[1].transform.position);
            return prince[0];
            }
            return null;
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
