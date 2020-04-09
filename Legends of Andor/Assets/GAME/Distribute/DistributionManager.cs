using Bag;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bag
{
    public class DistributionManager : MonoBehaviourPun
    {
        public static DistributionManager Instance;

        public static MyDictionary Items
        {
            get
            {
                return Instance.items.items;
            }
        }

        [Header("Items")]
        public Items items;
        public GameObject parent;
        public ItemDistributeUI itemPrefab;

        [Header("Take")]
        public GameObject takeParent;
        public ItemTakeUI itemTakePrefab;

        [Header("UI")]
        public Button next;

     

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            if(PhotonNetwork.IsMasterClient) Distribute((Item.Type.GoldCoin, 5), (Item.Type.Wineskin, 2));
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Distribute(params (Item.Type type, int amount)[] pairs)
        {
            var players = PhotonNetwork.CurrentRoom.Players.Values;
            var array = new Player[players.Count];
            players.CopyTo(array, 0);
            Distribute(array, pairs);
        }

        public void Distribute(Player[] players, params (Item.Type type, int amount)[] pairs)
        {
            photonView.RPC("BeginDistribute", RpcTarget.All);
            foreach(var (type, amount) in pairs)
            {
                photonView.RPC("AddDistributeItem", RpcTarget.All, type, amount);

                photonView.RPC("AddTakeItem", RpcTarget.All, type, players);
            }
        }

        [PunRPC]
        public void BeginDistribute()
        {
            //active true
        }

        [PunRPC]
        public void AddDistributeItem(Item.Type type, int amount)
        {
            var distribute = Instantiate(itemPrefab);
            distribute.transform.SetParent(parent.transform, false);
            distribute.Init(type, amount);
        }

        [PunRPC]
        public void AddTakeItem(Item.Type type, params Player[] players)
        {
            if (Array.Exists(players, p => p == PhotonNetwork.LocalPlayer))
            {
                var take = Instantiate(itemTakePrefab);
                take.transform.SetParent(takeParent.transform, false);
                take.Init(type);
            }
        }

        [PunRPC]
        public void EndDistribute()
        {
            //click confirm, active false
        }

        public static void ClickPlusRPC(ItemTakeUI takeUI)
        {
            Instance.photonView.RPC("ClickPlus", RpcTarget.All, PhotonNetwork.LocalPlayer, (int)takeUI.Type);
        }

        [PunRPC]
        public void ClickPlus(Player player, Item.Type type)
        {
            foreach (var i in FindObjectsOfType<ItemDistributeUI>())
            {
                if (i.Type == type)
                {
                    i.Take(ref player.ItemField(type));
                }
            }
        }

        public static void ClickMinusRPC(ItemTakeUI takeUI)
        {
            Instance.photonView.RPC("ClickMinus", RpcTarget.All, PhotonNetwork.LocalPlayer, (int)takeUI.Type);
        }

        [PunRPC]
        public void ClickMinus(Player player, Item.Type type)
        {
            foreach (var i in FindObjectsOfType<ItemDistributeUI>())
            {
                if (i.Type == type)
                {
                    i.GiveBack(ref player.ItemField(type));
                }
            }
        }


    }

}