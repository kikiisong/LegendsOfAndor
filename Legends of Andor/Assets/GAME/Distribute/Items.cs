using Photon.Realtime;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bag
{
    [CreateAssetMenu(menuName = "MyAssets/Items")]
    public class Items : ScriptableObject
    {
        public MyDictionary items;
    }

    [Serializable]
    public class MyDictionary : SerializableDictionaryBase<ItemType, Item> { }

    [Serializable]
    public class Item
    {
        public Sprite icon;
    }

    public enum ItemType
    {
        Coin, Brew, Wineskin, Herb, Shield, Helm, Bow, Falcon
    }

    public static class Helper {
        private static ref int ItemField(this Player player, ItemType type)
        {
            var hero = player.GetHero();
            switch (type)
            {
                case ItemType.Coin:
                    return ref hero.data.gold;
                case ItemType.Wineskin:
                    return ref hero.data.numWineskin;
                default:
                    throw new Exception();
            }
        }

        public static int GetItemField(this Player player, ItemType type)
        {
            return ItemField(player, type);
        }

        public static void ItemIncrement(this Player player, ItemType type)
        {
            if (type.IsHalfState())
            {
                player.ItemField(type) += 2;
            }
            else
            {
                player.ItemField(type)++;
            }
        }

        public static void ItemDecrement(this Player player, ItemType type)
        {
            player.ItemField(type)--;
        }
    }

    public static class ItemProperties
    {
        public static bool IsHalfState(this ItemType type)
        {
            return new[] { ItemType.Shield, ItemType.Wineskin, ItemType.Brew }.All(t => t == type);
        }
    }
}
