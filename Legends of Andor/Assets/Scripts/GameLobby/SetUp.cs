using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUp : MonoBehaviour
{
    public static SetUp Instance;

    public Transform[] spawnPoints;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Place(GameObject characterSelect, PhotonView photonView)
    {
        int i = PhotonNetwork.CurrentRoom.PlayerCount;
        print(i);
        if (photonView.IsMine)
        {
            print("t");
            gameObject.transform.position = Instance.spawnPoints[i - 1].position;
        }
    }
}
