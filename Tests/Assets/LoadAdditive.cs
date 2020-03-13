using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAdditive : MonoBehaviour
{
    public string nextScene;

    bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        print("Start Called");
    }

    void OnGUI()
    {
        if (open)
        {
            if(GUI.Button(new Rect(0, 0, 200, 100), "Unoad Scene"))
            {
                SceneManager.UnloadSceneAsync(nextScene);
                open = false;
            }
        }
        else if(GUI.Button(new Rect(0, 0, 200, 100), "Load Additive"))
        {
            SceneManager.LoadScene(nextScene, LoadSceneMode.Additive);
            open = true;
        }
    }
}
