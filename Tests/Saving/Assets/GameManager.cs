using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SavingSystem;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Saving.SaveGameState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
