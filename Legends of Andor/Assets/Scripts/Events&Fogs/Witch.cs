using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Witch : MonoBehaviour
{
    public int region;
    public int left;
    
    
   
       
    // Start is called before the first frame update
    void Start()
    {

       

        if (!Room.IsSaved)
        {
            left = 5;
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

   

    

  

    
}
