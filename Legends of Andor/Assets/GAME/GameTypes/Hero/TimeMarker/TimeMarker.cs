using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Routines;


public class TimeMarker : MonoBehaviourPun, TurnManager.IOnMove, TurnManager.IOnTurnCompleted, TurnManager.IOnEndDay
{
    Vector3 startingPosition;

    CoroutineQueue coroutineQueue;

    // Start is called before the first frame update
    void Start()
    {
        TurnManager.Register(this);
        startingPosition = transform.position;
        coroutineQueue = new CoroutineQueue(this);
        coroutineQueue.StartLoop();
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
            coroutineQueue.Enqueue(CommonRoutines.MoveTo(transform, position, 2));
            hero.data.numHours++;
            if(photonView.IsMine && hero.data.numHours == transforms.Count)
            {
                //or buy more hours, do it in OnEndTurn instead
                TurnManager.TriggerEvent_EndTurn(player);
            }
        }
    }

    public void OnEndDay(Player player)
    {
        if (photonView.Owner == player)
        {
            Hero hero = (Hero)photonView.Owner.CustomProperties[K.Player.hero];
            hero.data.numHours = 0;
            coroutineQueue.Enqueue(CommonRoutines.MoveTo(transform, startingPosition, 2));           
        }
    }

    public void OnTurnCompleted(Player player)
    {
        if (photonView.Owner == player)
        {
            Hero hero = (Hero)photonView.Owner.CustomProperties[K.Player.hero];
            List<Transform> transforms = GameMapManager.Instance.timeMarkerTransforms;
            if (photonView.IsMine && hero.data.numHours == transforms.Count)
            {
                //or buy more hours, do it in OnEndTurn instead
                TurnManager.TriggerEvent_EndDay(player);
            }
        }
    }
}
