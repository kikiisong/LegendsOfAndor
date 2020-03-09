using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using static Hero;

public class HeroFightController : MonoBehaviourPun
{
    
    public Type heroType;
    public int  maxWP,currentWP,currentSP,redDice,blackDice;
    public bool magic, herbS,brew,helm,sheild,herbW,bow;

    int times;
    int btimes;

    public void Start()
    {
        times = getDiceNum();
        btimes = blackDice;
    }


    public int getDiceNum() {
        switch (heroType) {
            case (Type.ARCHER):
                if (currentWP > 13)
                {
                    return 5;
                }
                else if (currentWP > 6)
                {
                    return 4;
                }
                else {
                    return 3;
                }
            case (Type.DWARF):
                if (currentWP > 13) {
                    return 3;
                }
                else if (currentWP > 6)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            case (Type.WARRIOR):
                if (currentWP > 13)
                {
                    return 4;
                }
                else if (currentWP > 6)
                {
                    return 3;
                }
                else
                {
                    return 2;
                }
            default:
                return 1;
        }
    }
    public void Attacked(int damage) {
        currentWP -= damage;
    }

}
