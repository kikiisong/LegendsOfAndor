/*using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Castle : MonoBehaviour
{
    //[SerializeField] public ExtraShield extraShiled;
    //[SceneName] public string nextScene;
  //  [SerializeField] public GameGraph gameGraph;

    private void Start()
    {
        GameGraph.Instance.PlaceAt(gameObject, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Castle.isGameEnd();
    }

    public void isGameEnd()
    {
        Region temp = GameGraph.Instance.FindNearest(gameObject.transform.position);
        List<MonsterMoveController> monsterOnRegion = GameGraph.Instance.FindObjectsOnRegion<MonsterMoveController>(temp);

        if (PhotonNetwork.IsMasterClient)
        {
            if (monsterOnRegion.Count > 0)
            {
              if (extraShiled.numberOfShileds > 0)
                {
                    int differenceBetweenShieldAndMonsters = extraShiled.numberOfShileds - monsterOnRegion.Count;
                    if (differenceBetweenShieldAndMonsters < -3)
                    {
                        //print("game is over");
                        PhotonNetwork.LoadLevel(nextScene);
                    }
                }
                else if (monsterOnRegion.Count >= 3)
                {
                    print("game is over");
                    PhotonNetwork.LoadLevel(nextScene);
                }
            }
        }
    }
}
*/
