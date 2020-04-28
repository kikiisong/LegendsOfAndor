using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class CheckMap : MonoBehaviour
{
    public GameObject regionInfoPanel;

    // Start is called before the first frame update
    void Start()
    {
        regionInfoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkRegionInfo()
    {
        regionInfoPanel.SetActive(true);
        string s = "";

        Player[] players = PhotonNetwork.PlayerList;
        s += "Hero on Region: ";
        for (int i = 0; i < players.Length; i++)
        {
            Hero hero = (Hero)players[i].GetHero();
            s += hero.GetCurrentRegion().label + "("+ hero.type +")";
        }

        s += "\r\n";



        Well[] wells = GameObject.FindObjectsOfType(typeof(Well)) as Well[];
        if(wells.Length>0)
        {
            s += "Well on Region: ";

            foreach (Well w in wells)
            {
                s += w.region + "  ";
            }

            s += "\r\n";
        }

        Fog[] fogs = GameObject.FindObjectsOfType(typeof(Fog)) as Fog[];
        if (fogs.Length > 0)
        {
            s += "Fog on Region: ";

            foreach (Fog f in fogs)
            {
                s += f.region + "  ";
            }

            s += "\r\n";
        }


        Witch[] witch = GameObject.FindObjectsOfType(typeof(Witch)) as Witch[];
        if (witch.Length > 0)
        {
            s += "Witch on Region: ";

            foreach (Witch wi in witch)
            {
                s += wi.region + "  ";
            }

            s += "\r\n";
        }

        Herb[] herb = GameObject.FindObjectsOfType(typeof(Herb)) as Herb[];
        if (herb.Length > 0)
        {
            s += "Herb on Region: ";

            foreach (Herb h in herb)
            {
                s += h.getRegion() + "  ";
            }

            s += "\r\n";
        }


        Debug.Log(s);
    }
}
