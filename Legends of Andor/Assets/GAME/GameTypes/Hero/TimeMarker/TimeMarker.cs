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
            Hero hero = (Hero)photonView.Owner.CustomProperties[K.Player.hero];
            Vector3 position = GameMapManager.Instance.timeMarkerTransforms[hero.data.numHours].position;
            transform.position = position;
            hero.data.numHours++;
        }
    }
}
