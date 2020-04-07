using Monsters;
using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMoveController : MonoBehaviourPun
{
    public MonsterType type;

    public Monster m;

    CoroutineQueue coroutineQueue;

    //Getters
    public Region CurrentRegion
    {
        get
        {
            return GameGraph.Instance.FindNearest(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        coroutineQueue = new CoroutineQueue(this);
        coroutineQueue.StartLoop();
    }

    public void MoveAlongPath(List<Region> path)
    {
        foreach (var region in path)
        {
            coroutineQueue.Enqueue(CommonRoutines.MoveTo(transform, region.position, 1));
        }
    }
}
