using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<Item> charItems = new List<Item>();
    public ItemDatabase itemDatabase;

    // Start is called before the first frame update
    void Start()
    {
        GiveItem(1);
    }
    public void GiveItem(int id)
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        charItems.Add(itemToAdd);
        Debug.Log("Added item: " + itemToAdd.itemName);
    }
    // Update is called once per frame
    void Update()
    {

    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase database;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();

    int slotAmount;

    [System.Obsolete]
    void Start()
    {

        database = GetComponent<ItemDatabase>();

        slotAmount = 9;
        inventoryPanel = GameObject.Find("InventoryPanel");
        slotPanel = inventoryPanel.transform.FindChild("SlotPanel").gameObject;
       // slotPanel = GameObject.Find("SlotPanel");
        for (int i = 0; i < slotAmount; i++)
        {
            Item obj = (ScriptableObject.CreateInstance("Item") as Item);
            obj.Init();
            items.Add(obj);
           
            slots.Add(Instantiate(inventorySlot));
            //now slots will be the child of the slotpanale, so they will inherit
            //the layout == avoid the issue of layering
            slots[i].transform.SetParent(slotPanel.transform);
        }
        AddItem(-1);
    
    }

    public void AddItem(int sellprice)
    {
        Debug.Log("before fetchby sell price");
        Item itemToAdd = database.FetchBySellPrice(sellprice);
        Debug.Log("after sell price ");
        Debug.Log("the price is  " + itemToAdd.itemName);
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].SellPrice == sellprice)
            {
              
                items[i] = itemToAdd;
               // Sprite img = itemToAdd.itemIcon;
                GameObject itemObj = Instantiate(inventoryItem);

               // GameObject itemObj = new GameObject("Test");
               // SpriteRenderer renderer = itemObj.AddComponent<SpriteRenderer>();
                //renderer.sprite = img;
                Debug.Log("We're inside the addditem fn");

                itemObj.transform.SetParent(slots[i].transform);
                itemObj.transform.position = Vector2.zero;

                break;
            }
        }

    }
}
