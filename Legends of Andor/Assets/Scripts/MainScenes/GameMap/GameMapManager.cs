using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapManager : MonoBehaviour
{
    public GameObject[] archerPrefabs;
    public GameObject[] warriorPrefabs;
    public GameObject[] dwarfPrefabs;
    public GameObject[] wizardPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        HeroUIData heroUIData = (HeroUIData) PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        switch (heroUIData.type)
        {
            case HeroType.ARCHER:
                Instantiate(archerPrefabs);
                break;
            case HeroType.WARRIOR:
                Instantiate(warriorPrefabs);
                break;
            case HeroType.DWARF:
                Instantiate(dwarfPrefabs);
                break;
            case HeroType.WIZARD:
                Instantiate(wizardPrefabs);
                break;
        }
    }

    private void Instantiate(GameObject[] prefabs) {
        foreach(GameObject prefab in prefabs)
        {
            PhotonNetwork.Instantiate(prefab);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
