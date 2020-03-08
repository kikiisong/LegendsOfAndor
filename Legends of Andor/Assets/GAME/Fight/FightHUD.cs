using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightHUD: MonoBehaviour
{

    public Text gameState;


    public void setFightHUD_START()
    {
        gameState.text = "The fight starts!";
    }

    public void setFightHUD_PLAYER()
    {
        gameState.text = "Player Turn: Please press dice first and applied the skill.";
    }

    public void setFightHUD_MONSTER()
    {
        gameState.text = "Monster Turn: Please wait for monster to attack and take actions.";
    }

    public void setFightHUD_COOP()
    {
        gameState.text = "Other Hero Turn: the other heroes is helping you.";
    }

    public void setFightHUD_CHECK()
    {
        gameState.text = "Check the Fight Result.";
    }

    public void setFightHUD_WIN()
    {
        gameState.text = "Conguatulations, you win!";
    }
    public void setFightHUD_LOSE()
    {
        gameState.text = "What a pity, you lose!";
    }

    public void rollResult(string a) {
        gameState.text = a;
    }
}
