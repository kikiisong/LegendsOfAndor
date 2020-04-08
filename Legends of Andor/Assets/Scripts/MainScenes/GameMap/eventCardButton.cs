using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventCardButton : MonoBehaviour
{
    public GameObject currentEventCard;

    public void setEventCard(GameObject a)
    {
        currentEventCard = a;
    }

    public void showTheWindow()
    {
        currentEventCard.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
             ///   Debug.Log("Hit " + hitInfo.transform.gameObject.name);
                //if (hitInfo.transform.gameObject.tag == "Construction")
                   // showTheWindow();
            }
            else
            {
                Debug.Log("No hit");
            }
           
        }
    }
}
