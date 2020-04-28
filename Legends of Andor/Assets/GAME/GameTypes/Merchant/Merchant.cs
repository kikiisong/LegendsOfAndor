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
        GameGraph.Instance.PlaceAt(gameObject, regionLabel);
    }

    private void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        Shop.merchantLocation = regionLabel;
        Shop.isDawrf = isDawrf;
        shopPanel.SetActive(true);
    }

    public void closeShop()
    {
        shopPanel.SetActive(false);
    }










}
