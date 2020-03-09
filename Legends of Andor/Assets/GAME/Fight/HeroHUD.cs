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
    public Button brew;
    public Button herbWill;
    public Button herbStrength;

    public Button trade;
    public Button falcon;
    public Button leave;
    public Button continueF;
    public Button RollDice;


    public void changeColor(Button b) {
        Image image = b.GetComponent<Image>();
        image.color = new Color((float)0.30, (float)0.30, (float)0.30);
        Debug.Log(b);
    }

    public void setHeroHUD(HeroFightController h)
    {
        dice.text = h.redDice + " /" + h.blackDice;
        currentWillpower.text = "" + h.currentWP;
        strengthPower.text = "" + h.currentSP;

        if (!h.magic) changeColor(magic);
        if (!h.sheild) changeColor(sheild);
        if (!h.helm) changeColor(helm);
        if (!h.herbS) changeColor(herbStrength);
        if (!h.herbW) changeColor(herbWill);
        if (!h.brew) changeColor(brew);

    }

    public void backColorMagic()
    {
        Image image = magic.GetComponent<Image>();
        image.color = new Color((float)1, (float)1, (float)1);
        Debug.Log(magic);

    }

    public void basicInfoUpdate(HeroFightController h) {
        dice.text = h.redDice + " /" + h.blackDice;
        currentWillpower.text = "" + h.currentWP;
        strengthPower.text = "" + h.currentSP;

    }

}