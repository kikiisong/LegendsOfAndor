using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Realtime;

namespace Bag
{
    public class ItemDistributeUI : MonoBehaviour
    { 
        public Image icon;
        public TextMeshProUGUI amountUI;

        private Item item;
        private int amount;

        public ItemType Type { get;  private set; }

        private void Update()
        {
            amountUI.text = amount.ToString();
        }

        private Dictionary<Player, int> startAmount;

        public void Init(ItemType type, int amount)
        {
            Type = type;
            item = DistributionManager.Items[type];
            this.amount = amount;

            startAmount = new Dictionary<Player, int>();
            foreach(var player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                startAmount[player] = player.GetItemField(type);
            }

            icon.sprite = item.icon;
        }

        private int ZeroedAmount(Player player)
        {
            return player.GetItemField(Type) - startAmount[player];
        }

        public void Take(Player player)
        {
            if (amount == 0) return;
            amount--;
            player.ItemIncrement(Type);
        }

        public void GiveBack(Player player)
        {
            if (ZeroedAmount(player) == 0) return;
            player.ItemDecrement(Type);
            amount++;
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }
    }
}
