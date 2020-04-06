using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMapManager : MonoBehaviour
{
    public static GameMapManager Instance;

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


    // Update is called once per frame
    void Update()
    {
        
    }
}
