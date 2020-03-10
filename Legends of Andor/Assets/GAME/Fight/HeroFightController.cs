using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static Hero;


public class HeroFightController : MonoBehaviourPun
{
    
    public Type heroType;
    public int  maxWP,currentWP,currentSP,redDice,blackDice;
    public bool magic, herbS,brew,helm,sheild,herbW,bow;
    public Dice dice;

    int times;
    int btimes;

    public int diceNum;

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

    public void OnRollDice(Button myArcherYesButton, Button mySkillYesButton, FightHUD fHUD)
    {
        StartCoroutine(HeroRoll(myArcherYesButton, mySkillYesButton,fHUD));
        
    }
    public void cleartimes() {
        times = getDiceNum();
        btimes = blackDice;
    }

    IEnumerator HeroRoll(Button myArcherYesButton, Button mySkillYesButton, FightHUD fHUD)
    {
        yield return new WaitForSeconds(2f);
        if (heroType == Hero.Type.ARCHER)
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
                OnYesClick(myArcherYesButton,mySkillYesButton);
            }
        }
        else
        {
            mySkillYesButton.gameObject.SetActive(true);
            dice.rollDice(redDice, blackDice);
            diceNum = dice.getMax();
            fHUD.rollResult(dice.printArrayList() + "Max:" + diceNum);
        }
        yield return new WaitForSeconds(4f);
    }

    public void OnYesClick(Button myArcherYesButton,Button mySkillYesButton)
    {
        myArcherYesButton.gameObject.SetActive(false);
        mySkillYesButton.gameObject.SetActive(true);
    }

    public bool onMagicClick()
    {
        //assume black dice is not allowed to flipped
        if (!magic)
        {
            return false;
        }

        magic = false;

        if (diceNum < 7)
        {
            diceNum = 7 - diceNum;
        }
        else
        {
            //donothing
        }
        return true;
    }

    bool usedhelm = false;

    public bool onSheildClick()
    {
        if (!sheild && !usedhelm)
        {
            return false;
        }
        sheild = false;

        return true;

    }

    public bool onHelmClick()
    {
        if (!helm)
        {
            return false; 
        }

        diceNum = dice.getSum();
        usedhelm = true;
        helm = false;
        return true;
    }

    public bool onHerbSClick()
    {
        if (!herbS) {
            return false;
        }
        diceNum += 2;
        herbS = false;
        return true;
    }

    public bool onHerbWClick()
    {
        if (!herbW)
        {
            return false;
        }

        currentWP += 2;
        herbW = false;
        return true;

    }

    public bool onBrewWClick()
    {
        if (!brew)
        {
            return false;
        }
        
        diceNum *= 2;
        herbW = false;
        return true;


    }
}
