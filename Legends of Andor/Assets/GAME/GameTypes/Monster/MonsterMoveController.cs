using Monsters;
using Newtonsoft.Json.Linq;
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
    public MonsterData data;
    public bool canMove = true;
    public bool hasHerb = false;

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
        if (!canMove) {
            m.isTower = true;
        }
    }

    public void InitRPC(JToken jToken)
    {
        photonView.RPC("InitMonster", RpcTarget.All, jToken.ToString());
    }

    [PunRPC]
    public void InitMonster(string json)
    {
        var j = JObject.Parse(json);
        data = j.ToObject<MonsterData>();
    }

    public void MoveAlongPath(List<Region> path)
    {
        foreach (var region in path)
        {
            coroutineQueue.Enqueue(CommonRoutines.MoveTo(transform, region.position, 1));
        }
    }
}
