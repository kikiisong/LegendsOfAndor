using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class pickUpButtonHandler : MonoBehaviourPun
{
    public Button dropDownButton;

    public void pickUpFarmer()
    {
        Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];//photonView.Owner is the Scene

        Region currentRegion = findCurrentRegion();

        List<Farmer> farmerOnRegion = GameGraph.Instance.FindObjectsOnRegion<Farmer>(currentRegion);

        Farmer temp = farmerOnRegion[0];
        //            tempFarmer = temp;

        // unfinished, need to see if there is a monster on the map
        //  List<Monster> monsterOnRegion = gameGraph.FindObjectsOnRegion<Monster>(currentRegion);
        //  if (monsterOnRegion.Count > 0 && monsterOnRegion[0] != null)
        //  {
        //      hero.data.numFarmers = 0;
        //  }

        // should have a while loop here, inorder to make the user to pick up and drop many times as they want

        if (temp.numberOfFarmer > 0)
        {
                print("pickupHave been pressed at region " + currentRegion.label);
                hero.data.numFarmers++;
                print("the hero has farmer num of " + hero.data.numFarmers);
                photonView.RPC("Decrease", RpcTarget.All, currentRegion.label);

              //  Decrease(currentRegion.label);

                print("After pick up there are " + temp.numberOfFarmer + " farmers on current region.");
                if (temp.numberOfFarmer == 0)
                {
                    gameObject.SetActive(false);
                }
                dropDownButton.gameObject.SetActive(true);
        }
    }

    private Region findCurrentRegion()
    {
        //Extract current player's region
        foreach (HeroMoveController c in GameObject.FindObjectsOfType<HeroMoveController>())
        {
            if (c.photonView.Owner == PhotonNetwork.LocalPlayer)
            {
                return GameGraph.Instance.FindNearest(c.transform.position);
            }
        }
        throw new System.Exception("No current region");
    }

    [PunRPC]
    public void Decrease(int currentRegion)
    {
        List<Farmer> farmerOnRegion = GameGraph.Instance.FindObjectsOnRegion<Farmer>(GameGraph.Instance.Find(currentRegion));

        if (farmerOnRegion.Count == 0)
        {
            return;
        }
        Farmer temp = farmerOnRegion[0];

        temp.decrease();
    }

}
