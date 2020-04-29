using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using Monsters;

public class Monster : MonoBehaviourPun
{
    public int maxWP, maxSP, redDice, rewardc, rewardw;
    public Dice dice;
    public bool isTower;
    public void Destroy() {
        Destroy(gameObject);
    }

    public void Start()
    {
        
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


    public bool isSkralOnTower()
    {
        Region temp = GameGraph.Instance.FindNearest(gameObject.transform.position);
        if (temp.label <= 56 && temp.label >= 51)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int returnSkralOnTowerSP()
    {
        int tempSP = 0;
        Player[] players = PhotonNetwork.PlayerList;
        int numberOfPlayer = players.Length;
        if (Room.Difficulty == Difficulty.Easy)
        {
            if (numberOfPlayer == 2)
            {
                tempSP = 10;
            }
            else if (numberOfPlayer == 3)
            {
                tempSP = 20;
            }
            else if (numberOfPlayer == 4)
            {
                tempSP = 30;
            }
        }
        else
        {
            if (numberOfPlayer == 2)
            {
                tempSP = 20;
            }
            else if (numberOfPlayer == 3)
            {
                tempSP = 30;
            }
            else if (numberOfPlayer == 4)
            {
                tempSP = 40;
            }
        }
        return tempSP;
    }

    public string PrintRoll()
    {
        return dice.printArrayList();
    }

    //public int MonsterRoll()
    //{

    //    dice.rollDice(redDice, 0);
    //    if (dice.CheckRepet())
    //    {
    //        damage = dice.getSum();
    //    }
    //    else
    //    {
    //        damage = dice.getMax();
    //    }
    //    return damage;
    //}

    //public void SetDice(string a) {
    //    char[] seperator = {' ' };
    //    string [] array = a.Split(seperator);
    //    List<int> l = new List<int>();
    //    foreach (string s in array){
    //        if (Regex.IsMatch(s, @"^\d+$"))
    //        {
    //            print(s);
    //            l.Add(int.Parse(s));
    //        } 
    //    }
    //    dice.setResult(l);
    //    if (dice.CheckRepet())
    //    {
    //        damage = dice.getSum();
    //    }
    //    else
    //    {
    //        damage = dice.getMax();
    //    }
    //    print(this.damage);
    //}

    public List<int> GetDice() {
        return dice.getResult();
    }
}