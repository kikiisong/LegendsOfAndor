using Photon.Realtime;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bag
{
    [CreateAssetMenu(menuName = "MyAssets/Items")]
    public class Items : ScriptableObject
    {
        public MyDictionary items;
    }

    [Serializable]
    public class MyDictionary : SerializableDictionaryBase<Item.Type, Item> { }

    [Serializable]
    public class Item
    {
        public Sprite icon;

        public enum Type
        {
            GoldCoin, Wineskin
        }
    }

    public static class Helper {
        public static ref int ItemField(this Player player, Item.Type type)
        {
            var hero = player.GetHero();
            switch (type)
            {
                case Item.Type.GoldCoin:
                    return ref hero.data.gold;
                case Item.Type.Wineskin:
                    return ref hero.data.numWineskin;
                default:
                    throw new Exception();
            }
        }

        /*public void UpdateHeroStats(Hero hero, Resource.Type itemType, int updateUnit)
        {
            if (spriteName == "coin") hero.data.gold += updateUnit;
            if (spriteName == "brew") hero.data.brew += updateUnit;
            if (spriteName == "wineskin") hero.data.numWineskin += updateUnit;
            if (spriteName == "herb") hero.data.herb += updateUnit;
            if (spriteName == "shield") hero.data.sheild += updateUnit;
            if (spriteName == "helm") hero.data.helm += updateUnit;
            if (spriteName == "bow") hero.data.bow += updateUnit;
            if (spriteName == "falcon") hero.data.falcon += updateUnit;
        }*/
    }
}
