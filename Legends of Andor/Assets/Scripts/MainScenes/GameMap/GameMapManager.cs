using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapManager : MonoBehaviour
{
    public static GameMapManager Instance;

    [Header("Instantiate")]
    public GameObject heroPrefab;
    public GameObject timeMarkerPrefab;

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
        PhotonNetwork.Instantiate(heroPrefab);
        PhotonNetwork.Instantiate(timeMarkerPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
