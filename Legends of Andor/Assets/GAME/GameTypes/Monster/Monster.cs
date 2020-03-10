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

    public int regionLabel;
    public Dice dice;
    int damage;
    public void desotry() {
        Destroy(gameObject);
    }
    public void Start()
    {
        TurnManager.Register(this);
    }

    public void Attacked(int damage)
    {
        
           currentWP -= damage;
        

    }
    public string printRoll()
    {

        return dice.printArrayList();
    }

    public int MonsterRoll()
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
        return damage;
    }

    public void OnSunrise()s
    {
        return;
    }


}