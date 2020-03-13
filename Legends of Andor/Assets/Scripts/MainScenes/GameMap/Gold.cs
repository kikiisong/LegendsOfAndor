using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public TextMeshProUGUI text;
    public int goldValue;
    public int GV
    { 
        get { return goldValue; }
        set
        {
            goldValue = value;
        }

    }

  
    public void increment()
    {
        goldValue++;
    }
    public void decrement()
    {
        goldValue--;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
