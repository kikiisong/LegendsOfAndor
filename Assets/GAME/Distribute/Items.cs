﻿using Photon.Realtime;
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
        Coin, Brew, Wineskin, Herb, Shield, Helm, Bow, Falcon, Telescope, WillPower
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
                    return ref hero.data.wineskin;
                case ItemType.Falcon:
                    return ref hero.data.falcon;
                case ItemType.Shield:
                    return ref hero.data.shield;
                case ItemType.Helm:
                    return ref hero.data.helm;
                case ItemType.Bow:
                    return ref hero.data.bow;
                case ItemType.Telescope:
                    return ref hero.data.telescope;
                case ItemType.Brew:
                    return ref hero.data.brew;
                case ItemType.Herb:
                    return ref hero.data.herb;
                case ItemType.WillPower:
                    return ref hero.data.WP;
                default:
                    throw new Exception();
            }
        }



        public static int GetItemField(this Player player, ItemType type)
        {
            return ItemField(player, type);
        }


        public static int GetDisplayItem(this Player player, ItemType type, out bool halfConsumed)
        {
            int value = GetItemField(player, type);
            halfConsumed = false;
            if (type.IsHalfState())
            {
                halfConsumed = value % 2 == 1;
                value /= 2;
            }
            return value;
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

        public static bool HasItem(this Player player, ItemType type)
        {
            return player.ItemField(type) > 0;
        }

        public static int NumSmallItems(this Player player)
        {
            var count = 0;
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                if (type.IsSmallItem())
                {
                    if (player.HasItem(type)) count++;
                }
            }
            return count;
        }

        public static bool HasLargeItem (this Player player)
        {
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                if (type.IsLargeItem() && player.HasItem(type)) return true;
            }
            return false;
        }
    }

    public static class ItemProperties
    {
        public static bool IsHalfState(this ItemType type)
        { 
            return new[] { ItemType.Shield, ItemType.Wineskin, ItemType.Brew }.Any(t => t == type);
        }

        public static bool IsSmallItem(this ItemType type)
        {
            return new[] {ItemType.Wineskin, ItemType.Brew, ItemType.Herb, ItemType.Telescope }.Any(t => t == type);
        }

        public static bool IsLargeItem(this ItemType type)
        {
            return new[] { ItemType.Shield, ItemType.Bow, ItemType.Falcon }.Any(t => t == type);
        }

    }
}
