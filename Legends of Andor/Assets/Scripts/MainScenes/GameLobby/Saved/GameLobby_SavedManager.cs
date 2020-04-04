using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLobby_SavedManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        foreach(var jObject in Room.Json["heroes"])
        {
            //Hero hero = Hero.FindInResources(jObject["type"].ToObject<Hero.Type>());
        }
    }
}
