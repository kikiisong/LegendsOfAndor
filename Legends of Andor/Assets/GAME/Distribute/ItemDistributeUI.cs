using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Bag
{
    public class ItemDistributeUI : MonoBehaviour
    { 
        public Image icon;
        public TextMeshProUGUI amountUI;

        private Item item;
        private int amount;

        public Item.Type Type { get;  private set; }

        private void Update()
        {
            amountUI.text = amount.ToString();
        }

        public void Init(Item.Type type, int amount)
        {
            Type = type;
            item = DistributionManager.Items[type];
            this.amount = amount;

            icon.sprite = item.icon;
        }

        public void Take(ref int yourAmount)
        {
            if (amount == 0) return;
            amount--;
            yourAmount++;
        }

        public void GiveBack(ref int yourAmount)
        {
            if (yourAmount == 0) return;
            yourAmount--;
            amount++;
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }
    }
}
