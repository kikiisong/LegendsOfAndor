using Monsters;
using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMoveController : MonoBehaviourPun, TurnManager.IOnSunrise
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

    public static List<MonsterMoveController> MonstersInOrder
    {
        get
        {
            var monsters = new List<MonsterMoveController>(FindObjectsOfType<MonsterMoveController>());
            monsters.OrderBy(m => m.type).ThenBy(m => m.CurrentRegion);
            return monsters;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TurnManager.Register(this);
        coroutineQueue = new CoroutineQueue(this);
        coroutineQueue.StartLoop();
    }

    public void OnSunrise()
    {
        MoveToNext();
    }

    void MoveToNext()
    {
        try
        {
            var path = new List<Region>();
            var taken = new List<Region>();
            foreach (var m in MonstersInOrder) //(inneficient)
            {
                if (m != this) //monsters before you
                {
                    var region = m.CurrentRegion;
                    while (true)
                    {
                        var next = GameGraph.Instance.NextEnemyRegion(region);
                        if (!taken.Contains(next))
                        {
                            taken.Add(next);
                            break;
                        }
                        else
                        {
                            region = next;
                        }
                    }
                }
                else //me
                {
                    var region = m.CurrentRegion;
                    while (true)
                    {
                        var next = GameGraph.Instance.NextEnemyRegion(region);
                        path.Add(next);
                        if (!taken.Contains(next)) break;
                        region = next;                        
                    }
                    break;
                }
            }

            //Move
            foreach(var region in path)
            {
                coroutineQueue.Enqueue(CommonRoutines.MoveTo(transform, region.position, 1));
            }
        }
        catch (GameGraph.NoNextRegionException)
        {
            
        }
    }
}
