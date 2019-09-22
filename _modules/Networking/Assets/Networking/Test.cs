using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Text;

public class Test : MonoBehaviour
{
    //byte[] recBuffer = new byte[1024];
    // Use this for initialization
    void Start()
    {
        int msg = 123456789;
        BinaryFormatter formatter = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        formatter.Serialize(ms, msg);

        string send = Encoding.ASCII.GetString(ms.GetBuffer());

        MemoryStream receive = new MemoryStream(Encoding.ASCII.GetBytes(send));
        int test = (int) formatter.Deserialize(receive);
        Debug.Log(test);

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
