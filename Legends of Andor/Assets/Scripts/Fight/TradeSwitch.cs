using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TradeSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToTrade()
    {
        print("a");
        SceneManager.LoadScene("TradeScene");
    }

    public void ToMap()
    {
        SceneManager.LoadScene("GaneLobby");
    }

    public void ToFight()
    {
       
    }


}
