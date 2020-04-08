using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*TO DO:
 * ensure we can't drop is groundBag is not active
 * fix -> if 1st action is to drop item --> load items 
 * update region stats
 * save region stats using json
 * show icon
 * 
 * 
 */
public class DropPickManager : MonoBehaviour
{
    // Start is called before the first frame update
    public  GameObject groundBag;
    public  GameObject backpack;
    private int emptySlot = 0;
    private bool oc = false; //open close

    Hero hero
    {
        get
        {
            return (Hero)PhotonNetwork.LocalPlayer.GetHero();
        }
    }

    void Start()
    {
        
    }
  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) opCl();
    }

    //allows to open and close ground panel
    public void openClose(bool b)
    {
        oc = b;
        groundBag.SetActive(b); 
        backpack.SetActive(b);
    }
    public void opCl()
    {
        openClose( oc ? false: true);         
    }

    //typeBag : if 0 then we want to drop item from inventory to ground bag
    //          if 1 do the opposite
    public void dropItem(string spriteName, int typeBag)
    {
        GameObject bag = typeBag == 0 ? groundBag : backpack;

        Debug.Log(spriteName);
        int empty = containsElement(spriteName, typeBag);

        Sprite spriteToLoad = Resources.Load<Sprite>(spriteName);

        //if we already have the element we just need to update its number
        if (empty != -1)
        {
          
            GameObject text = bag.gameObject.transform.GetChild(1).GetChild(empty).GetChild(1).gameObject;
            Text tx = text.gameObject.GetComponent<Text>();

            if (tx.text == "")
            {
                tx.text = "2";
            }
            else
            {
                int v = int.Parse(tx.text);
                v++;
                tx.text = (v).ToString();
            }
           
        }
        else // new element to add
        {
            GameObject image = bag.gameObject.transform.GetChild(1).GetChild(emptySlot).GetChild(0).gameObject;
     
            Image img = image.gameObject.GetComponent<Image>();
            img.sprite = spriteToLoad;
        }

    }


    //function checking whether item users drops already contains on panel
    public int containsElement(string name, int typeBag)
    {

        for (int i = 8; i >= 0; i--)
        {
            GameObject bag = typeBag == 0 ? groundBag : backpack;
    
            GameObject image = bag.gameObject.transform.GetChild(1).GetChild(i).GetChild(0).gameObject;
            Image img = image.gameObject.GetComponent<Image>();
           // Debug.Log(img.sprite.name + " " + name);
            if (img.sprite.name == name)
            {
                return i;
            }
            else if (img.sprite.name == "UIMask")
            {
                emptySlot = i;
            }
        }
        return -1;
    }
    
    public void updateHeroStats(string spriteName, int updateUnit)
    {
        if (spriteName == "coin") hero.data.gold += updateUnit;
        if (spriteName == "brew") hero.data.brew += updateUnit;
        if (spriteName == "wineskin") hero.data.numWineskin += updateUnit;
        if (spriteName == "herb") hero.data.herb += updateUnit;
        if (spriteName == "shield") hero.data.sheild += updateUnit;
        if (spriteName == "helm") hero.data.helm += updateUnit;
        if (spriteName == "bow") hero.data.bow += updateUnit;
        if (spriteName == "falcon") hero.data.falcon += updateUnit;
    }

}
