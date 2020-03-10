using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterMoveController : MonoBehaviourPun, TurnManager.IOnSunrise
{

    int regionlabel;
    // Start is called before the first frame update
    void Start()
    {
        regionlabel = GameGraph.Instance.FindNearest(transform.position).label;
        TurnManager.Register(this);
        print(regionlabel);
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
        print("monster is clicked");
        Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        
        print(hero.data.regionNumber);
        int current = hero.data.regionNumber;
        if (photonView.IsMine && TurnManager.IsMyTurn()
            && Input.GetMouseButtonDown(0) && current == regionlabel)
        {
            SceneManager.LoadSceneAsync("FightScene", LoadSceneMode.Additive);
        }
    }
}
