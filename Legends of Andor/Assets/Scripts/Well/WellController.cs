using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WellController : MonoBehaviour, TurnManager.IOnMove
{
    public GameObject drinkButton;

    // Start is called before the first frame update
    void Start()
    {
        TurnManager.Register(this);
        drinkButton = GameObject.Find("drinkButton");
        drinkButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void OnMove(Player player, Region currentRegion)
    {


    }

}
