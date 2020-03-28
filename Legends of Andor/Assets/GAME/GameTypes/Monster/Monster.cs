using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class Monster : MonoBehaviour, TurnManager.IOnSunrise
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
    public void setDice(string a) {
        print("A" + a);
        char[] seperator = {' ' };
        string [] array = a.Split(seperator);
        print(array.ToString());
        List<int> l = new List<int>();
        foreach (string s in array){
            if (Regex.IsMatch(s, @"^\d+$"))
            {
                print(s);
                l.Add(int.Parse(s));
            }
            
        }
       
        dice.setResult(l);
    }

    public List<int> getDice() {
        return dice.getResult();
        }

    public void OnSunrise()
    { 
            
        return;
    }


}