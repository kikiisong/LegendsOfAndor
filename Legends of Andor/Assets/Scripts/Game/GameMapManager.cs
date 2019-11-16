using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapManager : MonoBehaviour
{
    public GameObject heroPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Character character = (Character) PhotonNetwork.LocalPlayer.CustomProperties["character"];
        print(character.type);
        Hero hero = PhotonNetwork.Instantiate(heroPrefab.name, Vector3.zero, Quaternion.identity).GetComponent<Hero>();
        hero.SetUp(character);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
