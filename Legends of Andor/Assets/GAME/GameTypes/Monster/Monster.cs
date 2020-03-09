using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;



public class Monster : MonoBehaviour
{
    public int maxWP, maxSP, redDice, currentWP, rewardc, rewardw;

    int regionLabel;

    public void desotry() {
        Destroy(gameObject);
    }

    //public int calculateAttack(int dice)
    //{
    //    return dice + maxSP;
    //}

    //public int getMaxSP()
    //{
    //    return this.maxSP;
    //}

    //public int getMaxWP()
    //{
    //    return this.maxWP;
    //}

    //public int getRedDice()
    //{
    //    return this.redDice;
    //}

    //public int getRegionLabel()
    //{
    //    return this.regionLabel;
    //}

    //public void setRegionLabel(int newPosition)
    //{
    //    this.regionLabel = newPosition;
    //}

    //public int getCurrentWP() {
    //    return this.currentWP;
    //}

    //public int getRewardc() {
    //    return this.rewardc;
    //}

    //public int getRewardw() {
    //    return this.rewardw;
    //}


    public void Attacked(int damage)
    {
        
           currentWP -= damage;
        

    }


}