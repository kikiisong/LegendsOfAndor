using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class Witch : MonoBehaviourPun, TurnManager.IOnMove
{
    public int region;
    public Button brewButton;
    public Renderer witchIcon;
    public bool found;
    public int left;

    // Start is called before the first frame update
    void Start()
    {
        witchIcon = GetComponent<Renderer>();
        witchIcon.enabled = false;
        brewButton.gameObject.SetActive(false);
        TurnManager.Register(this);
        found = false;
        left = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(Player player, Region currentRegion)
    {
        if (found)
        {
            if (PhotonNetwork.LocalPlayer == player)
            {
                Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();//photonView.Owner is the Scene

                List<Witch> witchOnRegion = GameGraph.Instance.FindObjectsOnRegion<Witch>(currentRegion);

                if (witchOnRegion.Count > 0 && left > 0)
                {
                    brewButton.gameObject.SetActive(true);
                    brewButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    brewButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        //pop up window
                        //if click yes to buy then call bought
                        //photonView.RPC("bought", RpcTarget.AllBuffered, currentRegion.label);
                        left -= 1;

                        brewButton.gameObject.SetActive(false);
                    });


                }
                else
                {
                    brewButton.gameObject.SetActive(false);
                }
            }
        }


    }

    public void locate(int currReg)
    {
        GameGraph.Instance.PlaceAt(gameObject, currReg);
    }

    [PunRPC]
    public void bought(int currentRegion)
    {
        List<Witch> witchOnRegion = GameGraph.Instance.FindObjectsOnRegion<Witch>(currentRegion);

        Witch temp = witchOnRegion[0];

        temp.left -= 1;
    }
}
