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
    public bool isFighted;
    public int regionlabel;
    // Start is called before the first frame update
    void Start()
    {
        regionlabel = GameGraph.Instance.FindNearest(transform.position).label;
        TurnManager.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSunrise()
    {
        try
        {
            Region next = GameGraph.Instance.NextEnemyRegion(GameGraph.Instance.FindNearest(transform.position));
            print("assign regionlabel");
            regionlabel = next.label;
            StartCoroutine(CommonRoutines.MoveTo(transform, next.position, 1));
        }
        catch (GameGraph.NoNextRegionException)
        {
            //damage castle
        }
    }
    public void OnMouseDown()
    {
        isFighted = true;
    }
}
