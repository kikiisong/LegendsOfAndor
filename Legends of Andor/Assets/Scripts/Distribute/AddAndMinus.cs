using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddAndMinus : MonoBehaviour
{
    Text w;
    Text c;
    Button plusWill;
    Button plusStrength;
    Button minusWill;
    Button minusStrength;
    private int currentscore = 0;
    // Start is called before the first frame update
    void Start()
    {
        w = GetComponentInChildren<Text>();
        c = GetComponentInChildren<Text>();
        w.text = ""+currentscore;
        c.text = ""+currentscore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
