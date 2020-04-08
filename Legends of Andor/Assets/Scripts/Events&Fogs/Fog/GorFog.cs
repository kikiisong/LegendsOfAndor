using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GorFog : Fog
{
    public MonsterMoveController gorPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void uncover()
    {
        photonView.RPC("createGor", RpcTarget.AllBuffered, gorPrefab, transform.position, transform.rotation);
    }

    [PunRPC]
    public void createGor(MonsterMoveController gor, Vector3 pos, Quaternion rot)
    {
        Instantiate(gor, pos, rot);

    }
}
