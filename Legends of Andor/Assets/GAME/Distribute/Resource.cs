using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Resource : MonoBehaviour
{
    public Type type;
    public int amount;

    [Header("UI")]
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        Display();
    }

    void Display()
    {
        text.text = amount.ToString();
    }

    public void Take(ref int yourAmount)
    {
        if (amount == 0) return;          
        amount--;
        yourAmount++;
        Display();  
    }

    public void GiveBack(ref int yourAmount)
    {
        if (yourAmount == 0) return;
        yourAmount--;
        amount++;
        Display();
    }

    public enum Type
    {
        GoldCoint, Wineskin
    }
}
