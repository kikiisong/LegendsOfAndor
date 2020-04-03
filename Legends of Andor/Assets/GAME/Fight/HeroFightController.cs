using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Hero;

public class HeroFightController : MonoBehaviour
{
    Type heroType;

    int redDice;
    int blackDice;

    // Start is called before the first frame update
    void Start()
    {

        times = getDiceNum();
        btimes = hero.data.blackDice;
        hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
    }


    public int getDiceNum() {
        switch (heroType) {
            case (Type.ARCHER):
                if (hero.data.WP > 13)
                {
                    return 5;
                }
                else if (hero.data.WP > 6)
                {
                    return 4;
                }
                else {
                    return 3;
                }
            case (Type.DWARF):
                if (hero.data.WP > 13) {
                    return 3;
                }
                else if (hero.data.WP > 6)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            case (Type.WARRIOR):
                if (hero.data.WP > 13)
                {
                    return 4;
                }
                else if (hero.data.WP > 6)
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
        hero.data.WP -= damage;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cleartimes() {
        times = getDiceNum();
        btimes = hero.data.blackDice;
    }


    bool Magic;
    public void initializeMagic()
    {
        if (this.heroType == Type.WIZARD)
        {
            Magic = true;
        }
        else
        {
            Magic = false;
        }
    }



    public void OnYesClick(Button myArcherYesButton,Button mySkillYesButton);

    public bool useMagic()

    {
        //check for prefession
        if (Magic)
        {
            Magic = false;
            return true;
        }
        return false;
    }

    bool HerbS = false;
    public void initializeHerbS()
    {
        //TODO: if have herbs then true
        /*
            if (numHerb > 0)
        {
           HerbS = true;

        }
        else{
            HerbS = false;
        }
         */

    }
    public int useHerbStrength()
    {
        //TODO: return number of increase strength
        //0 means not able to use herb
        if (HerbS)
        {
            HerbS = false;
            int herb = 0;//get number of herbs
            //set number of herbs to 0
            return herb;
        }

        return 0;


    }

    bool Brew = false;
    public void initializeBrew()
    {
        //TODO: same logic
    }

    public void useBrew()
    {
        if (Helm != true)
        {
            Brew = true;
        }
        else
        {
            //Maybe pop some warning message
        }

    }

    bool Helm = false;
    public void useHelm()
    {
        if (Brew != true)
        {
            Helm = true;
        }
        else
        {
            // maybe pop some warning
        }

    }

    private void initial()
    {
        //after each round initialize everything
        Magic = false;
        Brew = false;
        Helm = false;
        HerbS = false;

    }
}
