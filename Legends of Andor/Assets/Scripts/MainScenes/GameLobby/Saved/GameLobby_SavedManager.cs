using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saving;
using Newtonsoft.Json.Linq;

public class GameLobby_SavedManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        foreach(var jObject in Room.Json["heroes"])
        {
            Hero hero = J.ToHero(jObject);
            print(hero.data);
        }
    }
}
