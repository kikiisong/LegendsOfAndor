using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Drop gold, pick up gold
/// Singleton
/// </summary>
public class GoldManager : MonoBehaviourPun, TurnManager.IOnMove
{
    public GameObject goldPrefab;

    Region current;

    void Start()
    {
        TurnManager.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) )
        {
            DropGold();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            PickupGold();
        }
    }

    void PickupGold()
    {
        List<Gold> golds = GameGraph.Instance.FindObjectsOnRegion<Gold>(current);
        if(golds.Count == 1)
        {
            Gold g = golds[0];
            g.Decrement();
            if (g.Amount == 0)
            {
                PhotonNetwork.Destroy(g.gameObject);
            }
            photonView.RPC("IncremHero", RpcTarget.All, PhotonNetwork.LocalPlayer);
        }
    }

    void DropGold()
    {
        Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        if (hero.data.gold > 0)
        {
            DropGold();
        }
        List<Gold> golds = GameGraph.Instance.FindObjectsOnRegion<Gold>(current);
        if (golds.Count == 0)
        {
            Debug.Log("we're inside list count 0");
            GameObject g = PhotonNetwork.Instantiate("Gold", transform.position, Quaternion.identity, 0);
            GameGraph.Instance.PlaceAt(g, current.label);
            g.GetComponent<Gold>().Increment();
            g.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "" + g.GetComponent<Gold>().Amount;
            photonView.RPC("decrem", RpcTarget.All, PhotonNetwork.LocalPlayer);
        }
        else if(golds.Count == 1)
        {
            Debug.Log("we're inside list count 1");
            Gold g = golds[0];
            g.photonView.RequestOwnership();
            g.Increment();
            g.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "" + g.Amount;

            photonView.RPC("decrem", RpcTarget.All, PhotonNetwork.LocalPlayer) ;

        }
    }

    /// <summary>
    /// Incrememt hero's money balance since picked up gold 
    /// </summary>
    [PunRPC]
    public void IncremHero(Player player)
    {
        Hero h = (Hero)player.CustomProperties[K.Player.hero];
        h.data.gold++;
        UpdateRegion();
    }

    [PunRPC]
    public void DecremHero(Player player)
    {
        Hero h = (Hero)player.CustomProperties[K.Player.hero];
        h.data.gold--;
        UpdateRegion();
    }

    void UpdateRegion()
    {
        //extract current player's region
        foreach (HeroMoveController c in GameObject.FindObjectsOfType<HeroMoveController>())
        {
            if (c.photonView.Owner == PhotonNetwork.LocalPlayer)
            {
                current = GameGraph.Instance.FindNearest(c.transform.position);
            }
        }
  
        //string regionNum = Regex.Replace(current.ToString(), "[^0-9]", "");
        //Debug.Log("this is region number: " + regionNum);
    }

 
    public void OnMove(Player player, Region currentRegion)
    {
        UpdateRegion();
    }
}
