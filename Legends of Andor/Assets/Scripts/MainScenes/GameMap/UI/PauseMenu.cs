using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject settings;

    public static bool IsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Click_Toggle();
        }
    }

    void Pause()
    {
        IsPaused = true;
        //Time.timeScale = 0.1f;
        settings.SetActive(true);
    }

    void Resume()
    {
        IsPaused = false;
        //Time.timeScale = 1f;
        settings.SetActive(false);
    }

    public void Click_Toggle()
    {
        if (IsPaused) Resume();
        else Pause();
    }
}
