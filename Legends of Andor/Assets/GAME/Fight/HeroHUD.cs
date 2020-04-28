using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bag;

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
    }

    public void setHeroHUD(Hero h)
    {
        dice.text = h.GetDiceNum() + " /" + h.data.blackDice;
        currentWillpower.text = "" + h.data.WP;
        strengthPower.text = "" + h.data.SP;
        
        if (!h.GetMagic()) changeColor(magic);
        if (!h.HasShield()) changeColor(sheild);
        if (!h.HasHelm()) changeColor(helm);
        if (!h.HasHerb()) changeColor(herbStrength);
        if (!h.HasHerb()) changeColor(herbWill);
        if (!h.HasBrew()) changeColor(brew);

    }

    public void backColorMagic()
    {
        Image image = magic.GetComponent<Image>();
        image.color = new Color((float)1, (float)1, (float)1);

    }

    public void basicInfoUpdate(Hero h) {
        print("WP"+ h.data.WP);
        dice.text = h.GetDiceNum() + " /" + h.data.blackDice;
        currentWillpower.text = "" + h.data.WP;
        strengthPower.text = "" + h.data.SP;

    }

}