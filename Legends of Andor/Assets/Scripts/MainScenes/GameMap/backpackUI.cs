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

    public void Open()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);

        }
        emptySlot = 0;

        cleanBag();
        populateBag();
  
    }

    public void fillBag(int slotNumber, string spriteName, int parameter)
{

    GameObject itemsList = GameObject.Find("/Canvas/Backpack/ItemsList");

    Sprite spriteToLoad = Resources.Load<Sprite>(spriteName);

    GameObject image = itemsList.gameObject.transform.GetChild(slotNumber).GetChild(0).gameObject;       
    GameObject text = itemsList.gameObject.transform.GetChild(slotNumber).GetChild(1).gameObject;


    Image img = image.gameObject.GetComponent<Image>();
    Text tx = text.gameObject.GetComponent<Text>();

    img.sprite = spriteToLoad;
    if (parameter > 1)
    {
        tx.text = parameter.ToString();
    }
        if (spriteName == "UIMask" && parameter == 0)
        {
            tx.text = "";
        }
    }
public void populateBag()
{
 
        if (hero.data.wineskin > 0)
    {
        fillBag(emptySlot, "wineskin", hero.data.wineskin);
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
    if (hero.data.shield > 0)
    {
        fillBag(emptySlot, "shield", hero.data.shield);
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
    if (hero.data.numFarmers > 0)
    {
        fillBag(emptySlot, "farmer", hero.data.numFarmers);
        emptySlot++;
    }


    }

public int EmptySlot()
{
    for (int i = 0; i < 9; i++)
    {
        if (slots[i].gameObject.GetComponent<Image>().name == "UIMask")
        {
            return i;
        }
    }
    return 0;
}

    private void cleanBag()
    {
        for (int i = 0; i < 6; i++)
        {
            fillBag(i, "UIMask", 0);
        }
    }
} 
    

