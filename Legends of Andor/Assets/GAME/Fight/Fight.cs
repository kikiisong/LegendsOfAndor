using UnityEngine;
using ExitGames.Client.Photon;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
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


public class Fight : MonoBehaviourPun
{
    public FightState fightstate;

   
    public Transform monsterStation;
    public GameObject monster;
    public MonsterHUD mHUD;
    public HeroHUD hHUD;
    public FightHUD fHUD;
    public Button myArcherYesButton;
    public Button mySkillYesButton;
    public Dice dice;
    public List<Hero> aHeroes;

    Monster aMonster;

    public Hero hero;
    public int diceNum;
    int damage;
    int times;
    int btimes;

    // Use this for initialization
    void Start()
    {
        fightstate = FightState.START;

        foreach (KeyValuePair<int, Player> pair in PhotonNetwork.CurrentRoom.Players)
        {
            Player player = pair.Value;
            if (player.CustomProperties.ContainsKey(K.Player.isFight))
            {
                bool fight = (bool)player.CustomProperties[K.Player.isFight];
                if (fight) {
                    hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
                    aHeroes.Add(hero);
                }
                StartCoroutine(setUpBattle());

            }
        }
    }


            //--------START--------//

            IEnumerator setUpBattle()
            {

                mySkillYesButton.gameObject.SetActive(false);
                myArcherYesButton.gameObject.SetActive(false);

                GameObject monsterGo = Instantiate(monster, monsterStation);
                aMonster = monsterGo.GetComponent<Monster>();
                mHUD.setMonsterHUD(aMonster);
                hHUD.setHeroHUD(hero);
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
                times = hero.getDiceNum();
                btimes = hero.data.blackDice;
            }
            public void OnRollDice()
            {
                if (fightstate != FightState.HERO)
                {
                    return;

                }
                StartCoroutine(HeroRoll());

            }
            

            //--------ROLL--------//
            IEnumerator HeroRoll()
            {

                if (hero.type == Hero.Type.ARCHER)
                {

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
            IEnumerator HeroAttack()
            {
                //return the finalAttack
                //LOOP to the the total?
                //how to distinguish between the current and others. Dont have to?
                //TODO:TURNMANAGER
                
                    //diceNum+= (HeroFightController) curernt.getRemoteDiceNum();
                    //this dice will be final cooperative damage
                
                diceNum += hero.data.SP;
       

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
                if (damage > diceNum)
                {
            //for (int i = 0; i < aHeroes.Length; i++)
            //{
            //    aHeroes[i].Attacked(damage - diceNum);
            //    hHUD.basicInfoUpdate(aHeroes[i]);
            //}
                    hero.Attacked(damage-diceNum);
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
                else if (hero.data.WP <= 0)
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
            //TODO: should fetch data from ..
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
                //TODO: should fetch data from ..
                diceNum *= 2;
                hero.data.herbW = false;
                fHUD.rollResult("Applied Brew:" + diceNum);


            }

            public void onSkillClick()
            {
                mySkillYesButton.gameObject.SetActive(false);
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
                times = hero.getDiceNum();
                btimes = hero.data.blackDice;
                fightstate = FightState.HERO;
                //Reinitialize something
                //Button?
                diceNum = 0;
                damage = 0;

                if (hero.type == Hero.Type.WIZARD)
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


}
    

