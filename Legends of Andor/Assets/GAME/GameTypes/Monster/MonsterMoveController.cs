using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterMoveController : MonoBehaviourPun, TurnManager.IOnSunrise
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

    public void OnSunrise()
    {
        try
        {
            Region next = GameGraph.Instance.NextEnemyRegion(GameGraph.Instance.FindNearest(transform.position));
            StartCoroutine(CommonRoutines.MoveTo(transform, next.position, 1));
        }
        catch (GameGraph.NoNextRegionException)
        {
            //damage castle
        }
    }

    public void OnMouseDown()
    {
        //In order to enter the fight scene
        print("called");
        SceneManager.LoadSceneAsync("FightScene", LoadSceneMode.Additive);
    }
}
