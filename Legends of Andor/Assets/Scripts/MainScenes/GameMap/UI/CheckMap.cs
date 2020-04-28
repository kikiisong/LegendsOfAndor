using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        Debug.Log(s);
    }
}
