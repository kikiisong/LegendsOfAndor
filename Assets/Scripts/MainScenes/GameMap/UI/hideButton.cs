using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideButton : MonoBehaviour
{
    public GameObject showButton; 
    // Start is called before the first frame update
    void Start()
    {
        showButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hidePanel()
    {
        transform.parent.gameObject.SetActive(false);
        showButton.SetActive(true);
    }
}
