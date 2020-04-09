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
        public GameObject panel;
        public Button confirm;

     

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            confirm.onClick.AddListener(() =>
            {
                photonView.RPC("EndDistribute", RpcTarget.All);
            });
        }

        // Update is called once per frame
        void Update()
        {

        }

        public static void Distribute(params (ItemType type, int amount)[] pairs)
        {
            var players = PhotonNetwork.CurrentRoom.Players.Values;
            var array = new Player[players.Count];
            players.CopyTo(array, 0);
            Distribute(array, pairs);
        }

        public static void Distribute(Player[] players, params (ItemType type, int amount)[] pairs)
        {
            Instance.photonView.RPC("BeginDistribute", RpcTarget.All);
            foreach(var (type, amount) in pairs)
            {
                Instance.photonView.RPC("AddDistributeItem", RpcTarget.All, type, amount);
                Instance.photonView.RPC("AddTakeItem", RpcTarget.All, type, players);
            }
        }

        [PunRPC]
        public void BeginDistribute()
        {
            panel.SetActive(true);
        }

        [PunRPC]
        public void AddDistributeItem(ItemType type, int amount)
        {
            var distribute = Instantiate(itemPrefab);
            distribute.transform.SetParent(parent.transform, false);
            distribute.Init(type, amount);
        }

        [PunRPC]
        public void AddTakeItem(ItemType type, params Player[] players)
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
            panel.SetActive(false);
        }

        public static void ClickPlusRPC(ItemTakeUI takeUI)
        {
            Instance.photonView.RPC("ClickPlus", RpcTarget.All, PhotonNetwork.LocalPlayer, (int)takeUI.Type);
        }

        [PunRPC]
        public void ClickPlus(Player player, ItemType type)
        {
            foreach (var i in FindObjectsOfType<ItemDistributeUI>())
            {
                if (i.Type == type)
                {
                    i.Take(player);
                }
            }
        }

        public static void ClickMinusRPC(ItemTakeUI takeUI)
        {
            Instance.photonView.RPC("ClickMinus", RpcTarget.All, PhotonNetwork.LocalPlayer, (int)takeUI.Type);
        }

        [PunRPC]
        public void ClickMinus(Player player, ItemType type)
        {
            foreach (var i in FindObjectsOfType<ItemDistributeUI>())
            {
                if (i.Type == type)
                {
                    i.GiveBack(player);
                }
            }
        }


    }

}