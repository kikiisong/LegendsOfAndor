using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Bag;

public class Shop : MonoBehaviour
{
    private static Hero Hero
    {
        get
        {
            return PhotonNetwork.LocalPlayer.GetHero();
        }
    }

    public static int merchantLocation;
    public static bool isDawrf;

    public GameObject messageBox;
    public TextMeshProUGUI message;

    public Button buttonConfirm;
    public Button buttonClose;

    string itemToBuy;

    public Button SP;
    public Button WINESKIN;
    public Button SHIELD;
    public Button BOW;
    public Button HELM;
    public Button FALCON;
    public Button TELESCOPE;

    int price;

    // Start is called before the first frame update
    void Start()
    {
        buttonConfirm.onClick.AddListener(() => ConfirmClicked(itemToBuy));
        buttonClose.onClick.AddListener(() => CancelClicked());

        SP.onClick.AddListener(() => BuyItem("SP"));
        WINESKIN.onClick.AddListener(() => BuyItem("WINESKIN"));
        SHIELD.onClick.AddListener(() => BuyItem("SHIELD"));
        BOW.onClick.AddListener(() => BuyItem("BOW"));
        HELM.onClick.AddListener(() => BuyItem("HELM"));
        FALCON.onClick.AddListener(() => BuyItem("FALCON"));
        TELESCOPE.onClick.AddListener(() => BuyItem("TELESCOPE"));
    }

    private void OnEnable()
    {
        messageBox.SetActive(false);
    }

    void BuyItem(string itemName)
    {
        if (Hero.GetCurrentRegion().label != merchantLocation) //hero not here
        {

            messageBox.SetActive(true);
            buttonClose.gameObject.SetActive(true);
            message.text = "You are not at this shop yet! You can purchase when you are here. Welcome back in the future!";

        }
        else // hero here
        {
            //set price
            price = 2;
            if (isDawrf && Hero.type == Hero.Type.DWARF)
            {
                price = 1;
            }

            //not enough gold
            if (Hero.data.gold < price)
            {
                messageBox.SetActive(true);
                buttonClose.gameObject.SetActive(true);
                message.text = itemName + " costs "+ price +" gold to purchase. " + "You don't have enough gold!";
            }
            else // enough gold
            {
                messageBox.SetActive(true);
                buttonConfirm.gameObject.SetActive(true);
                buttonClose.gameObject.SetActive(true);
                message.text = "Are you sure you want to buy "+ itemName + " with " + price + " gold?";
                itemToBuy = itemName;

            }



        }
        
    }

    private void CancelClicked()
    {
        buttonClose.gameObject.SetActive(false);
        buttonConfirm.gameObject.SetActive(false);
        messageBox.SetActive(false);
        itemToBuy = null;

    }

    private void ConfirmClicked(string itemName)
    {
       
        itemToBuy = null;

        messageBox.SetActive(false);
        buttonConfirm.gameObject.SetActive(false);
        buttonClose.gameObject.SetActive(false);

        bool bought = false;

    
        if (itemName=="SP")
        {
            if (Hero.data.SP < 14)
            {
                Hero.data.SP += 1;
                Hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonClose.gameObject.SetActive(true);
                message.text = "You already have full strength points!";
            }
            
        }


        
        if (itemName == "WINESKIN")
        {
            if (PhotonNetwork.LocalPlayer.NumSmallItems() < 3)
            {
                Bag.Helper.ItemIncrement(PhotonNetwork.LocalPlayer, ItemType.Wineskin);
                Hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonClose.gameObject.SetActive(true);
                message.text = "You have no space for this item";
            }
        }


        //
        if (itemName == "FALCON")
        {
            if (!PhotonNetwork.LocalPlayer.HasLargeItem())
            {

                PhotonNetwork.LocalPlayer.ItemIncrement(ItemType.Falcon);
                Hero.data.gold -= price;
                bought = true;
            }
            else
            {
                print("here! hasLarge");
                messageBox.SetActive(true);
                buttonClose.gameObject.SetActive(true);
                message.text = "You have no space for this item";
            }       
        }



        if (itemName == "SHIELD")
        {
            if (!Bag.Helper.HasLargeItem(PhotonNetwork.LocalPlayer))
            {
                PhotonNetwork.LocalPlayer.ItemIncrement(ItemType.Shield);
                Hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonClose.gameObject.SetActive(true);
                message.text = "You have no space for this item";
            }
        }



        if (itemName == "BOW")
        {
            if (Hero.type == Hero.Type.ARCHER)
            {
                messageBox.SetActive(true);
                buttonClose.gameObject.SetActive(true);
                message.text = "You already have a Bow";
               
            }
            else if (!Bag.Helper.HasLargeItem(PhotonNetwork.LocalPlayer))
            {
                Bag.Helper.ItemIncrement(PhotonNetwork.LocalPlayer, ItemType.Bow);
                Hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonClose.gameObject.SetActive(true);
                message.text = "You do not have space for this item";
            }

        }


        if (itemName == "HELM")
        {
            if (!Bag.Helper.HasItem(PhotonNetwork.LocalPlayer, ItemType.Helm))
            {
                Bag.Helper.ItemIncrement(PhotonNetwork.LocalPlayer, ItemType.Helm);
                Hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonClose.gameObject.SetActive(true);
                message.text = "You already heve a helmet.";
            }
        }


        if (itemName == "TELESCOPE")
        {
            if (Bag.Helper.NumSmallItems(PhotonNetwork.LocalPlayer) < 3)
            {
                Bag.Helper.ItemIncrement(PhotonNetwork.LocalPlayer, ItemType.Telescope);
                Hero.data.gold -= price;
                bought = true;
            }
            else
            {
                messageBox.SetActive(true);
                buttonClose.gameObject.SetActive(true);
                message.text = "You do not have space for this item";
            }
        }

        if (bought)
        {
            messageBox.SetActive(true);
            buttonClose.gameObject.SetActive(true);
            message.text = "You bought a " + itemName + "!";
            print("bought " + itemName);
        }
    }
}
