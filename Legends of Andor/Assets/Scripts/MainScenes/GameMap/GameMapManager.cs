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
        PhotonNetwork.Instantiate(heroPrefab.name, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
