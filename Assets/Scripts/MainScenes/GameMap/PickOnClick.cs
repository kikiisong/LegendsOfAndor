using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickOnClick : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Hero getClickedHero()
    {
        Player[] players = PhotonNetwork.PlayerList;
        for( int i =0; i < players.Length; i++)
        {
            if (players[i].GetCurrentRegion().position == this.gameObject.transform.position)
            {
                return players[i].GetHero();
            }
        }
        return null;
    }

    public void OnMouseDown()
    {
        Hero h = getClickedHero();
    
        if (this.gameObject.name == "WineskinPrefab")
        {
            h.data.wineskin += 2;
        }
        if(this.gameObject.name == "ShieldPrefab")
        {
            h.data.shield++;
        }

        PhotonNetwork.Destroy(this.gameObject);
    }

}
