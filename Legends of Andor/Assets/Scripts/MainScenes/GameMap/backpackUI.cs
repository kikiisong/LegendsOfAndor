using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backpackUI : MonoBehaviour
{
    public GameObject Panel;

    public List<GameObject> slots = new List<GameObject>();
    private int emptySlot = 0;
    Hero hero
    {
        get
        {
            return (Hero)PhotonNetwork.LocalPlayer.GetHero();
        }
    }

    private void Start()

    {
        /*     GameObject itemsList = GameObject.Find("/Canvas/Backpack/ItemsList");
             for(int i = 0; i < 18; i++)
             {
                 slots.Add(itemsList.transform.GetChild(i).gameObject);
             }
           //  populateBag();*/

    }



    public void Open()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);

        }
        emptySlot = 0;
       // Debug.Log("clicked");
        populateBag();
  
    }

    public void fillBag(int slotNumber, string spriteName, int parameter)
{

    GameObject itemsList = GameObject.Find("/Canvas/Backpack/ItemsList");
       // Debug.Log("found itemlist");
    Sprite spriteToLoad = Resources.Load<Sprite>(spriteName);

    GameObject image = itemsList.gameObject.transform.GetChild(slotNumber).GetChild(0).gameObject;
    //    Debug.Log("found item");
      //  Debug.Log(image.name);
       
    GameObject text = itemsList.gameObject.transform.GetChild(slotNumber).GetChild(1).gameObject;
   // Debug.Log("found itemlist");

    Image img = image.gameObject.GetComponent<Image>();
    Text tx = text.gameObject.GetComponent<Text>();

    img.sprite = spriteToLoad;
    if (parameter > 1)
    {
        tx.text = parameter.ToString();
    }
}
public void populateBag()
{
        bool initGold = false;
        if (initGold)
    {
        fillBag(emptySlot, "coin", 23);
        emptySlot++;
    }
        if (hero.data.numWineskin > 0)
    {
        fillBag(emptySlot, "wineskin", hero.data.numWineskin);
        emptySlot++;
    }
    if (hero.data.gold > 0 )
    {
        fillBag(emptySlot, "coin", hero.data.gold);
        emptySlot++;
    }
    if (hero.data.brew > 0)
    {
        fillBag(emptySlot, "brew", hero.data.brew);
        emptySlot++;
    }
    if (hero.data.herb > 0)
    {
        fillBag(emptySlot, "herb", hero.data.herb);
        emptySlot++;
    }
    if (hero.data.sheild > 0)
    {
        fillBag(emptySlot, "shield", hero.data.sheild);
        emptySlot++;
    }
    if (hero.data.helm > 0)
    {
        fillBag(emptySlot, "helm", hero.data.helm);
        emptySlot++;
    }
    if (hero.data.bow > 0)
    {
        fillBag(emptySlot, "bow", hero.data.bow);
        emptySlot++;
    }
    if (hero.data.falcon > 0)
    {
        fillBag(emptySlot, "falcon", hero.data.falcon);
        emptySlot++;
    }


}

public int EmptySlot()
{
    for (int i = 0; i < 18; i++)
    {
        if (slots[i].gameObject.GetComponent<Image>().name == "UIMask")
        {
            return i;
        }
    }
    return 0;
}
   } 
    

