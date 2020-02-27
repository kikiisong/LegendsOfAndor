using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    // Start is called before the first frame update
    public FightManager fm;
    public void ToOtherMap(string s)
    {
        //"TradeSceneWithEgal"
        if (fm.getFight().getFightState() == FightState.DECISION) {

            SceneManager.LoadScene(s);
        }
    }
}
