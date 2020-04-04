using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backpackUI : MonoBehaviour
{
    public GameObject Panel;

    public void Open()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }
    
}
