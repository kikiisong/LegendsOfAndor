using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;


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
    public FightHUD fHUD;
    public Dice dice;
    public Button myArcherYesButton;
    public Button mySkillYesButton;


    int times;
    int btimes;
    HeroFightController[] aHeroes;
    Monster aMonster;
    HeroFightController thisHero;

    // Use this for initialization
    void Start()
    {
        fightstate = FightState.START;
        StartCoroutine(setUpBattle());
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator setUpBattle()
    {

        //print("disable1");
        mySkillYesButton.gameObject.SetActive(false);
        //print("disable2");
        myArcherYesButton.gameObject.SetActive(false);

        aHeroes = new HeroFightController[heroes.Length];
        for (int i=0;i<heroes.Length;i++) {
            GameObject playerGo = Instantiate(heroes[i], transforms[i]);
            aHeroes[i] = playerGo.GetComponent<HeroFightController>();
         }
        GameObject monsterGo = Instantiate(monster,monsterStation);
        aMonster = monsterGo.GetComponent<Monster>();

       
        mHUD.setMonsterHUD(aMonster);

        //TODO:only initiate yourself for your pannel
        thisHero = aHeroes[0];
        hHUD.setHeroHUD(thisHero);
        fHUD.setFightHUD_START();
        fightstate = FightState.HERO;
        yield return new WaitForSeconds(2f);

        playerturn();
    }

    void playerturn() {
        //roll the dice
        //confirm the action 
        fHUD.setFightHUD_PLAYER();
        times = thisHero.redDice;
        btimes = thisHero.blackDice;
    }

    public FightState getFightState()
    {
        return this.fightstate;

    }


    public void OnRollDice() {
        if (fightstate != FightState.HERO) {
            return;

        }
        StartCoroutine(HeroRoll());

    }


    int diceNum;
    int damage;

    IEnumerator HeroRoll() {
       
        if (thisHero.heroType == Hero.Type.ARCHER) {

            myArcherYesButton.gameObject.SetActive(true);
            if (btimes > 0)
            {
                diceNum = dice.getOne(true);
                btimes--;
                fHUD.rollResult("Value:" + diceNum + " Left B/R:" + btimes + "/" + times);
                
            }
            else if (times > 0)
            {
                diceNum = dice.getOne(false);
                times--;
                fHUD.rollResult("Value:" + diceNum + " Left B/R:" + btimes + "/" + times);
                
            }
            else {
                OnYesClick();
            }
        }
        else {
            mySkillYesButton.gameObject.SetActive(true);
            dice.rollDice(thisHero.redDice, thisHero.blackDice);
            diceNum = dice.getMax();
            fHUD.rollResult(dice.printArrayList()+"Max:"+ diceNum);
        }
        yield return new WaitForSeconds(4f);
        //TODO: initiate a confirm button
    }

    IEnumerator HeroAttack()
    {
        aMonster.Attacked(diceNum);
        print("Work1");
        mHUD.setMonsterHUD(aMonster);
        yield return new WaitForSeconds(4f);
    }


    public void OnYesClick() {
        myArcherYesButton.gameObject.SetActive(false);
        mySkillYesButton.gameObject.SetActive(true);
    }

    public void OnConfirmClick() {
        mySkillYesButton.gameObject.SetActive(false);
        StartCoroutine(HeroAttack());
    }

    public void onMagicClick() {
        //assume black dice is not allowed to flipped
        if (fightstate != FightState.MONSTER && thisHero.magic)
        {
            return;
        }

        thisHero.magic = false;

        if (diceNum < 7)
        {
            diceNum = 7 - diceNum;
        }
        else {
            //donothing
        }
        fHUD.rollResult("Applied Magic:"+diceNum);
    }
    bool usedhelm = false;

    public void onSheildClick() {
        if (fightstate !=FightState.MONSTER && thisHero.sheild&&!usedhelm) {
            return;
        }
        damage = 0;
        thisHero.sheild = false;
        fHUD.rollResult("Applied Sheild:" + damage);

    }

    public void onHelmClick() {
        if (fightstate != FightState.HERO && thisHero.helm)
        {
            return;
        }
        diceNum = dice.getSum();
        usedhelm = true;
        thisHero.helm = false;

        fHUD.rollResult("Applied Helm:" + diceNum);


    }
    
    public void onHerbSClick() {
        if (fightstate != FightState.HERO && thisHero.herbS)
        {
            return;
        }
        //TODO: should fetch data from ..
        diceNum += 2;
        thisHero.herbS = false;
        fHUD.rollResult("Applied Herb on Strength:" + diceNum);
    }

    public void onHerbWClick()
    {
        if (fightstate != FightState.HERO && thisHero.herbW)
        {
            return;
        }
        //TODO: should fetch data from ..
        thisHero.currentWP += 2;
        thisHero.herbW = false;
        fHUD.rollResult("Applied Herb on Will:" + thisHero.currentWP +"/"+ diceNum);
        hHUD.basicInfoUpdate(thisHero);

    }

    public void onBrewWClick()
    {
        if (fightstate != FightState.HERO && thisHero.herbW)
        {
            return;
        }
        //TODO: should fetch data from ..
        diceNum *= 2;
        thisHero.herbW = false;
        fHUD.rollResult("Applied Brew:" + diceNum);


    }

    public void onSkillClick() {
        StartCoroutine(HeroAttack());
    }

    public int MonsterAttack()
    {
        int attack = 0;//roll Dice
        //special event check
        int finalAttack = 0;
        return finalAttack;
        //return the finalAttack
    }
}
