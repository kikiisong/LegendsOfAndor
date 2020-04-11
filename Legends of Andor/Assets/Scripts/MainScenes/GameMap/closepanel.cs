using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closepanel : MonoBehaviour
{
    public GameObject shieldOptionPanel;

    public void closePanel()
    {
        transform.parent.gameObject.SetActive(false);
        shieldOptionPanel.SetActive(false);
    }
}
