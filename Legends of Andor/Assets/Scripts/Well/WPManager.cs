using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class WPManager : MonoBehaviour
{

    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        string wp = hero.data.WP.ToString();
        text.text = "WP: " + wp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
