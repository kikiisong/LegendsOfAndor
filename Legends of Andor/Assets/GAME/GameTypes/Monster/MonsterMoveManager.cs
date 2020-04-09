using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterMoveManager : MonoBehaviour, TurnManager.IOnSunrise
{
    public static List<MonsterMoveController> MonstersInOrder
    {
        get
        {
            var monsters = new List<MonsterMoveController>(FindObjectsOfType<MonsterMoveController>());
            monsters.OrderBy(m => m.type).ThenBy(m => m.CurrentRegion);
            var wardraks = monsters.Where(m => m.type == Monsters.MonsterType.Wardrak).OrderBy(m => m.CurrentRegion);
            monsters.AddRange(wardraks);
            return monsters;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TurnManager.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSunrise()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            MoveToNext();
        }
    }

    void MoveToNext()
    {
       
        var taken = new List<Region>();
        foreach (var m in MonstersInOrder)
        {
            var path = new List<Region>();
            try
            {
                var region = m.CurrentRegion;
                while (true)
                {
                    var next = GameGraph.Instance.NextEnemyRegion(region);
                    path.Add(next);
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
            catch (GameGraph.NoNextRegionException)
            {

            }
            finally
            {
                m.MoveAlongPath(path);
            }
        }     
    }
}
