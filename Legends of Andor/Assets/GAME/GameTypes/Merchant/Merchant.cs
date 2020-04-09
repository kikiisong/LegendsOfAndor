using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Merchant : MonoBehaviour
{

    public bool isDawrf;
    public int regionLabel;
    public GameObject shopPanel;
    




    // Start is called before the first frame update
    void Start()
    {
       
           


    }

    // Update is called once per frame
    void Update()
    {
        GameGraph.Instance.PlaceAt(gameObject, regionLabel);
    }


    
    /*
    private void OnMouseDown()
    {
        print("Working");

        popUp.locationLabel = regionLabel;
        popUp.isDawrf = this.isDawrf;



        SceneManager.LoadSceneAsync("MerchantScene", LoadSceneMode.Additive);

    }
    */


    private void OnMouseDown()
    {
    

        Shop.merchantLocation = regionLabel;
        Shop.isDawrf = this.isDawrf;
        shopPanel.SetActive(true);



    }

    public void closeShop()
    {
        shopPanel.SetActive(false);
    }










}
