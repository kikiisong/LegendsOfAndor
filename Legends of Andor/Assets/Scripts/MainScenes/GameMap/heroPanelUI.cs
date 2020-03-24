using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class heroPanelUI : MonoBehaviour
{

    
    public Slider wp;
    public Slider sp;

    public TextMeshProUGUI gold;
    public TextMeshProUGUI wpNum;
    public TextMeshProUGUI spNum;





    Hero hero
    {
        get
        {
            return (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {

        wp.value = hero.data.WP;

        wpNum.text = "WILLPOWER:  " + hero.data.WP.ToString();
        spNum.text = "STRENGTH:  " + hero.data.SP.ToString();

        sp.value = hero.data.SP;
        gold.text = hero.data.gold.ToString();

    }
}
