using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenChicken : MonoBehaviour, TurnManager.IOnSunrise
{
    public AudioSource sunriseSound;

    public void OnSunrise()
    {
        sunriseSound.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        TurnManager.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
