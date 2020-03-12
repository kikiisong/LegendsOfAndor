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


    public void openMenu(Hero h)
    {
        print("Working");
        SceneManager.LoadScene("MerchantScene", LoadSceneMode.Additive);
        
        //buySP(h, amt)
        //buyWineSkin()
        //buyShield()
        //buyFelcon()
        //buyBow()
        //buyHelm()
        //buyTelescope()

    }



    void buySP(Hero h, int amt)
    {

        //dwarf hero type
        if (h.type == Hero.Type.DWARF && this.isDawrf == true)
        {
            if (h.data.gold >= amt)
            {
                h.data.gold -= amt;
                h.data.SP += amt;
            }
            else
            {
                //pop not enough gold
            }
        }
        //other hero type
        else
        {
            if (h.data.gold >= 2 * amt)
            {
                h.data.gold -= (2 * amt);
                h.data.SP += amt;
            }
            else
            {
                //pop not enough gold
            }
        }

    }

    private void OnMouseDown()
    {
        print("Working");
        SceneManager.LoadSceneAsync("MerchantScene", LoadSceneMode.Additive);
    }


}
