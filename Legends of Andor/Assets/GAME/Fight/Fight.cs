using UnityEngine;
using ExitGames.Client.Photon;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using Routines;
using System.Collections.Generic;

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
        DECISION

    }


public class Fight : MonoBehaviourPun, FightTurnManager.IOnSunrise

//, FightTurnManager.IOnMove
{
    public static Fight Instance;
    public FightState fightstate;

    [Header("Monster")]
    public Transform monsterStation;
    public GameObject monster;

    [Header("HUD")]
    public MonsterHUD mHUD;
    public HeroHUD hHUD;
    public FightHUD fHUD;

    [Header("ArcherSpecial")]
    public Button myArcherYesButton;
    public Button mySkillYesButton;

    [Header("Dice")]
    public Dice dice;
    public List<Hero> aHeroes;

    [Header("Prefabs")]
    public GameObject archerPrefabs;
    public GameObject warriorPrefabs;
    public GameObject dwarfPrefabs;
    public GameObject wizardPrefabs;
    public Transform[] transforms = new Transform[4];

    public Monster aMonster;
    public int diceNum;
    public int damage;

    // Use this for initialization
    void Start()
    {
        if (Instance == null) Instance = this;
        Debug.Log(Instance);
        plotCharacter();
        fightstate = FightState.START;
        FightTurnManager.Register(this);
        StartCoroutine(setUpBattle());
    }



    //--------START--------//
    void plotCharacter() {
        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (player.CustomProperties.ContainsKey(K.Player.isFight))
            {
                Hero hero = (Hero)player.CustomProperties[K.Player.hero];
                aHeroes.Add(hero);
            }
        }

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey(K.Player.isFight))
        {
            Hero hero = (Hero)player.CustomProperties[K.Player.hero];

            switch (hero.type)
            {
                case Hero.Type.ARCHER:
                    GameObject go1 = PhotonNetwork.Instantiate(archerPrefabs);
                    go1.transform.position = transforms[0].position;
                    go1.SetActive(true);
                    break;
                case Hero.Type.WARRIOR:
                    GameObject go2 = PhotonNetwork.Instantiate(warriorPrefabs);
                    go2.transform.position = transforms[1].position;
                    go2.SetActive(true);
                    break;
                case Hero.Type.DWARF:
                    GameObject go3 = PhotonNetwork.Instantiate(dwarfPrefabs);
                    go3.transform.position = transforms[2].position;
                    go3.SetActive(true);
                    break;
                case Hero.Type.WIZARD:
                    GameObject go4 = PhotonNetwork.Instantiate(wizardPrefabs);
                    go4.transform.position = transforms[3].position;
                    go4.SetActive(true);
                    break;
            }

            //TODO: get current Reigon monster
            if (PhotonNetwork.IsMasterClient) {
                GameObject monsterGo = Instantiate(monster, monsterStation);
                aMonster = monsterGo.GetComponent<Monster>();
                hHUD.setHeroHUD(hero);
                mHUD.setMonsterHUD(aMonster);

            }
        }
        

    }

    IEnumerator setUpBattle()
    {
        myArcherYesButton.gameObject.SetActive(false);
        mySkillYesButton.gameObject.SetActive(false);
        fHUD.setFightHUD_START();
        fightstate = FightState.HERO;
        yield return new WaitForSeconds(2f);

        playerTurn();
        yield return new WaitForSeconds(2f);

    }
    //--------HERO--------//
    //--------MESSAGE--------//

    Player player;
    Hero hero;

    public void playerTurn()
    {
        player = PhotonNetwork.LocalPlayer;
        hero = (Hero)player.CustomProperties[K.Player.hero];
        hero.data.times = hero.getDiceNum();
        hero.data.btimes = hero.data.blackDice;
        fHUD.setFightHUD_PLAYER();

    }

    public void OnRollDice()
    {
        //roll the dice
        //confirm the action


        if (fightstate != FightState.HERO || !FightTurnManager.IsMyTurn()
            || !photonView.IsMine || !FightTurnManager.CanFight())
        {
            print("return");
            return;

        }
        print("heroRoll");
        StartCoroutine(HeroRoll(hero));
    }

            //--------ROLL--------//
        IEnumerator HeroRoll(Hero hero)
        {

            if (hero.type == Hero.Type.ARCHER)
            {

                myArcherYesButton.gameObject.SetActive(true);
                if (hero.data.btimes > 0)
                {
                    diceNum = dice.getOne(true);
                    hero.data.btimes -=1;
                    fHUD.rollResult("Value:" + diceNum + " Left B/R:" + hero.data.btimes + "/" + hero.data.times);

                }
                else if (hero.data.times > 0)
                {
                    diceNum = dice.getOne(false);
                    hero.data.times -=1;
                    fHUD.rollResult("Value:" + diceNum + " Left B/R:" + hero.data.btimes + "/" + hero.data.times);

                }
                else
                {
                    OnYesClick();
                }
            }
            else
            {
                mySkillYesButton.gameObject.SetActive(true);
                dice.rollDice(hero.getDiceNum(), hero.data.blackDice);
                diceNum = dice.getMax();
                fHUD.rollResult(dice.printArrayList() + "Max:" + diceNum);
            }
            yield return new WaitForSeconds(4f);
        }


    //--------ATTACK--------//
    IEnumerator HeroAttackFinished()
    {
        print("HeroAttackRunning");
        fightstate = FightState.MONSTER;
        fHUD.setFightHUD_MONSTER();

        yield return new WaitForSeconds(2f);

        MonsterAttack();

    }


    public void MonsterAttack()
    {

        if (fightstate != FightState.MONSTER)
        {
            return;

        }
        StartCoroutine(MonsterRoll());


    }

    IEnumerator MonsterRoll()
    {

        dice.rollDice(aMonster.redDice, 0);
        if (dice.CheckRepet())
        {
            damage = dice.getSum();
        }
        else
        {
            damage = dice.getMax();
        }

        fHUD.rollResult(dice.printArrayList() + "Max:" + damage);
        damage += aMonster.maxSP;
        yield return new WaitForSeconds(4f);
        fightstate = FightState.CHECK;
        fHUD.setFightHUD_CHECK(diceNum, damage);
        StartCoroutine(Check());
        yield return new WaitForSeconds(4f);
    }


    //--------CHECK--------//
    IEnumerator Check()
    {
        //TODO: makesure this wont run twice
        int totalAttack=0;
        foreach (Hero h in aHeroes) {
            totalAttack += h.data.attackNum;
        }
        if (damage > totalAttack)
        {
            hero.Attacked(damage - totalAttack);
        }
        else if (damage < totalAttack)
        {
            aMonster.Attacked(totalAttack - damage);
            mHUD.basicInfo(aMonster);
        }
        yield return new WaitForSeconds(2f);
        if (aMonster.currentWP <= 0)
        {
            fightstate = FightState.WIN;
            fHUD.setFightHUD_WIN();
            yield return new WaitForSeconds(2f);
            Destroy(aMonster);
            SceneManager.LoadSceneAsync("Distribution", LoadSceneMode.Additive);

        }
        else if (hero.data.WP <= 0)
        {
            fightstate = FightState.LOSE;
            fHUD.setFightHUD_LOSE();
            yield return new WaitForSeconds(2f);
            SceneManager.UnloadSceneAsync("FightScene");
            //TODO: 一些参数修改
        }
        else
        {
            fightstate = FightState.DECISION;
            fHUD.setFightHUD_DICISION();
            yield return new WaitForSeconds(2f);
        }


        print("test end");
        //should listen to the event, check if user wanna do something else

    }

            /*bunch of listener*/

    public void OnYesClick()
    {
        myArcherYesButton.gameObject.SetActive(false);
        mySkillYesButton.gameObject.SetActive(true);
    }

    public void onMagicClick()
    {
        //assume black dice is not allowed to flipped
        if (fightstate != FightState.MONSTER && hero.data.magic)
        {
            return;
        }

        hero.data.magic = false;

        if (diceNum < 7)
        {
            diceNum = 7 - diceNum;
        }
        else
        {
            //donothing
        }
        fHUD.rollResult("Applied Magic:" + diceNum);
    }

    bool usedhelm = false;

    public void onSheildClick()
    {
        if (fightstate != FightState.MONSTER && hero.data.sheild && !usedhelm)
        {
            return;
        }
        damage = 0;
        hero.data.sheild = false;
        fHUD.rollResult("Applied Sheild:" + damage);

    }

    public void onHelmClick()
    {
        if (fightstate != FightState.HERO && hero.data.helm)
        {
            return;
        }
        diceNum = dice.getSum();
        usedhelm = true;
        hero.data.helm = false;

        fHUD.rollResult("Applied Helm:" + diceNum);


    }

    public void onHerbSClick()
    {
        if (fightstate != FightState.HERO && hero.data.herbS)
        {
            return;
        }
        //TODO: should fetch data from ..
        diceNum += 2;
        hero.data.herbS = false;
        fHUD.rollResult("Applied Herb on Strength:" + diceNum);
    }

    public void onHerbWClick()
    {
        if (fightstate != FightState.HERO && hero.data.herbW)
        {
            return;
        }
            
    hero.data.WP += 2;
    hero.data.herbW = false;
        fHUD.rollResult("Applied Herb on Will:" + hero.data.WP + "/" + diceNum);
        hHUD.basicInfoUpdate(hero);

    }

    public void onBrewWClick()
    {
        if (fightstate != FightState.HERO && hero.data.herbW)
        {
            return;
        }

        diceNum *= 2;
        hero.data.herbW = false;
        fHUD.rollResult("Applied Brew:" + diceNum);


    }

    public void onSkillClick()
    {
        mySkillYesButton.gameObject.SetActive(false);
        hero.data.attackNum = diceNum+hero.data.SP;
        FightTurnManager.TriggerEvent_Fight();
        print("nextOne");
        FightTurnManager.TriggerEvent_EndFight();
    }

    /*Four button*/
    public void OnLeaveClick()
    {
        if (fightstate != FightState.DECISION)
        {
            return;
        }
        //Initialize the mosnter
        aMonster.currentWP = aMonster.maxWP;
        SceneManager.UnloadSceneAsync("FightScene");

    }

    public void OnConitnueClick()
    {
        if (fightstate != FightState.DECISION)
        {
            return;
        }


        hero.data.times = hero.getDiceNum();
        hero.data.btimes = hero.data.blackDice;
        fightstate = FightState.HERO;
        diceNum = 0;
        damage = 0;
        if (hero.type == Hero.Type.WIZARD)
        {
            hHUD.backColorMagic();
        }

    }

    public void OnFalconClick()
    {
        if (fightstate != FightState.DECISION)
        {
            return;
        }
        //Initialize the mosnter
        print("Falcon");
    }

    public void OnTradeClick()
    {
        if (fightstate != FightState.DECISION)
        {
            return;
        }
        //Initialize the mosnter
        print("Trade");
    }

    public void OnSunrise()
    {
       print("hi");
       StartCoroutine(HeroAttackFinished());
    }
}
    

