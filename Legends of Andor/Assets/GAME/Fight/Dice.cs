using System.Collections;
using Photon.Pun;
using Routines;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Dice : MonoBehaviourPun
{
    /**
     * resually a hero is allowed to have one balck dice
     * therefore, if isBlack is true, we know that the hero want to use the black Dice
     */

    public List<int> a = new List<int>();


    public void rollDice(int numRed, int numBlack){
        
        for (int i = numRed; i > 0; i--)
        {
            a.Add(randGenerator(false));
        }

        for (int i = numBlack; i > 0; i--)
        {
            a.Add(randGenerator(true));
        }
        Debug.Log(a);
    }

    public List<int> getResult() {
        return a;
    }

    private int randGenerator(bool isBlack)
    {

        if(!isBlack){
            int min = 1;
            int max = 7;
            return Random.Range(min, max);
        }
        else{
            int[] temp = {6,6,8,10,10,12};
            // 6,6,8,10,10,12
            return temp[Random.Range(1, 6)];

        }
    }

    public int getSum() {
        int m = getMax();
        a.Remove(m);
        int n = getMax();
        a.Add(m);
        return m + n;
    }

    public int getMax() {
        return a.Max();
    }

    public int getOne(bool isBlack) {
        return randGenerator(isBlack);
    }

    public string printArrayList()
    {
        string r = "";
        //int max = 0;
        for (int i = 0; i < a.Count; i++)
        {
            r += a[i] + " ";
        }
        a.Clear();
        return r;
        
    }


}
