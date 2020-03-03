using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TimeMarker : MonoBehaviourPun, TurnManager.IOnMove
{
    // Start is called before the first frame update
    void Start()
    {
        TurnManager.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(Player player, Region currentRegion)
    {
        if (photonView.Owner == player) {
            transform.position += Vector3.up;
        }
    }
}
