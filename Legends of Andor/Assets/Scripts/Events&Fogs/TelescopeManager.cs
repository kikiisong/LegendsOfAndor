using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class TelescopeManager : MonoBehaviour, TurnManager.IOnTurnCompleted, TurnManager.IOnEndDay, TurnManager.IOnMove
{
    public Button useTele;
    public GameGraph gGraph;
    // Start is called before the first frame update
    void Start()
    {
        useTele.gameObject.SetActive(false);
        TurnManager.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(Player player, Region currentRegion)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            useTele.gameObject.SetActive(false);
        }
    }

    public void OnTurnCompleted(Player player)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();
            if (hero.data.telescope > 0)
            {
                useTele.gameObject.SetActive(true);
            }
            else
            {
                useTele.gameObject.SetActive(false);
            }

        }
    }

    public void OnEndDay(Player player)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();
            if(hero.data.telescope > 0)
            {
                useTele.gameObject.SetActive(true);
            }
            else
            {
                useTele.gameObject.SetActive(false);
            }

        }
    }

    public void useIt()
    {
        Player p = PhotonNetwork.LocalPlayer;
        var r = p.GetCurrentRegion();
        List<Region> neighbours = gGraph.AdjacentRegions(r);
        foreach (Region reg in neighbours)
        { 
            //TODO:show runestone and fog
            Debug.Log("Region " + r.label + " " + "gold " + r.data.gold);//testing
        }
       

    }
}
