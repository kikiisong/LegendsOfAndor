using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class FarmerCreator : MonoBehaviourPun, TurnManager.IOnMove
{
    
    [SerializeField] public GameGraph gameGraph;
    public GameObject pickUpButton;
    public GameObject dropDownButton;
    

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
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];

            List<Farmer> farmerOnRegion = gameGraph.FindObjectsOnRegion<Farmer>(currentRegion);
            Farmer temp = farmerOnRegion[0];

            // unfinished, need to see if there is a monster on the map
            List<Monster> monsterOnRegion = gameGraph.FindObjectsOnRegion<Monster>(currentRegion);
            if (monsterOnRegion.Count > 0 && monsterOnRegion[0] != null)
            {
                hero.data.numFarmers = 0;
            }
            if (temp.numberOfFarmer > 0)
            {
                Debug.Log("farmer > 0");
                pickUpButton.SetActive(true);
                pickUpButton.GetComponent<Button>().onClick.RemoveAllListeners();
                pickUpButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    hero.data.numFarmers++;
                    temp.decreaseNumOfFarmer();
                    if (temp.numberOfFarmer == 0)
                    {
                        pickUpButton.SetActive(false);
                    }
                    dropDownButton.SetActive(true);
                    Debug.Log("farmer number is " + hero.data.numFarmers);
                });
            }
            else
            {
                Debug.Log("farmer = 0");
                pickUpButton.SetActive(false);
            }

            if (hero.data.numFarmers > 0)
            {
                dropDownButton.GetComponent<Button>().onClick.RemoveAllListeners();
                dropDownButton.GetComponent<Button>().onClick.AddListener(() =>
                {
                    hero.data.numFarmers--;
                    temp.increaseNumOfFarmer();
                    if (hero.data.numFarmers == 0)
                    {
                        dropDownButton.SetActive(false);
                    }
                    pickUpButton.SetActive(true);
                });
            }
        }
        

    

}