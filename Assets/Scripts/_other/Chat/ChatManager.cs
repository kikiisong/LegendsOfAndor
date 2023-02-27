using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click_Close(GameObject closed)
    {
        closed.SetActive(false);
    }

    public void Click_Open(GameObject opened)
    {
        opened.SetActive(true);
    }
}
