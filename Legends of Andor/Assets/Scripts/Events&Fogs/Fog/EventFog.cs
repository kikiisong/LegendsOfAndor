using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFog : Fog
{
    public EventCardController myEvents;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void uncover()
    {
        myEvents.flipped();
    }
}
