using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class WitchManager : MonoBehaviourPun, TurnManager.IOnTurnCompleted, TurnManager.IOnEndDay, TurnManager.IOnMove
{
    public Button brewButton;
    public GameObject askWindow;
    public int price;
    int displayPrice;

    // Start is called before the first frame update
    void Start()
    {
        askWindow.SetActive(false);
        brewButton.gameObject.SetActive(false);
        TurnManager.Register(this);

        Player[] players = PhotonNetwork.PlayerList;
        if (players.Length == 2)
        {
            price = 3;
        }
        else if (players.Length == 3)
        {
            price = 4;
        }
        else
        {
            price = 5; //will be 5 when testing with 1 player
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(Player player, Region currentRegion)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            askWindow.SetActive(false);
            brewButton.gameObject.SetActive(false);
        }
    }

    public void OnTurnCompleted(Player player)
    {

        if (PhotonNetwork.LocalPlayer == player)
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();//photonView.Owner is the Scene
            var r = player.GetCurrentRegion();
            List<Witch> witchOnRegion = GameGraph.Instance.FindObjectsOnRegion<Witch>(r.label);
            int left = witchOnRegion[0].left;

            if (witchOnRegion.Count > 0 && left > 0)
            {
                brewButton.gameObject.SetActive(true);
            }
            else
            {
                brewButton.gameObject.SetActive(false);
            }


        }
    }

    public void OnEndDay(Player player)
    {

        if (PhotonNetwork.LocalPlayer == player)
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();//photonView.Owner is the Scene
            var r = player.GetCurrentRegion();
            List<Witch> witchOnRegion = GameGraph.Instance.FindObjectsOnRegion<Witch>(r.label);
            int left = witchOnRegion[0].left;

            if (witchOnRegion.Count > 0 && left > 0)
            {
                brewButton.gameObject.SetActive(true);
            }
            else
            {
                brewButton.gameObject.SetActive(false);
            }


        }
    }

    public void btAppear()
    {
        Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();//photonView.Owner is the Scene
        displayPrice = price;
        if (hero.type == Hero.Type.ARCHER)
        {
            displayPrice = price - 1;
        }
        Text t = askWindow.transform.GetChild(1).GetComponent<Text>();
        t.text = "Do you want to buy witch's brew for with  " + displayPrice + " Gold";

        askWindow.SetActive(true);


        if (hero.data.gold < displayPrice)
        {
            t = askWindow.transform.GetChild(1).GetComponent<Text>();
            t.text = "You don't have enough gold.";
            askWindow.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            askWindow.SetActive(true);
        }
    }

    public void bought()
    {

        Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();//photonView.Owner is the Scene
        var r = PhotonNetwork.LocalPlayer.GetCurrentRegion();
        photonView.RPC("buyBrew", RpcTarget.AllBuffered, r.label, (int)hero.type, displayPrice);

        askWindow.SetActive(false);
    }

    [PunRPC]
    public void buyBrew(int currentRegion, int heroType, int paid)
    {
        List<Witch> witchOnRegion = GameGraph.Instance.FindObjectsOnRegion<Witch>(currentRegion);

        Witch temp = witchOnRegion[0];

        temp.left -= 1;

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Hero hero = (Hero)players[i].GetHero();
            if ((int)hero.type == heroType)
            {
                hero.data.gold -= paid;
                hero.data.brew += 2;
                break;
            }
        }
    }
}
