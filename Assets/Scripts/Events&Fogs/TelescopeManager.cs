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
    public GameObject TeleInfo;

    // Start is called before the first frame update
    void Start()
    {
        useTele.gameObject.SetActive(false);
        TeleInfo.SetActive(false);
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
        string s="";
        foreach (Region reg in neighbours)
        {
            List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(reg.label);
            if (fogOnRegion.Count > 0)
            {
                s += "Under the fog on region " + reg.label + " : " + fogOnRegion[0].type.ToString("g") + "\r\n";
            }
            //Debug.Log("Region " + r.label + " " + "gold " + r.data.gold);//testing
        }

        if(s.Length > 0)
        {
            Text t = TeleInfo.transform.GetChild(1).GetComponent<Text>();
            t.text = s;
        }
        else
        {
            Text t = TeleInfo.transform.GetChild(1).GetComponent<Text>();
            t.text = "There is nothing to be seen.";
        }

        TeleInfo.SetActive(true);
    }
}
