using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class FogManager : MonoBehaviourPun, TurnManager.IOnMove
{

    // Start is called before the first frame update
    void Start()
    {
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
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();//photonView.Owner is the Scene

            List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(currentRegion);

            if (fogOnRegion.Count > 0)
            {

                

                        //photonView.RPC("Empty", RpcTarget.AllBuffered, currentRegion.label);

                        
                //make sure fog is removed
            }
            
        }

    }
}
