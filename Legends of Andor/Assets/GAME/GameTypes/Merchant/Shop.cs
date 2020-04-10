using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Shop : MonoBehaviour
{

    private Hero hero;

    public static int merchantLocation;
    public static bool isDawrf;

    public GameObject messageBox;
    public TextMeshProUGUI message;

    public GameObject buttonOK;
    public GameObject buttonConfirm;
    public GameObject buttonCancel;


    string itemToBuy;



    public Button SP;
    public Button WINESKIN;

    int price;

    // Start is called before the first frame update
    void Start()
    {

        hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();
        buttonOK.GetComponent<Button>().onClick.AddListener(() => OKClicked());
        buttonConfirm.GetComponent<Button>().onClick.AddListener(() => ConfirmClicked(itemToBuy));

        SP.onClick.AddListener(() => BuyItem("SP"));
        WINESKIN.onClick.AddListener(() => BuyItem("WINESKIN"));


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void BuyItem(string itemName)
    {

        print(merchantLocation);
        print("Hero: " + hero.data.regionNumber);
        print(isDawrf);
        print(itemName);


        if (hero.data.regionNumber != merchantLocation) //hero not here
        {
        
            messageBox.SetActive(true);
            buttonOK.SetActive(true);
            message.text = "You are not at this shop yet! You can purchase when you are here. Welcome back in the future!";

        }
        else // hero here
        {
            //set price
            price = 2;
            if (isDawrf && hero.type == Hero.Type.DWARF)
            {
                price = 1;
            }


            //not enough gold
            if (hero.data.gold < price)
            {
                messageBox.SetActive(true);
                buttonOK.SetActive(true);
                message.text = itemName + " costs "+ price +" gold to purchase. " + "You don't have enough gold!";
            }
            else // enough gold
            {
                messageBox.SetActive(true);
                buttonConfirm.SetActive(true);
                buttonCancel.SetActive(true);
                message.text = "Are you sure you want to buy "+ itemName + " with " + price + " gold?";
                itemToBuy = itemName;

            }



        }
        
    }


    private void OKClicked()
    {
        buttonOK.SetActive(false);
        messageBox.SetActive(false);
    }

    private void CancelClicked()
    {
        buttonOK.SetActive(false);
        messageBox.SetActive(false);

    }

    private void ConfirmClicked(string itemName)
    {
        if (itemName=="SP")
        {
            hero.data.SP += 1;
            hero.data.gold -= price;
        }
        if (itemName == "WINESKIN")
        {
            hero.data.numWineskin += 1;
            hero.data.gold -= price;
        }
        buttonConfirm.SetActive(false);
        buttonCancel.SetActive(false);
        messageBox.SetActive(false);




    }


}
