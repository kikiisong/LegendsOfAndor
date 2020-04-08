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
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, float.MaxValue, LayerMask.GetMask("EventCardButton")))
            {
                showTheWindow();
            }
            else
            {
                Debug.Log("No card hit");
            }

        }
    }

}
