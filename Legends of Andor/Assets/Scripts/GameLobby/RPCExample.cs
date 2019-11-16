using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPCExample : MonoBehaviourPun
{
    public Image image;

    // Start is called before the first frame update
    void Start()
    {
        int i = PhotonNetwork.CurrentRoom.PlayerCount;
        if (photonView.IsMine)
        {
            image.gameObject.transform.position = SetUp.Instance.spawnPoints[i - 1].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            photonView.RPC("Change", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void Change()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);

        image.color = new Color(r, g, b, 1f);
    }
}
