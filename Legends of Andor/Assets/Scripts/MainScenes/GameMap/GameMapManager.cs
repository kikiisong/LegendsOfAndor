using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapManager : MonoBehaviour
{
    public GameObject archerPrefab;
    public GameObject warriorPrefab;
    public GameObject dwarfPrefab;
    public GameObject wizardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        HeroUIData heroUIData = (HeroUIData) PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        GameObject prefab = null;
        switch (heroUIData.type)
        {
            case HeroType.ARCHER:
                prefab = archerPrefab;
                break;
            case HeroType.WARRIOR:
                prefab = warriorPrefab;
                break;
            case HeroType.DWARF:
                prefab = dwarfPrefab;
                break;
            case HeroType.WIZARD:
                prefab = wizardPrefab;
                break;
        }

        if(prefab != null)
        {
            PhotonNetwork.Instantiate(prefab.name, Vector3.zero, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
