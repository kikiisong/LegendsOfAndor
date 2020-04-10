using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/*TO DO:
 *
 * save region stats using json (+++++)
 * warn block if one wants to add more elements that they are allowed (--)
 * destroy object in 
 */
public class DropPickManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject groundBag;
    public GameObject backpack;
    public GameObject goldpotPrefab;


    private int groundSize = 9;
    private int backpackSize = 6;
    private int emptySlot = 0;
    public int objGroundBag = 0;
    private bool oc = false; //open close

    Region Current
    {
        get
        {
            //Extract current player's region
            foreach (HeroMoveController c in GameObject.FindObjectsOfType<HeroMoveController>())
            {
                if (c.photonView.Owner == PhotonNetwork.LocalPlayer)
                {
                    return GameGraph.Instance.FindNearest(c.transform.position);
                }
            }
            throw new System.Exception("No current region");
        }
    }

    Hero hero
    {
        get
        {
            return (Hero)PhotonNetwork.LocalPlayer.GetHero();
        }
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

        cleanBag(); //when closing bag clean it

        loadItemsOnRegion(); //load data from current region

        groundBag.SetActive(b);
        backpack.SetActive(b);
    }

    public void opCl()
    {
        openClose(oc ? false : true);
    }


    // UI responsible function 
    //typeBag : if 0 then we want to drop item from inventory to ground bag
    //          if 1 do the opposite
    public void dropItem(string spriteName, int typeBag)
    {
        GameObject bag = typeBag == 0 ? groundBag : backpack;

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
        GameObject bag = typeBag == 0 ? groundBag : backpack;
        int size = typeBag == 0 ? groundSize : backpackSize;

        for (int i = size - 1; i >= 0; i--)
        {
            //GameObject bag = typeBag == 0 ? groundBag : backpack;

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
        if (spriteName == "shield") hero.data.shield += updateUnit;
        if (spriteName == "helm") hero.data.helm += updateUnit;
        if (spriteName == "bow") hero.data.bow += updateUnit;
        if (spriteName == "falcon") hero.data.falcon += updateUnit;
    }


    public void updateRegionStats(string spriteName, int updateUnit)
    {

        if (spriteName == "coin") Current.data.gold += updateUnit;
        if (spriteName == "brew") Current.data.brew += updateUnit;
        if (spriteName == "wineskin") Current.data.numWineskin += updateUnit;
        if (spriteName == "herb") Current.data.herb += updateUnit;
        if (spriteName == "shield") Current.data.sheild += updateUnit;
        if (spriteName == "helm") Current.data.helm += updateUnit;
        if (spriteName == "bow") Current.data.bow += updateUnit;
        if (spriteName == "falcon") Current.data.falcon += updateUnit;

        Current.data.numOfItems += updateUnit;

        if (Current.data.numOfItems == 0) displayRegionIcon(false);
        if (Current.data.numOfItems == 1) displayRegionIcon(true);

    }

    public void displayRegionIcon(bool display)
    {
        if (display)
        {

            var missle = Instantiate<GameObject>(goldpotPrefab);
            GameGraph.Instance.PlaceAt(missle, Current.label);
        }
        else
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("goldpot"))
            {
                if (obj.transform.position == Current.position)
                {
                    Destroy(obj); //change it to 
                }
            }
        }
    }


    private void loadItemsOnRegion()
    {
        Debug.Log("total numebr of items " + Current.data.numOfItems);
        emptySlot = 0;
        if (Current.data.numWineskin > 0)
        {
            fillBag(emptySlot, "wineskin", Current.data.numWineskin);
            emptySlot++;
        }
        if (Current.data.gold > 0)
        {
            fillBag(emptySlot, "coin", Current.data.gold);
            emptySlot++;
        }
        if (Current.data.brew > 0)
        {
            fillBag(emptySlot, "brew", Current.data.brew);
            emptySlot++;
        }
        if (Current.data.herb > 0)
        {
            fillBag(emptySlot, "herb", Current.data.herb);
            emptySlot++;
        }
        if (Current.data.sheild > 0)
        {
            fillBag(emptySlot, "shield", Current.data.sheild);
            emptySlot++;
        }
        if (Current.data.helm > 0)
        {
            fillBag(emptySlot, "helm", Current.data.helm);
            emptySlot++;
        }
        if (Current.data.bow > 0)
        {
            fillBag(emptySlot, "bow", Current.data.bow);
            emptySlot++;
        }
        if (Current.data.falcon > 0)
        {
            fillBag(emptySlot, "falcon", Current.data.falcon);
            emptySlot++;
        }

    }
    private void cleanBag()
    {
        for (int i = 0; i < groundSize; i++)
        {
            fillBag(i, "UIMask", 0);
        }
    }

    //UI function:
    // simply loads images of data of a region
    public void fillBag(int slotNumber, string spriteName, int parameter)
    {

        GameObject itemsList = groundBag.gameObject.transform.GetChild(1).gameObject;
        Sprite spriteToLoad;
        if (spriteName == "UIMask")
        {
            // spriteToLoad = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UIMask.psd");
            spriteToLoad = Resources.Load<Sprite>("UIMask");
        }
        else
        {
            spriteToLoad = Resources.Load<Sprite>(spriteName);
        }

        GameObject image = itemsList.gameObject.transform.GetChild(slotNumber).GetChild(0).gameObject;
        GameObject text = itemsList.gameObject.transform.GetChild(slotNumber).GetChild(1).gameObject;

        Image img = image.gameObject.GetComponent<Image>();
        Text tx = text.gameObject.GetComponent<Text>();

        img.sprite = spriteToLoad;
        if (parameter > 1)
        {
            tx.text = parameter.ToString();
        }
    }
}
