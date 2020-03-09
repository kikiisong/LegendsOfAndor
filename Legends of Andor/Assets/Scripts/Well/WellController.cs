using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
using Photon.Pun;
=======
>>>>>>> 5289f4d5818528ba3a92c41b6e4ec7cc09803f68
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
