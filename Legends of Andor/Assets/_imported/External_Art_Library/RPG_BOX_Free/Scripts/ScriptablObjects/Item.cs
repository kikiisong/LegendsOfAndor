using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class Item : ScriptableObject
{
    public Sprite itemIcon;
    public string itemName;
    public int SellPrice;
    public int BuyPrice;
    public ItemType itemType;
    public Rarity itemRarity;

    [Header("If Misc/Consumable")]
    [TextArea(3, 10)]
    public string Description;
    public bool IsStackable;

    [Header("If Gear")]
    public int LevelReq;
    public StatValue mainStat;
    public GearMainType gearMainType;

    public List<ItemAttribute> Attributes;

    //bag - ?
   // public Dictionary<string, int> stats = new Dictionary<string, int>();
    public int id;
    //constructor - for inventory 
    //public Item(int id, string title, Dictionary<string ,int> stats, bool stackable)
    public Item(int id, string title, bool stackable)
    {
        this.id = id;
        itemIcon = Resources.Load<Sprite>("Assets/_imported/External_Art_Library/RPG_BOX_Free/Resources/ItemsIcons/" + title);
        itemName = title;
        IsStackable = stackable;
        //this.stats = stats;
        
    }

    //constructor - for inventory 
    public Item(Item item)
    {
        itemIcon = Resources.Load<Sprite>("Assets/_imported/External_Art_Library/RPG_BOX_Free/Resources/ItemsIcons/" + itemName);
        itemName = item.itemName;
        IsStackable = item.IsStackable;

    }
}


public enum Rarity
{
    Normal = 0, Rare = 1, Epic = 2,Mythic=3, Legendary = 4
}

public enum GearMainType
{
    Weapon = 0, Armor = 1, Ring = 2, Helmet =3,Amulet=4
}

public enum ItemType
{
    Gear = 0, Misc = 1, Consumable=2
}

public enum MainStat
{
    Attack = 0, Magic = 1, Defence = 2
}

[System.Serializable]
public class StatValue
{
    public MainStat TheStat;
    public int TheValue;

    public StatValue(MainStat MS, int Value)
    {
        TheStat = MS;
        TheValue = Value;
    }
}