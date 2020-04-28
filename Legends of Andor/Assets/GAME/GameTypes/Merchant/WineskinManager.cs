using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WineskinManager : MonoBehaviourPun
{
    public Button button;

    static Hero Hero
    {
        get
        {
            return PhotonNetwork.LocalPlayer.GetHero();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() =>
        {
            photonView.RPC("UseWineskin", RpcTarget.All, PhotonNetwork.LocalPlayer);
        });
        button.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        button.gameObject.SetActive(Hero.data.wineskin > 0);
    }

    [PunRPC]
    public void UseWineskin(Player player)
    {
        var hero = player.GetHero();
        hero.data.wineskin--;
        hero.data.wineskinStacked++;
    }
}
