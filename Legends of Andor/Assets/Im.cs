using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Im : MonoBehaviour
{

    public DropPickManager i;
    public GameObject slot;


    public void OnMouseDown()
    {
        if (i.groundBag.active)
        {
            GameObject image = slot.transform.GetChild(0).gameObject;
            Image img = image.gameObject.GetComponent<Image>();

            GameObject text = slot.transform.GetChild(1).gameObject;
            Text tx = text.gameObject.GetComponent<Text>();

            Sprite uimask = Resources.Load<Sprite>("UIMask");

            GameObject calledBag = slot.transform.parent.parent.gameObject;

            int bagType = calledBag.name == "GroundBag" ? 1 : 0;
            //   Debug.Log(calledBag.name + "bad type " + bagType);

            // usure that clicking on empty icon won't do anything
            if (img.sprite.name != uimask.name)
            {

                if (tx.text == "")
                {
                    i.dropItem(img.sprite.name, bagType);

                    //update region stats
                    int updUnit = bagType == 1 ? -1 : 1;
                    i.updateRegionStats(img.sprite.name, updUnit);

                    img.sprite = uimask;
                }
                else
                {

                    int count = int.Parse(tx.text);
                    count--;
                    i.dropItem(img.sprite.name, bagType);

                    //update region stats
                    int updUnit = bagType == 1 ? -1 : 1;
                    i.updateRegionStats(img.sprite.name, updUnit);

                    //empty slot if dropped item
                    if (count != 0)
                    {
                        tx.text = (count).ToString();
                    }
                    else
                    {
                        img.sprite = uimask;
                        tx.text = "";
                    }
                }

                //update hero stats
                int updateUnit = bagType == 0 ? -1 : 1;
                i.updateHeroStats(img.sprite.name, updateUnit);

            }
        }
    }


}
