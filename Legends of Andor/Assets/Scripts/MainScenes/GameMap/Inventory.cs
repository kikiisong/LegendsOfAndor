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
        
    }
}
