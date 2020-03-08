using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Hero;

public class HeroFightController : MonoBehaviour
{
    
    public Type heroType;
    public int  maxWP,currentWP,currentSP,redDice,blackDice;
    public bool magic, herbS,brew,helm,sheild,herbW,bow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    //public bool useMagic()
    //{
    //    //check for prefession
    //    if (Magic)
    //    {
    //        Magic = false;
    //        return true;
    //    }
    //    return false;
    //}

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
    //public int useHerbStrength()
    //{
    //    //TODO: return number of increase strength
    //    //0 means not able to use herb
    //    if (HerbS)
    //    {
    //        HerbS = false;
    //        int herb = 0;//get number of herbs
    //        //set number of herbs to 0
    //        return herb;
    //    }

    //    return 0;


    //}

    //public void initializeBrew()
    //{
    //    //TODO: same logic
    //}

    //public void useBrew()
    //{
    //    if (Helm != true)
    //    {
    //        Brew = true;
    //    }
    //    else
    //    {
    //        //Maybe pop some warning message
    //    }

    //}

    //public void useHelm()
    //{
    //    if (Brew != true)
    //    {
    //        Helm = true;
    //    }
    //    else
    //    {
    //        // maybe pop some warning
    //    }

    //}

}
