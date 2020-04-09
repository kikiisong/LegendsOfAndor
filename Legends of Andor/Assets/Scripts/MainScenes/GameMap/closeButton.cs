using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeButton : MonoBehaviour
{
    public void closePanel()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
