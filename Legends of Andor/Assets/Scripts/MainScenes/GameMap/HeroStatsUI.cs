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
    public TextMeshProUGUI currentRegionUI;
    public TextMeshProUGUI wpUI;
    public TextMeshProUGUI goldUI;

    Hero Hero
    {
        get
        {
            return (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        }
    }
    Region CurrentRegion
    {
        get
        {
            foreach (HeroMoveController c in GameObject.FindObjectsOfType<HeroMoveController>())
            {
                if (c.photonView.Owner == PhotonNetwork.LocalPlayer)
                {
                    return GameGraph.Instance.FindNearest(c.transform.position);
                }
            }
            throw new System.Exception("No current region");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        toggle.onClick.AddListener(() =>
        {
            panel.SetActive(!panel.activeSelf);
        });
    }

    // Update is called once per frame
    void Update()
    {
        //inefficient, but drawing every frame anyways
        currentRegionUI.text = CurrentRegion.label.ToString();
        wpUI.text = "WP: " + Hero.data.WP;
        goldUI.text = "Gold: " + Hero.data.gold;
    }
}
