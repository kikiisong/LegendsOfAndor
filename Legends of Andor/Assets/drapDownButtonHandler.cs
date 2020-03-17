using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class drapDownButtonHandler : MonoBehaviourPun
{
    public Button pickUpButton;
    public GameObject extraShileds;

    public void dropDownFarmer()
    {
        Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];//photonView.Owner is the Scene

        Region currentRegion = findCurrentRegion();

        List<Farmer> farmerOnRegion = GameGraph.Instance.FindObjectsOnRegion<Farmer>(currentRegion);

        if(farmerOnRegion.Count == 0)
        {
            return;
        }
        Farmer temp = farmerOnRegion[0];

        if (hero.data.numFarmers > 0)
        {
                print("Drop down Have been pressed , at region " + currentRegion.label);
                hero.data.numFarmers--;

                if (currentRegion.label == 0)
                {
                    // extraShileds.GetComponent<ExtraShield>().increaseShieldsNum();
                    photonView.RPC("increaseShield", RpcTarget.AllBuffered);
                }
                else
                {
                    photonView.RPC("Increase", RpcTarget.AllBuffered, currentRegion.label);

                    print("After drop down there are " + temp.numberOfFarmer + " farmers on cureent region.");
                }

                if (hero.data.numFarmers == 0)
                {
                    gameObject.SetActive(false);
                }

                if (currentRegion.label != 0)
                {
                    pickUpButton.gameObject.SetActive(true);
                }
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
    public void Increase(int currentRegion)
    {
        List<Farmer> farmerOnRegion = GameGraph.Instance.FindObjectsOnRegion<Farmer>(GameGraph.Instance.Find(currentRegion));

        if (farmerOnRegion.Count == 0)
        {
            return;
        }

        Farmer temp = farmerOnRegion[0];

        temp.increase();
    }

    [PunRPC]
    public void increaseShield()
    {
        extraShileds.GetComponent<ExtraShield>().increaseShieldsNum();
    }
}