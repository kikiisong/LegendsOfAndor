using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;



public class Monster : MonoBehaviour,TurnManager.IOnSunrise
{
    public int maxWP, maxSP, redDice, currentWP, rewardc, rewardw;
    public bool isFighted;
    public int regionlabel;
    public Dice dice;
    public int damage;
    public void desotry() {
        Destroy(gameObject);
    }
    public void Start()
    {
        TurnManager.Register(this);
        regionlabel = GameGraph.Instance.FindNearest(transform.position).label;
    }

    public void Attacked(int damage)
    {
        
           currentWP -= damage;
        

    }
    public string printRoll()
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

    public void OnSunrise()
    { 
            
        return;
    }


}