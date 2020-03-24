using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Merchant : MonoBehaviour
{

    public bool isDawrf;
    public int regionLabel;



  

    // Start is called before the first frame update
    void Start()
    {
       
           GameGraph.Instance.PlaceAt(gameObject, regionLabel);
       
    }

    // Update is called once per frame
    void Update()
    {

    }


    

    private void OnMouseDown()
    {
        print("Working");

        popUp.locationLabel = regionLabel;
        popUp.isDawrf = this.isDawrf;



        SceneManager.LoadSceneAsync("MerchantScene", LoadSceneMode.Additive);

    }










}
