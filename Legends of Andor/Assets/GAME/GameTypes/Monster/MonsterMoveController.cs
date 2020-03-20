using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MonsterMoveController : MonoBehaviourPun, TurnManager.IOnSunrise
{
    public GameObject startGame;
    public GameObject joinFight;

    public bool isFighted; //isFighting?

    public int regionlabel;


    // Start is called before the first frame update
    void Start()
    {
        regionlabel = GameGraph.Instance.FindNearest(transform.position).label; //not reliable, could be 0, can be changed after Start
        TurnManager.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSunrise()
    {
        MoveToNext(); //called in same order? otherwise different results for each player
    }

    void MoveToNext()
    {
        try
        {
            Region next = GameGraph.Instance.NextEnemyRegion(GameGraph.Instance.FindNearest(transform.position));
            regionlabel = next.label;
            StartCoroutine(CommonRoutines.MoveTo(transform, next.position, 1, atEnd:()=> {
                if (MonsterOnRegion()) MoveToNext();
            }));
        }
        catch (GameGraph.NoNextRegionException)
        {
            
        }
    }
    public void OnMouseDown()
    {
        isFighted = true;
    }

    bool MonsterOnRegion()
    {
        foreach(MonsterMoveController monster in GameObject.FindObjectsOfType<MonsterMoveController>())
        {
            if (monster != this && regionlabel == monster.regionlabel) return true;
        }
        return false;
    }
}
