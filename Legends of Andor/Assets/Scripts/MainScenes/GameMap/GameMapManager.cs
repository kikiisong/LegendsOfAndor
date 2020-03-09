using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapManager : MonoBehaviour
{
    public static GameMapManager Instance;

    [Header("Instantiate")]
    public GameObject[] archerPrefabs;
    public GameObject[] warriorPrefabs;
    public GameObject[] dwarfPrefabs;
    public GameObject[] wizardPrefabs;

    [Header("TimeMarkers")]
    public List<Transform> timeMarkerUpdatePositions;
    public List<Transform> timeMarkerInitialPositions;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Not singleton");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Hero hero = (Hero) PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        switch (hero.type)
        {
            case Hero.Type.ARCHER:
                Instantiate(archerPrefabs);
                break;
            case Hero.Type.WARRIOR:
                Instantiate(warriorPrefabs);
                break;
            case Hero.Type.DWARF:
                Instantiate(dwarfPrefabs);
                break;
            case Hero.Type.WIZARD:
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
