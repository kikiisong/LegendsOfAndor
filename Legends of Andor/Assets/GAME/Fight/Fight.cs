using UnityEngine;
using ExitGames.Client.Photon;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

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


public class Fight : MonoBehaviourPun
{
    public FightState fightstate;

    public GameObject archerPrefabs;
    public GameObject warriorPrefabs;
    public GameObject dwarfPrefabs;
    public GameObject wizardPrefabs;

    public GameObject[] heroes = new GameObject[4];

    public Transform[] transforms = new Transform[4];

    public Transform monsterStation;
    public GameObject monster;
    public MonsterHUD mHUD;
    public HeroHUD hHUD;
    public FightHUD fHUD;
    public Button myArcherYesButton;
    public Button mySkillYesButton;

    HeroFightController[] aHeroes;
    Monster aMonster;
    HeroFightController thisHero;




    // Use this for initialization
    void Start()
    {
        fightstate = FightState.START;
        StartCoroutine(setUpBattle());
    }


    //--------START--------//

    IEnumerator setUpBattle()
    {
        aHeroes = new HeroFightController[heroes.Length];
        mySkillYesButton.gameObject.SetActive(false);
        myArcherYesButton.gameObject.SetActive(false);
        int i = 0;
        if (PhotonNetwork.LocalPlayer.IsMasterClient &&
            (bool) PhotonNetwork.LocalPlayer.CustomProperties[K.Player.isFight])
        {

            HeroFightController hero = (HeroFightController)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
            thisHero = hero; 

            switch (hero.heroType)
            {
                case Hero.Type.ARCHER:
                    Instantiate(archerPrefabs, transforms[0]);
                    break;
                case Hero.Type.WARRIOR:
                    Instantiate(warriorPrefabs, transforms[1]);
                    break;
                case Hero.Type.DWARF:
                    Instantiate(dwarfPrefabs, transforms[2]);
                    break;
                case Hero.Type.WIZARD:
                    Instantiate(wizardPrefabs, transforms[3]);
                    break;
            }
        }

        GameObject playerGo = Instantiate(heroes[i], transforms[i]);
        aHeroes[i] = playerGo.GetComponent<HeroFightController>();
        
        //TODO: get this monster from Photon
        GameObject monsterGo = Instantiate(monster, monsterStation);
        aMonster = monsterGo.GetComponent<Monster>();


        mHUD.setMonsterHUD(aMonster);

        //TODO:only initiate yourself for your pannel
        //TODO:Does PHOTON know this?
        thisHero = aHeroes[0];
        hHUD.setHeroHUD(thisHero);
        fHUD.setFightHUD_START();
        fightstate = FightState.HERO;
        yield return new WaitForSeconds(2f);
        print("Here");
        playerturn();
    }


    //--------HERO--------//
    //--------MESSAGE--------//
    void playerturn()
    {
        //roll the dice
        //confirm the action
        fHUD.setFightHUD_PLAYER();
        //TODO: turn maganer

    }

 

    public void OnRollDice()
    {
        thisHero.OnRollDice(myArcherYesButton, mySkillYesButton, fHUD);

    }

    int diceNum;
    int damage;

    //--------ROLL--------//

    //--------ATTACK--------//
    IEnumerator HeroAttack()
    {
        //return the finalAttack
        //LOOP to the the total?
        //how to distinguish between the current and others. Dont have to?
        //for (int i = 0; i < aHeroes.Length; i++) {
        //    //diceNum+= (HeroFightController) curernt.getRemoteDiceNum();
        //    //this dice will be final cooperative damage
        //}
        diceNum = thisHero.diceNum;
        yield return new WaitForSeconds(2f);
        diceNum += thisHero.currentSP;
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

        damage = aMonster.MonsterRoll();

        fHUD.rollResult(aMonster.printRoll() + "Max:" + damage);
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
        if (damage > diceNum)
        {
            for (int i = 0; i < aHeroes.Length; i++)
            {
                aHeroes[i].Attacked(damage - diceNum);
                hHUD.basicInfoUpdate(aHeroes[i]);
            }
        }
        else if (damage < diceNum)
        {
            aMonster.Attacked(diceNum - damage);
            mHUD.basicInfo(aMonster);
        }
        yield return new WaitForSeconds(2f);
        if (aMonster.currentWP <= 0)
        {
            fightstate = FightState.WIN;
            fHUD.setFightHUD_WIN();
            yield return new WaitForSeconds(2f);
            //TODO: desotry Monster
            Destroy(aMonster);
            //TODO: solveOverlap
            //TODO: how could LoadScneneknows the maximum reward.
            SceneManager.LoadSceneAsync("Distribution", LoadSceneMode.Additive);

        }
        else if (thisHero.currentWP <= 0)
        {
            fightstate = FightState.LOSE;
            fHUD.setFightHUD_LOSE();
            yield return new WaitForSeconds(2f);
            SceneManager.UnloadSceneAsync("FightScene");
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
    public void onMagicClick()
    {
        //assume black dice is not allowed to flipped
        if (fightstate != FightState.MONSTER)
        {
        }

        if (thisHero.onMagicClick())
        {
            fHUD.rollResult("Applied Magic:" + thisHero.diceNum);
        }
    }
    bool usedhelm = false;

    public void onSheildClick()
    {
        if (fightstate != FightState.MONSTER && !usedhelm)
        {
            return;
        }

        if (thisHero.onSheildClick())
        {
            damage = 0;
        }

        fHUD.rollResult("Applied Sheild:" + damage);

    }

    public void onHelmClick()
    {
        if (fightstate != FightState.HERO)
        {
            return;
        }

        if (thisHero.onHelmClick())
        {
            diceNum = thisHero.diceNum;

            fHUD.rollResult("Applied Helm:" + diceNum);
        }
    }

    public void onHerbSClick()
    {
        if (fightstate != FightState.HERO)
        {
            return;
        }
        if (thisHero.onHerbSClick())
        {
            diceNum = thisHero.diceNum;
            //late();
            fHUD.rollResult("Applied Herb on Strength:" + diceNum);
        }
    }

    public void onHerbWClick()
    {
        if (fightstate != FightState.HERO)
        {
            return;
        }
        if (thisHero.onHerbWClick())
        {
            diceNum = thisHero.diceNum;

            fHUD.rollResult("Applied Herb on Will:" + thisHero.currentWP + "/" + diceNum);
            hHUD.basicInfoUpdate(thisHero);
        }

    }

    public void onBrewWClick()
    {
        if (fightstate != FightState.HERO)
        {
            return;
        }
        if (thisHero.onBrewWClick())
        {
            diceNum = thisHero.diceNum;
            fHUD.rollResult("Applied Brew:" + diceNum);
        }


    }

    public void OnYesClick()
    {
        thisHero.OnYesClick(myArcherYesButton, mySkillYesButton);
    }

    public void onSkillClick()
    {
        mySkillYesButton.gameObject.SetActive(false);
        thisHero.cleartimes();
        StartCoroutine(HeroAttack());
    }

    /*Get method*/
    public FightState getFightState()
    {
        return this.fightstate;

    }


    //Handling  the turn manager
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
        //TODO: replace this by a functions correlated with WP
        fightstate = FightState.HERO;
        //Reinitialize something
        //Button?
        diceNum = 0;
        damage = 0;

        if (thisHero.heroType == Hero.Type.WIZARD)
        {
            hHUD.backColorMagic();
        }

        playerturn();


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

    IEnumerator late()
    {
        yield return new WaitForSeconds(2f);
    }
}