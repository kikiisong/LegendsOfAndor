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
    
    public void setMonsterHUD(Monster m) {
        reward.text = m.rewardc + " /" + m.rewardw;
        currentWillpower.text = ""+m.currentWP;
        strengthPower.text = "" + m.maxSP;


    }

    public void basicInfo(Monster m) {

    }
}
