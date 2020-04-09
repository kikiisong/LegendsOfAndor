using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Bag
{
    public class ItemTakeUI : MonoBehaviour
    {
        public Image icon;
        public TextMeshProUGUI amountUI;

        public Button plus;
        public Button minus;

        public ItemType Type { get; private set; }

        private int ItemAmount
        {
            get
            {
                return PhotonNetwork.LocalPlayer.ItemField(Type);
            }
        }

        private ItemDistributeUI Distributer
        {
            get
            {
                foreach (var i in FindObjectsOfType<ItemDistributeUI>())
                {
                    if (i.Type == Type)
                    {
                        return i;
                    }
                }
                throw new Exception();
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            plus.onClick.AddListener(() =>
            {
                DistributionManager.ClickPlusRPC(this);
                print(ItemAmount);
            });
            minus.onClick.AddListener(() =>
            {
                DistributionManager.ClickMinusRPC(this);
                print(ItemAmount);
            });
        }

        // Update is called once per frame
        void Update()
        {
            amountUI.text = ItemAmount.ToString();
        }

        public void Init(ItemType type)
        {
            Type = type;
            icon.sprite = DistributionManager.Items[type].icon;
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }
    }
}
