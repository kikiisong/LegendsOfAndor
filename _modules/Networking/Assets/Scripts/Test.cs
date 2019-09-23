using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Text;
using Networking;

public class Test : MonoBehaviour
{
    //byte[] recBuffer = new byte[1024];
    // Use this for initialization
    void Start()
    {
        /*BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        formatter.Serialize(ms, gameObject);

        string send = Encoding.ASCII.GetString(ms.GetBuffer());

        MemoryStream receive = new MemoryStream(Encoding.ASCII.GetBytes(send));
        GameObject test = (GameObject) formatter.Deserialize(receive);
        Debug.Log(test.ToString());*/
        /*
        string send = NetworkUtils.ConvertToString(transform);
        Transform receive = NetworkUtils.ConvertTo<Transform>(send);
        Debug.Log(receive);
        */

        string json = JsonUtility.ToJson(gameObject);
        Debug.Log(json);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
