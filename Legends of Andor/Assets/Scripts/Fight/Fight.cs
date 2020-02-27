using UnityEngine;
using System.Collections;

public class Fight : MonoBehaviour
{
   // FightState fightstate;
    //Hero[] heroes;
   // Monster monster;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    /*
    //public FightState getFightState()
    //{
    //    return this.state;

    //}*/
    

    public int MonsterAttack() {
        int attack = 0;//roll Dice
        //special event check
        int finalAttack= monster.calculateAttack(attack);
        return finalAttack;
        //return the finalAttack
    }
}
