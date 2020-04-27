using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Fog : MonoBehaviourPun
{
    public int region;
    public FogType type;
    public EventCardController myEvents;
    public Renderer fogIcon;

    // Start is called before the first frame update
    void Start()
    {
        fogIcon = GetComponent<Renderer>();
        //type = FogType.SP;
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    

    

}
