using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] ExtraShield extraShiled;
    [SerializeField] GameGraph gameGraph;

    private void Start()
    {
        extraShiled = GameObject.Find("ExtraShield").GetComponent<ExtraShield>();
        gameGraph = GameObject.Find("Graph").GetComponent<GameGraph>();
    }

    // Update is called once per frame
    void Update()
    {
        isGameEnd();
    }

    public void isGameEnd()
    {
        Region temp = gameGraph.FindNearest(gameObject.transform.position);
        List<MonsterMoveController> monsterOnRegion = gameGraph.FindObjectsOnRegion<MonsterMoveController>(temp);

        //if(monsterOnRegion.Count >= 3)
        //{
        //    print("Game Over");
        //}
    }
}
