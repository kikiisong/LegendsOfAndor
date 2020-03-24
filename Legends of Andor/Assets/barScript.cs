using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class barScript : MonoBehaviour
{

    public Slider slider;
    private Hero hero;


    // Start is called before the first frame update
    void Start()
    {
        hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = hero.data.WP;
    }

    
}
