using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroHUD : MonoBehaviour
{
    [Header("Text")]
    public Text dice;
    public Text currentWillpower;
    public Text strengthPower;

    [Header("SkillButton")]
    public Button magic;
    public Button helm;
    public Button sheild;
    public Button brew;
    public Button herbWill;
    public Button herbStrength;

    [Header("EventButton")]
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

    public void setHeroHUD(Hero h)
    {
        dice.text = h.getDiceNum() + " /" + h.data.blackDice;
        currentWillpower.text = "" + h.data.WP;
        strengthPower.text = "" + h.data.SP;

        if (!h.data.magic) changeColor(magic);
        if (!h.data.sheild) changeColor(sheild);
        if (!h.data.helm) changeColor(helm);
        if (!h.data.herbS) changeColor(herbStrength);
        if (!h.data.herbW) changeColor(herbWill);
        if (!h.data.brew) changeColor(brew);

    }

    public void backColorMagic()
    {
        Image image = magic.GetComponent<Image>();
        image.color = new Color((float)1, (float)1, (float)1);
        Debug.Log(magic);

    }

    public void basicInfoUpdate(Hero h) {
        dice.text = h.getDiceNum() + " /" + h.data.blackDice;
        currentWillpower.text = "" + h.data.WP;
        strengthPower.text = "" + h.data.SP;

    }

}