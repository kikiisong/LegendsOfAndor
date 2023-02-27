using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHUD : MonoBehaviour
{
    // Start is called before the first frame update
    public Text reward;
    public Text currentWillpower;
    public Text strengthPower;
    
    public void setMonsterHUD(Monster m,int currentWP) {
        if (m.isSkralOnTower())
        {
            int maxSP = m.returnSkralOnTowerSP();
            strengthPower.text = "" + maxSP;
            reward.text = m.rewardc + " /" + m.rewardw;
            currentWillpower.text = "" + currentWP;
            return;
        }

        reward.text = m.rewardc + " /" + m.rewardw;
        currentWillpower.text = ""+currentWP;
        strengthPower.text = "" + m.maxSP;
        


    }

    public void basicInfo(Monster m, int currentWP) {
        reward.text = m.rewardc + " /" + m.rewardw;
        currentWillpower.text = "" + currentWP;
        strengthPower.text = "" + m.maxSP;
    }
}
