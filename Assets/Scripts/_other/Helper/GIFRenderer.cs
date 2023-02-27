using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GIFRenderer : MonoBehaviour
{
    public int framesPerSecond = 10;
    public Texture2D[] frames;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float index = Time.time * framesPerSecond % frames.Length;
        //GetComponent<SpriteRenderer>().material.mainTexture = frames[(int) index];
        Texture2D texture = frames[(int)index];
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
