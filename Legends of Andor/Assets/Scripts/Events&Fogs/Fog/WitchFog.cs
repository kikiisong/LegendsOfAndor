using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class WitchFog : Fog
{
    public Witch witchPrefab;
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
        photonView.RPC("createGor", RpcTarget.AllBuffered, region, witchPrefab, transform.position, transform.rotation);

        //the current player gets a brew
    }

    [PunRPC]
    public void createWitch(int regionlabel, Witch witch, Vector3 pos, Quaternion rot)
    {
        Witch myWitch = Instantiate(witchPrefab, transform.position, transform.rotation);
        myWitch.region = region;

    }
}
