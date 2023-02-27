using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showButton : MonoBehaviour
{
    public GameObject infoPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPanel()
    {
        transform.parent.gameObject.SetActive(false);
        infoPanel.SetActive(true);
    }
}
