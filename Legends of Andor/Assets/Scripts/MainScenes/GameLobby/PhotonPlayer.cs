using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviourPun
{
    public GameObject myAvatar;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            //myAvatar = PhotonNetwork.Instantiate(Path.Combine(), )
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
