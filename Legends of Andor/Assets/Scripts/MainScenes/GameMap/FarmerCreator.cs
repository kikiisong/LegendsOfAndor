using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class FarmerCreator : MonoBehaviourPun, TurnManager.IOnMove
{

    [SerializeField] public GameGraph gameGraph;
    [SerializeField] public GameObject extraShileds;
    public GameObject pickUpButton;
    public GameObject dropDownButton;

  //  public Farmer tempFarmer;

    void Start()
    {

        pickUpButton = GameObject.Find("pickUpFarmerButton");
        dropDownButton = GameObject.Find("dropDownFarmerButton");
        pickUpButton.SetActive(false);
        dropDownButton.SetActive(false);

        TurnManager.Register(this);
    }

    public void OnMove(Player player, Region currentRegion)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            print("the player made a move");
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];//photonView.Owner is the Scene

            List<Farmer> farmerOnRegion = gameGraph.FindObjectsOnRegion<Farmer>(currentRegion);

            Farmer temp = farmerOnRegion[0];
//            tempFarmer = temp;

            // unfinished, need to see if there is a monster on the map
            List<Monster> monsterOnRegion = gameGraph.FindObjectsOnRegion<Monster>(currentRegion);
            if (monsterOnRegion.Count > 0 && monsterOnRegion[0] != null)
            {
                hero.data.numFarmers = 0;
            }

            // should have a while loop here, inorder to make the user to pick up and drop many times as they want

            if (temp.numberOfFarmer > 0)
            {
                pickUpButton.SetActive(true);
                pickUpButton.GetComponent<Button>().onClick.RemoveAllListeners();
                pickUpButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    print("pickupHave been pressed at region " + currentRegion.label);
                    hero.data.numFarmers++;

                    photonView.RPC("Decrease", RpcTarget.AllBuffered, currentRegion.label);

                    print("After pick up there are " + temp.numberOfFarmer + " farmers on cureent region.");
                    if (temp.numberOfFarmer == 0)
                    {
                        pickUpButton.SetActive(false);
                    }
                    dropDownButton.SetActive(true);
                });

            }
            else
            {
                pickUpButton.SetActive(false);
            }

            if (hero.data.numFarmers > 0)
            {
                dropDownButton.GetComponent<Button>().onClick.RemoveAllListeners();
                dropDownButton.GetComponent<Button>().onClick.AddListener(() =>
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
                        dropDownButton.SetActive(false);
                    }

                    if (currentRegion.label != 0)
                    {
                        pickUpButton.SetActive(true);
                    }
                });
            }
        }


    }

    [PunRPC]
    public void Decrease(int currentRegion)
    {
        List<Farmer> farmerOnRegion = gameGraph.FindObjectsOnRegion<Farmer>(GameGraph.Instance.Find(currentRegion));

        Farmer temp = farmerOnRegion[0];

        temp.decrease();
    }

    [PunRPC]
    public void Increase(int currentRegion)
    {
        List<Farmer> farmerOnRegion = gameGraph.FindObjectsOnRegion<Farmer>(GameGraph.Instance.Find(currentRegion));

        Farmer temp = farmerOnRegion[0];

        temp.increase();
    }

    [PunRPC]
    public void increaseShield()
    {
        extraShileds.GetComponent<ExtraShield>().increaseShieldsNum();
    }

}
