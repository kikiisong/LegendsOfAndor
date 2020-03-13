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
        //print(regionlabel);
        //startGame = GameObject.Find("StartFight");
        //Debug.Log(startGame);
        //joinFight = GameObject.Find("JoinFight");
        //Debug.Log(joinFight);
        //startGame.SetActive(true);
        //joinFight.SetActive(true);
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
    //    //print("monster is clicked");
    //    //Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
    //    //print(hero.data.regionNumber);
    //    //int current = hero.data.regionNumber;
    //    //if (photonView.IsMine && TurnManager.IsMyTurn()
    //    //    && Input.GetMouseButtonDown(0) && current == regionlabel)
    //    //{
            
    //    //}

    //    //if some list is empty maybe subrotine for ask for join
    //    //ADD this hero into a main list
    //    //Display a Join Button on all
    //    //finalized 
    //    //SceneManager.LoadSceneAsync("FightScene", LoadSceneMode.Additive);
    
    }
}
