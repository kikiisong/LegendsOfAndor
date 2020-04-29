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
public class Castle : MonoBehaviourPun
{
    public static Castle Instance;
    public ExtraShield extraShiled;
    [SceneName] public string nextScene;
    [SceneName] public string winScene;
    // [SerializeField] public GameGraph gameGraph;
    public bool isSkralOntowelDefeaded;
    public int numberOfPlayers;
    public GameObject winManager;

    private void Start()
    {
        GameGraph.Instance.PlaceAt(gameObject, 0);


        // According to the number of players the shield should be different.
        numberOfPlayers = PhotonNetwork.PlayerList.Length;
        if (!Room.IsSaved)
        {
            setInitialShield();
        }
        
    }

    public void setShieldNum(int numOfS)
    {
        extraShiled.numberOfShileds = numOfS;
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
        monsterMove();
    }

    public void monsterMove()
    {
        
        Region temp = GameGraph.Instance.FindNearest(gameObject.transform.position);
        List<MonsterMoveController> monsterOnRegion = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(temp);

//        print("The number of monsters on the region castle is " + monsterOnRegion.Count);
        if (PhotonNetwork.IsMasterClient)
        {
            if (monsterOnRegion.Count > 0)
            {
                if (extraShiled.numberOfShileds > 0)
                {
                    int differenceBetweenShieldAndMonsters = extraShiled.numberOfShileds - monsterOnRegion.Count;
                    if (differenceBetweenShieldAndMonsters < 0)
                    {
                      //  print("game is over");
                        PhotonNetwork.LoadLevel(nextScene);
                    }
                    else
                    {
                        
                        photonView.RPC("decreseTheShield", RpcTarget.AllBuffered, monsterOnRegion.Count);
                        print("The monster number is " + monsterOnRegion.Count);
                    }
                }
                else
                {
                   //  print("game is over");
                   PhotonNetwork.LoadLevel(nextScene);
                }
            }
        }
    }


    // test the ground bag see if there is a herb in it
    public bool isHerbBack()
    {
        Region r = GameGraph.Instance.Find(0);
        bool hasHerb = (r.data.herb > 0) ? true : false;
        return hasHerb;
    }

    public void tellCastle()
    {
        photonView.RPC("defeatTheSkralOnMonster", RpcTarget.AllBuffered);
    }

    // After the skrall on tower is defeated, this function should be triggered
    [PunRPC]
    public void defeatTheSkralOnMonster()
    {
        isSkralOntowelDefeaded = true;
        if (winManager.GetComponent<WinManager>().checkIsWinning())
        {
            // load win sence
            PhotonNetwork.LoadLevel(winScene);
        }
        else
        {
            // load false sence
            PhotonNetwork.LoadLevel(nextScene);
        }
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
        if (PhotonNetwork.IsMasterClient) {
            Region temp = GameGraph.Instance.FindNearest(gameObject.transform.position);
            List<MonsterMoveController> monsterOnRegion = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(temp);
            for (int i = 0; i < monsterOnRegion.Count; i++)
            {
                PhotonNetwork.Destroy(monsterOnRegion[i].gameObject);
            }
        }
    }
}

