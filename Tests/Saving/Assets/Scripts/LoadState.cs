using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadState : MonoBehaviour
{
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        JObject jObject = SavingSystem.Helper.GetJson("game");
        JArray jArray = (JArray) jObject["array"];
        foreach(var item in jArray.Children())
        {
            var pos = item["position"].ToObject<Vector3>();
            var gameObject = Instantiate(prefab);
            gameObject.transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
