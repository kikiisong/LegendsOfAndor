using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AddAndMinus : MonoBehaviour
{
    public Text w;
    public Text c;
    int monsterCoin = 2;
    int monsterStrong = 2;
    
    /*
    Button plusWill;
    Button plusStrength;
    Button minusWill;
    Button minusStrength;
    */
    int currentscore = 0;
    int currentscorestrength = 0;
    


    public void PLusCoin()
    {
        currentscore++;
        //TODO:minsterWill
        currentscore = Mathf.Min(currentscore, monsterCoin);
        w.text = currentscore.ToString();
          
    }


    public void PlusStrength()
    {
        currentscorestrength++;
        //TODO:minsterStrong
        currentscorestrength = Mathf.Min(currentscorestrength, monsterStrong);
        c.text = currentscorestrength.ToString();

    }


    public void MinusCoin()
    {
        currentscore--;
        currentscore = Mathf.Max(0, currentscore);
        w.text = currentscore.ToString();

    }


    public void MinusStrength()
    {
        currentscorestrength--;
        currentscorestrength = Mathf.Max(0, currentscorestrength);
        w.text = currentscore.ToString();

    }

}
