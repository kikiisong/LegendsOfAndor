using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Routines;


public class TimeMarker : MonoBehaviourPun, TurnManager.IOnMove, TurnManager.IOnTurnCompleted, TurnManager.IOnEndDay
{
    CoroutineQueue coroutineQueue;

    // Start is called before the first frame update
    void Start()
    {
        int i = TurnManager.Instance.GetWaitIndex(photonView.Owner);
        if(i != -1)
        {
            transform.position = GameMapManager.Instance.timeMarkerInitialPositions[i].position;
        }
        TurnManager.Register(this);
        coroutineQueue = new CoroutineQueue(this);
        coroutineQueue.StartLoop();
    }
    

    public void OnMove(Player player, Region currentRegion)
    {
        if (photonView.Owner == player) {
            Hero hero = (Hero)player.CustomProperties[K.Player.hero];
            var transforms = GameMapManager.Instance.timeMarkerUpdatePositions;
            var position = transforms[hero.data.numHours - 1].position;
            coroutineQueue.Enqueue(CommonRoutines.MoveTo(transform, position, 2));
        }
        else
        {   
            int i = TurnManager.Instance.GetWaitIndex(photonView.Owner);
            if (i != -1)
            {
                Vector3 waitPosition = GameMapManager.Instance.timeMarkerInitialPositions[i].position;
                coroutineQueue.Enqueue(CommonRoutines.MoveTo(transform, waitPosition, 2));
            }            
        }
    }

    public void OnEndDay(Player player)
    {
        if (photonView.Owner == player)
        {
            Hero hero = (Hero)player.CustomProperties[K.Player.hero];
            hero.data.numHours = 0;
            int i = TurnManager.Instance.GetWaitIndex(photonView.Owner);
            Vector3 waitPosition = GameMapManager.Instance.timeMarkerInitialPositions[i].position;
            coroutineQueue.Enqueue(CommonRoutines.MoveTo(transform, waitPosition, 2));           
        }
    }

    public void OnTurnCompleted(Player player)
    {
        if (photonView.Owner == player)
        {
            Hero hero = (Hero)player.CustomProperties[K.Player.hero];
            List<Transform> transforms = GameMapManager.Instance.timeMarkerUpdatePositions;
            if (photonView.IsMine && hero.data.numHours == transforms.Count)
            {
                //or buy more hours, do it in OnEndTurn instead
                TurnManager.TriggerEvent_EndDay(player);
            }
        }
    }
}
