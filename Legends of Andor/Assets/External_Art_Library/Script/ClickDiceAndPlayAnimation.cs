
using UnityEngine;


public class ClickDiceAndPlayAnimation : MonoBehaviour
{

    Texture2D[] frames;
    int framesPerSecond = 10;
    //AudioClip speakSound;
    void Start() { 
    
        frames = gameObject.GetComponent<Texture2D[]>();
        this.GetComponent<Animation>().Play();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int index = (int)Time.time * framesPerSecond;
            index %= frames.Length;
            Renderer.material.mainTexture = frames[index];

            //audio.PlayOneShot(speakSound);
        }
    }
}
