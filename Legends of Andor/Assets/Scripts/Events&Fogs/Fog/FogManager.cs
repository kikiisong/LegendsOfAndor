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
        int[] regions = { 8, 11, 12, 13, 16, 32, 46, 44, 42, 64, 63, 56, 47, 48, 49 };
        Shuffle(regions);
        //TODO assign types to fogs
        //photonView.RPC("Typing", RpcTarget.AllBuffered, regions);
        
        
    }

    void Shuffle(int[] list)
    {
        System.Random rand = new System.Random();
        int n = list.Length;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            int temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
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

                photonView.RPC("Encounter", RpcTarget.AllBuffered, currentRegion.label);

            }
            
        }

    }

    [PunRPC]
    public void Encounter(int currentRegion)
    {
        List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(GameGraph.Instance.Find(currentRegion));

        Fog curr = fogOnRegion[0];
        curr.uncover();

        //make sure fog is removed
        Destroy(curr);
    }

   
}
