using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public GameObject castle;

    public bool checkIsWinning()
    {
        if(castle.GetComponent<Castle>().isSkralDefeated() == true &&
            castle.GetComponent<Castle>().isHerbBack() == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
