using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


public class TimeMarker : MonoBehaviourPun, TurnManager.IOnMove, TurnManager.IOnEndDay
{
    Vector3 startingPosition;

    // Start is called before the first frame update
    void Start()
    {
        TurnManager.Register(this);
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(Player player, Region currentRegion)
    {
        if (photonView.Owner == player) {
            Hero hero = (Hero)photonView.Owner.CustomProperties[K.Player.hero];
            List<Transform> transforms = GameMapManager.Instance.timeMarkerTransforms;
            Vector3 position = transforms[hero.data.numHours].position;
            transform.position = position;
            hero.data.numHours++;
            if(photonView.IsMine && hero.data.numHours == transforms.Count)
            {
                //or buy more hours, do it in OnEndTurn instead
                TurnManager.TriggerEvent_EndDay(player);
            }
        }
    }

    public void OnEndDay(Player player)
    {
        if (photonView.Owner == player)
        {

            int i = TurnManager.Instance.GetWaitIndex(photonView.Owner);
            Vector3 waitPosition = GameMapManager.Instance.timeMarkerInitialPositions[i].position;
            coroutineQueue.Enqueue(CommonRoutines.MoveTo(transform, waitPosition, 2));           

            Hero hero = (Hero)photonView.Owner.CustomProperties[K.Player.hero];
            hero.data.numHours = 0;
            transform.position = startingPosition;

        }
    }
}
