using System.Collections;
using Photon.Pun;
using Routines;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviourPun
{
    /**
     * resually a hero is allowed to have one balck dice
     * therefore, if isBlack is true, we know that the hero want to use the black Dice
     */

    bool isBlack;
    ArrayList result;

    public Dice(bool isBlack){
        this.isBlack = isBlack;
        
        result = new ArrayList();
    }

    public void rollDice(int numDice){
        
        for (int i = numDice; i > 0; i--)
        {
            result.Add(randGenerator());
        }
        Debug.Log(result);
    }

    public ArrayList getResult() {
        return result;
    }


    public void setResult(List<int> a) {
        foreach(int i in a){
            this.a.Add(i);
        }

    }

    private int randGenerator(bool isBlack);

    private int randGenerator()

    {

        if(!isBlack){
            int min = 1;
            int max = 6;
            return Random.Range(min, max);
        }
        else{
            int[] temp = {6,6,8,10,10,12};
            // 6,6,8,10,10,12
            return temp[Random.Range(1, 6)];

        }
    }


}
