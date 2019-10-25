using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text username;
    public Text password;

    [SceneName]
    public string nextScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click_Enter()
    {
        string name = username.text;
        PlayerPrefs.SetString(K.USERNAME, name);
        SceneManager.LoadScene(nextScene);
    }
}


public class K
{
    public static string USERNAME = "username";
}
