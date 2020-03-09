using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Hero;

public class HeroFightController : MonoBehaviour
{
    
    public Type heroType;
    public int  maxWP,currentWP,currentSP,redDice,blackDice;
    public bool magic, herbS,brew,helm,sheild,herbW,bow;



    public void Attacked(int damage) {
        currentWP -= damage;
    }

}
