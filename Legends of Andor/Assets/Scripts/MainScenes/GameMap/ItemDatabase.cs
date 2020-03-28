using System.Collections;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using LitJson;
//using Item;
using System.IO;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> database = new List<Item>();
    private JsonData itemData;

    // Start is called before the first frame update
    void Start()
    {
       
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
        ConstructItemDatabase();

        Debug.Log(database[0].itemName);
        Debug.Log(database[0].SellPrice) ;
    }
    public Item FetchBySellPrice(int sellprice)
    {
        Debug.Log("Indside fetch by sell price fn");
        for (int i = 0; i < database.Count; i++)
        {
            Debug.Log("sell price "+ database[i].itemName);

            if (database[i].SellPrice == sellprice )
            {
                return database[i];
            }
        }
        return null;
    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            Item obj = ScriptableObject.CreateInstance("Item") as Item;
            obj.Init();
            database.Add(obj);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
