using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Bag;

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
        buttonCancel.GetComponent<Button>().onClick.AddListener(() => CancelClicked());

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
        itemToBuy = null;

    }

    private void ConfirmClicked(string itemName)
    {
        bool bought = false;
        if (itemName=="SP")
        {
            if (hero.data.SP < 14)
            {
                hero.data.SP += 1;
                hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonOK.SetActive(true);
                message.text = "You already have full strength points!";
            }
            
        }


        if (itemName == "WINESKIN")
        {
            if (Bag.Helper.NumSmallItems(PhotonNetwork.LocalPlayer) < 3)
            {
                Bag.Helper.ItemIncrement(PhotonNetwork.LocalPlayer, ItemType.Wineskin);
                hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonOK.SetActive(true);
                message.text = "Yoou do not have space for this item";
            }
        }


        if (itemName == "FALCON")
        {
            if (!Bag.Helper.hasLarge(PhotonNetwork.LocalPlayer))
            {
                Bag.Helper.ItemIncrement(PhotonNetwork.LocalPlayer, ItemType.Falcon);
                hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonOK.SetActive(true);
                message.text = "You do not have space for this item";
            }       
        }


        if (itemName == "SHIELD")
        {
            if (!Bag.Helper.hasLarge(PhotonNetwork.LocalPlayer))
            {
                Bag.Helper.ItemIncrement(PhotonNetwork.LocalPlayer, ItemType.Shield);
                hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonOK.SetActive(true);
                message.text = "You do not have space for this item";
            }
        }



        if (itemName == "BOW")
        {
            if (!Bag.Helper.hasLarge(PhotonNetwork.LocalPlayer))
            {
                Bag.Helper.ItemIncrement(PhotonNetwork.LocalPlayer, ItemType.Bow);
                hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonOK.SetActive(true);
                message.text = "You do not have space for this item";
            }

        }


        if (itemName == "HELM")
        {
            if (!Bag.Helper.HasItem(PhotonNetwork.LocalPlayer, ItemType.Helm))
            {
                Bag.Helper.ItemIncrement(PhotonNetwork.LocalPlayer, ItemType.Helm);
                hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonOK.SetActive(true);
                message.text = "You already heve a helmet.";
            }
        }


        if (itemName == "TELESCOPE")
        {
            if (Bag.Helper.NumSmallItems(PhotonNetwork.LocalPlayer) < 3)
            {
                Bag.Helper.ItemIncrement(PhotonNetwork.LocalPlayer, ItemType.Telescope);
                hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonOK.SetActive(true);
                message.text = "You do not have space for this item";
            }
        }


        if (bought)
        {
            messageBox.SetActive(false);
            buttonConfirm.SetActive(false);
            buttonCancel.SetActive(false);
        }


        

    }


}
