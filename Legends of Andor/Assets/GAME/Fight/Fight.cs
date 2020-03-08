using UnityEngine;
using System.Collections;

public enum FightState
{
    START,
    HERO,
    //hero roll dice
    MONSTER,
    //monster roll dice
    CHECK,
    //check who wins
    COOP,
    //wait for other seletction
    WIN,
    //label if hero wins
    LOSE,
   
}

public class Fight : MonoBehaviour
{
    public FightState fightstate;

    public GameObject[] heroes= new GameObject[4];
   
    public Transform[] transforms = new Transform[4];

    public Transform monsterStation;
    public GameObject monster;
    public MonsterHUD mHUD;
    public HeroHUD hHUD;

    HeroFightController[] aHeroes;
    Monster aMonster;


    // Use this for initialization
    void Start()
    {
        fightstate = FightState.START;
        setUpBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void setUpBattle()
    {   aHeroes = new HeroFightController[heroes.Length];
        for (int i=0;i<heroes.Length;i++) {
            GameObject playerGo = Instantiate(heroes[i], transforms[i]);
            aHeroes[i] = playerGo.GetComponent<HeroFightController>();
         }
        GameObject monsterGo = Instantiate(monster,monsterStation);
        aMonster = monsterGo.GetComponent<Monster>();

        fightstate = FightState.HERO;
        mHUD.setMonsterHUD(aMonster);
        //TODO:only initiate yourself
        hHUD.setMonsterHUD(aHeroes[1]);
    }


    public FightState getFightState()
    {
        return this.fightstate;

    }

    public int MonsterAttack() {
        int attack = 0;//roll Dice
        //special event check
        int finalAttack = 0;
        return finalAttack;
        //return the finalAttack
    }
}
