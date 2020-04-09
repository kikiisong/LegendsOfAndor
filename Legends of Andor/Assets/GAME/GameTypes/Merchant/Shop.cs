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

    

    public Button SP;
    public Button Brew;

    // Start is called before the first frame update
    void Start()
    {

        hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();
        buttonOK.GetComponent<Button>().onClick.AddListener(() => OKClicked());

        SP.onClick.AddListener(() => BuyItem("SP"));
        Brew.onClick.AddListener(() => BuyItem("BREW"));


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void BuyItem(string itemName)
    {

        print(merchantLocation);
        print(isDawrf);
        print(itemName);


        if (hero.data.regionNumber != merchantLocation)
        {
            print("not here!");
            messageBox.SetActive(true);
            buttonOK.SetActive(true);
            message.text = "You are not at this shop yet! \n Visit again when you are here!";

           
        }
        
    }


    private void OKClicked()
    {
        buttonOK.SetActive(false);
        messageBox.SetActive(false);
    }


}
