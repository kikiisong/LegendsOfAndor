using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class HeroStatsUI : MonoBehaviour
{
    public Button toggle;
    public GameObject panel;

    [Header("Text")]
    public TextMeshProUGUI wpUI;
    public TextMeshProUGUI goldUI;

    Hero hero;

    // Start is called before the first frame update
    void Start()
    {
        hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        toggle.onClick.AddListener(() =>
        {
            panel.SetActive(!panel.activeSelf);
        });
    }

    // Update is called once per frame
    void Update()
    {
        //inefficient, but drawing every frame anyways
        wpUI.text = "WP: " + hero.data.WP;
        goldUI.text = "Gold: " + hero.data.gold;
    }
}
