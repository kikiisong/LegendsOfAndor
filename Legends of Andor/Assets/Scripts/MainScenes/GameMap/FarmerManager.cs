using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class FarmerManager : MonoBehaviourPun, TurnManager.IOnMove
{
    public static FarmerManager Instance;

    public GameObject extraShileds;
    public Button pickUpButton;
    public Button dropDownButton;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pickUpButton.gameObject.SetActive(false);
        dropDownButton.gameObject.SetActive(false);

        TurnManager.Register(this);
    }

    public void OnMove(Player player, Region currentRegion)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            print("the player made a move");
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];//photonView.Owner is the Scene

            List<Farmer> farmerOnRegion = GameGraph.Instance.FindObjectsOnRegion<Farmer>(currentRegion);

            if(farmerOnRegion.Count == 0)
            {
                return;
            }
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
                pickUpButton.gameObject.SetActive(true);
            //    pickUpButton.onClick.RemoveAllListeners();
            //    pickUpButton.onClick.AddListener(() =>
            //    {
            //        print("pickupHave been pressed at region " + currentRegion.label);
            //       hero.data.numFarmers++;
            //        print("the hero's num is " + hero.data.numFarmers);
            //        photonView.RPC("Decrease", RpcTarget.AllBuffered, currentRegion.label);
            //
            //        print("After pick up there are " + temp.numberOfFarmer + " farmers on cureent region.");
            //        if (temp.numberOfFarmer == 0)
            //        {
            //            pickUpButton.gameObject.SetActive(false);
            //        }
            //        dropDownButton.gameObject.SetActive(true);
            //    });

            }
            else
            {
                pickUpButton.gameObject.SetActive(false);
            }

            
        }


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


    public void SetFarmerRPC()
    {
        photonView.RPC("Increase", RpcTarget.AllBuffered, 24);
        photonView.RPC("Increase", RpcTarget.AllBuffered, 32);
    }
}
