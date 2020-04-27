using Monsters;
using Newtonsoft.Json.Linq;
using Photon.Pun;
using Routines;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MonsterMoveController : MonoBehaviourPun
{
    public MonsterType type;
    public MonsterData data;
    public bool canMove = true;
    public bool hasHerb = false;
    public int maxWP, maxSP, redDice, currentWP, rewardc, rewardw;
    public bool isFighted;
    public Dice dice;
    public int damage;
    public bool isTower;
    //public Monster m;

    CoroutineQueue coroutineQueue;

    //Getters
    public Region CurrentRegion
    {
        get
        {
            return GameGraph.Instance.FindNearest(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        coroutineQueue = new CoroutineQueue(this);
        coroutineQueue.StartLoop();
        if (!canMove) {
            isTower = true;
        }
    }

    public void InitRPC(JToken jToken)
    {
        photonView.RPC("InitMonster", RpcTarget.All, jToken.ToString());
    }

    [PunRPC]
    public void InitMonster(string json)
    {
        var j = JObject.Parse(json);
        data = j.ToObject<Monsters.MonsterData>();
    }

    public void MoveAlongPath(List<Region> path)
    {
        foreach (var region in path)
        {
            coroutineQueue.Enqueue(CommonRoutines.MoveTo(transform, region.position, 1));
        }
    }

    public void InitRPC(MonsterData data)
    {
        photonView.RPC("InitMonster", RpcTarget.All, data.wp, data.sp);
    }

    [PunRPC]
    public void InitMonster(int wp, int sp)
    {
        maxWP = wp;
        maxSP = sp;
    }

    public void Attacked(int damage)
    {
        currentWP -= damage;
        print("CurrentWP" + currentWP);
    }


    public string PrintRoll()
    {
        return dice.printArrayList();
    }

    public void MonsterRoll()
    {

        dice.rollDice(redDice, 0);
        if (dice.CheckRepet())
        {
            damage = dice.getSum();
        }
        else
        {
            damage = dice.getMax();
        }
    }

    public void SetDice(string a)
    {
        char[] seperator = { ' ' };
        string[] array = a.Split(seperator);
        List<int> l = new List<int>();
        foreach (string s in array)
        {
            if (Regex.IsMatch(s, @"^\d+$"))
            {
                print(s);
                l.Add(int.Parse(s));
            }
        }
        dice.setResult(l);
        if (dice.CheckRepet())
        {
            damage = dice.getSum();
        }
        else
        {
            damage = dice.getMax();
        }
        print(this.damage);
    }

    public List<int> GetDice()
    {
        return dice.getResult();
    }
}
