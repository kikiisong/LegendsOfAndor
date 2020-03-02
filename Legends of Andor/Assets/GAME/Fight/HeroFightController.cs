using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFightController : MonoBehaviour
{
    HeroType heroType;

    int redDice;
    int blackDice;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool Magic;
    public void initializeMagic()
    {
        if (this.heroType == HeroType.WIZARD)
        {
            Magic = true;
        }
        else
        {
            Magic = false;
        }
    }
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
