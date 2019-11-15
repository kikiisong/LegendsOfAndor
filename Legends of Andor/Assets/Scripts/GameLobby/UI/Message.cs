using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public Text text;

    public void set(object message)
    {
        text.text = (string)message;
    }
}
