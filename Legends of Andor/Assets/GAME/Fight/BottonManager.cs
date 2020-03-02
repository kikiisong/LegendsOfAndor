using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottonManager : MonoBehaviour
{ 
    public void changeColor(Button b) {
        Image image = b.GetComponent<Image>();
        image.color = new Color((float)0.30, (float)0.30, (float)0.30);
        Debug.Log(b);
    }
}