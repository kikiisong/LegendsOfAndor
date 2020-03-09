using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

//public GameObject myBtn;
public class DropGold : MonoBehaviourPun, TurnManager.IOnMove
{
    Button myBtn;
    Region current;
    public GameObject gold;
    void Start()
    {
        myBtn = GameObject.Find("DropButton").GetComponent<Button>();
        TurnManager.Register(this);
        //photonView.OwnershipTransfer = OwnershipOption.Request;

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

        g.photonView.RequestOwnership();
        g.decrement();
        g.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "" + g.goldValue;

        if (g.goldValue == 0)
        {

            PhotonNetwork.Destroy(g.gameObject);
        }
        photonView.RPC("increm", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    [PunRPC]
    //drop gold function 
    void drop()
    {

        List<Gold> list = GameGraph.Instance.FindObjectsOnRegion<Gold>(current);
        Debug.Log("list count = " + list.Count);
        if (list.Count == 0)
        {
            Debug.Log("we're inside list count 0");
            GameObject g = PhotonNetwork.Instantiate("Gold", transform.position, Quaternion.identity, 0);
            GameGraph.Instance.PlaceAt(g, current.label);
            g.GetComponent<Gold>().increment();
            g.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "" + g.GetComponent<Gold>().goldValue;
        }
        else
        {
            Debug.Log("we're inside list count 1");
            Gold g = list[0];
            g.increment();
            g.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "" + g.goldValue;

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
  
        string regionNum = Regex.Replace(current.ToString(), "[^0-9]", "");
        myBtn.GetComponentInChildren<Text>().text = regionNum;
        Debug.Log("this is region number: " + regionNum);
    }
    public void OnMove(Player player, Region currentRegion)
    {
        // throw new System.NotImplementedException();
        updateRegionButton();

    }


}
