using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHUD : MonoBehaviour
{

    public Text dice;
    public Text currentWillpower;
    public Text strengthPower;

    public Button magic;
    public Button helm;
    public Button sheild;
    public Button wineskin;
    public Button herbWill;
    public Button herbStrength;

    public Button trade;
    public Button falcon;
    public Button leave;
    public Button continueF;


    public void changeColor(Button b) {
        Image image = b.GetComponent<Image>();
        image.color = new Color((float)0.30, (float)0.30, (float)0.30);
        Debug.Log(b);
    }

    public void setMonsterHUD(HeroFightController h)
    {
        dice.text = h.redDice + " /" + h.blackDice;
        currentWillpower.text = "" + h.currentWP;
        strengthPower.text = "" + h.currentSP;


    }





}