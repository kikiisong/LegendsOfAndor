using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;



public class Monster : MonoBehaviour
{
    [Header("Basic INFO")]
    [SerializeField]
    int maxWP, maxSP, redDice, blackDice;

    int regionLabel;
    int currentWP;

    public int calculateAttack(int dice)
    {
        return dice + maxSP;
    }

    public int getMaxSP()
    {
        return this.maxSP;
    }

    public int getMaxWP()
    {
        return this.maxWP;
    }

    public int getRedDice()
    {
        return this.redDice;
    }

    public int getBlackDice()
    {
        return this.blackDice;
    }

    public int getRegionLabel()
    {
        return this.regionLabel;
    }

    public void setRegionLabel(int newPosition)
    {
        this.regionLabel = newPosition;
    }

    public void Attacked(int damage)
    {
        if (currentWP == 0)
        {
            //not has been initialized case
            currentWP = maxSP - damage;
        }
        else {
            currentWP -= damage;
        }

    }


}