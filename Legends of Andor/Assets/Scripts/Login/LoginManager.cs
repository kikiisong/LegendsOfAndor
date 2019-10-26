using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public InputField usernameView;
    public InputField passwordView;

    [SceneName]
    public string nextScene;
 

    // Start is called before the first frame update
    void Start()
    {
        //Last username
        usernameView.text = PlayerPrefs.GetString(K.USERNAME);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Click_Enter()
    {
        string name = usernameView.text;
        string password = passwordView.text;

        /* Verify password and username?
          string actualPassword = PlayerPrefs.GetString(K.PASSWORD);
        
        if (actualPassword.Equals(""))
        {
            PlayerPrefs.SetString(K.PASSWORD, password);
        }else if (!actualPassword.Equals(password) )
        {
            //error popup
        }*/

        PlayerPrefs.SetString(K.USERNAME, name);
        SceneManager.LoadScene(nextScene);
    }
}

//Keys
public class K
{
    public static string USERNAME = "username";
    public static string PASSWORD = "password";
}
