using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TradeSwitch : MonoBehaviour
{

    private void Start()
    {
        print("hey");
    }
    // Start is called before the first frame update
    public void ToTrade()
    {
        print("a");
        SceneManager.LoadScene("TradeScene");
        print("hey2");
    }

    public void ToMap()
    {
        SceneManager.LoadScene("GaneLobby");
    }

    public void ToFight()
    {
       
    }


}
