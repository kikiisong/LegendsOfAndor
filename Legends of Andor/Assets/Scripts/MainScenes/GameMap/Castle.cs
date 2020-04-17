using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

// the castle needs to have
// 1. function returns how many monsters are here on the sun rise
// 2. function that tells you if there is a herb in here
// 3. increase and decrease the shields number
public class Castle : MonoBehaviourPun, TurnManager.IOnSunrise
{
    public ExtraShield extraShiled;
    [SceneName] public string nextScene;
    // [SerializeField] public GameGraph gameGraph;
    public bool isSkralOntowelDefeaded;
    public int numberOfPlayers;

    private void Start()
    {
        GameGraph.Instance.PlaceAt(gameObject, 0);

        TurnManager.Register(this);

        // According to the number of players the shield should be different.
        numberOfPlayers = PhotonNetwork.PlayerList.Length;
        setInitialShield();
    }

    private void setInitialShield()
    {
       if(numberOfPlayers == 2)
        {
            extraShiled.numberOfShileds = 3;
        }
       else if(numberOfPlayers == 3)
        {
            extraShiled.numberOfShileds = 2;
        }
       else if(numberOfPlayers == 4)
        {
            extraShiled.numberOfShileds = 1;
        }
        else
        {
            extraShiled.numberOfShileds = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Castle.isGameEnd();
    }

    public void OnSunrise()
    {
        
        Region temp = GameGraph.Instance.FindNearest(gameObject.transform.position);
        List<MonsterMoveController> monsterOnRegion = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(temp);

        print("The number of monsters on the region castle is " + monsterOnRegion.Count);
        if (PhotonNetwork.IsMasterClient)
        {
            if (monsterOnRegion.Count > 0)
            {
                if (extraShiled.numberOfShileds > 0)
                {
                    int differenceBetweenShieldAndMonsters = extraShiled.numberOfShileds - monsterOnRegion.Count;
                    if (differenceBetweenShieldAndMonsters < 0)
                    {
                        print("game is over");
                     //   PhotonNetwork.LoadLevel(nextScene);
                    }
                    else
                    {
                        
                        photonView.RPC("decreseTheShield", RpcTarget.AllBuffered, monsterOnRegion.Count);
                    }
                }
                else
                {
                    print("game is over");
                   // PhotonNetwork.LoadLevel(nextScene);
                }
            }
        }
    }


    // test the ground bag see if there is a herb in it
    public bool isHerbBack()
    {
        return false;
    }

    // After the skrall on tower is defeated, this function should be triggered
    public void defeatTheSkralOnMonster()
    {
        isSkralOntowelDefeaded = true;
    }

    // test if the skral on tower is defeated
    public bool isSkralDefeated()
    {
        return isSkralOntowelDefeaded;
    }

    // you need to decrease the shield number and also make the mosters disappear on the map.
    [PunRPC]
    public void decreseTheShield(int a)
    {
        extraShiled.numberOfShileds = extraShiled.numberOfShileds - a;
        Region temp = GameGraph.Instance.FindNearest(gameObject.transform.position);
        List<MonsterMoveController> monsterOnRegion = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(temp);
        for(int i = 0; i < monsterOnRegion.Count; i++)
        {
            monsterOnRegion[i].gameObject.SetActive(false);
        }
    }


}

