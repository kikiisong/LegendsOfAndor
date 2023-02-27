using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

using UnityEngine.UI;
using TMPro;

public class heroPanelUI : MonoBehaviour
{

    
    public Slider wp;
    public Slider sp;
    

    public TextMeshProUGUI gold;
    public TextMeshProUGUI wpNum;
    public TextMeshProUGUI spNum;

    public Image icon;
    

    Hero Hero
    {
        get
        {
            return PhotonNetwork.LocalPlayer.GetHero();
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        //icon = GetComponent<Image>();
        icon.sprite = Hero.ui.GetSprite();
    }

    

    // Update is called once per frame
    void Update()
    {

        wp.value = Hero.data.WP;
        sp.value = Hero.data.SP;
        wpNum.text = "WILLPOWER:  " + Hero.data.WP.ToString();
        spNum.text = "STRENGTH:  " + Hero.data.SP.ToString();
        gold.text = Hero.data.gold.ToString();

    }

    
}
