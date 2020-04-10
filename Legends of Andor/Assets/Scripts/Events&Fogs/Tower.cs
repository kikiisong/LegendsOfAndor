using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    //TODO call (once) PhotonNetwork.Instantiate(towerPrefab and then monsterPrefab, GameGraph.Instance.Find(label).position, Quaternion.identity);
    //  id = photonView.ViewID of monster (to make sure it's the correct one if there was already a monster on the same region)
    //TODO call castle.InitRPC(monster) (only once)

    public class Tower : MonoBehaviourPun
    {
        MonsterMoveController linkedMonster;

        // Update is called once per frame
        void Update()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if(linkedMonster == null) //TODO not sure if it works
                {
                    PhotonNetwork.Destroy(gameObject); //TODO better way?
                }
            }
        }
        
        public void InitRPC(PhotonView photonView)
        {
            var id = photonView.ViewID;
            photonView.RPC("InitTower", RpcTarget.All, id);
        }

        [PunRPC]
        public void InitTower(int id)
        {
            var monster = PhotonView.Find(id);
            linkedMonster = monster.GetComponent<MonsterMoveController>();
            linkedMonster.canMove = false;
        }
    }
}
