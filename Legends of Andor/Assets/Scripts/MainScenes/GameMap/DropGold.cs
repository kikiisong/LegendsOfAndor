using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;


public class DropGold : MonoBehaviourPun, TurnManager.IOnMove
{
    Button myBtn;
    Region current;
    public GameObject gold;
    void Start()
    {
        myBtn = GameObject.Find("DropButton").GetComponent<Button>();
        TurnManager.Register(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            drop();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            pickup();
        }
    }

    [PunRPC]
    //pick up function
    void pickup()
    {

        List<Gold> list = GameGraph.Instance.FindObjectsOnRegion<Gold>(current);
        Gold g = list[0];

        g.decrement();
        g.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "" + g.goldValue;
        Debug.Log("before the photon view");
        
        Debug.Log("after the photon view");
        if (g.goldValue == 0)
        {
            //   list.Remove(g);
            Destroy(g.gameObject);
            // Destroy(g.GetComponentInChildren<TMPro.TextMeshProUGUI>());    
            Debug.Log("list size after removing the obejct  " + list.Count);

            // g.GetComponent<Renderer>().enabled = false;
            // g.GetComponentInChildren<TMPro.TextMeshProUGUI>().enabled = false;

        }
        photonView.RPC("increm", RpcTarget.All, PhotonNetwork.LocalPlayer);
        ///notify everyone else? 

    }

    [PunRPC]
    //drop gold function 
    void drop()
    {
        //check if containts gold already

        List<Gold> list = GameGraph.Instance.FindObjectsOnRegion<Gold>(current);
        Debug.Log("list size before the instantiating a gold object " + list.Count);
        if (list.Count == 0)
        {
 
            GameObject g = PhotonNetwork.Instantiate("Gold", transform.position, Quaternion.identity, 0);
            GameGraph.Instance.PlaceAt(g, current.label);
            g.GetComponent<Gold>().increment();
            g.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "" + g.GetComponent<Gold>().goldValue;
            

        }
        else
        {
            Gold g = list[0];
            g.increment();
            g.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "" + g.goldValue;

            //decrement for the player
          //  PhotonView photonView = PhotonView.Get(this);
             photonView.RPC("decrem", RpcTarget.All, PhotonNetwork.LocalPlayer) ;

        }
    }
    [PunRPC]
    public void decrem(Player player)
    {
        Hero h = (Hero) player.CustomProperties[K.Player.hero];
        h.data.gold--;
    }

    //incrememt hero's money balance since picked up gold 
    [PunRPC]
    public void increm(Player player)
    {
        Hero h = (Hero)player.CustomProperties[K.Player.hero];
        h.data.gold++;
    }
    void updateRegionButton()
    {
        //extract current player's region
        foreach (HeroMoveController c in GameObject.FindObjectsOfType<HeroMoveController>())
        {
            if (c.photonView.Owner == PhotonNetwork.LocalPlayer)
            {
                //cc = c.photonView.Owner;
                current = GameGraph.Instance.FindNearest(c.transform.position);
            }

        }
        //current = GameGraph.Instance.FindNearest(transform.position);
        string regionNum = Regex.Replace(current.ToString(), "[^0-9]", "");
        myBtn.GetComponentInChildren<Text>().text = regionNum;
        Debug.Log("this is,m,mn,mn,m from aosk" + regionNum);
    }
    public void OnMove(Player player, Region currentRegion)
    {
        // throw new System.NotImplementedException();
        updateRegionButton();

    }


}
