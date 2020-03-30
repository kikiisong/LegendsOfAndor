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
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();
        usernameView.text = PlayerPrefs.GetString(K.Preferences.USERNAME);
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
        //Popup new name created

        PlayerPrefs.SetString(K.Preferences.USERNAME, name); //How to store multiple names
        SceneManager.LoadScene(nextScene);
    }
}