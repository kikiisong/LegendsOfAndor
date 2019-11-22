using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneForOne : MonoBehaviour {

    // Start is called before the first frame update
    //[]
    //public string map;
    public void Start()
    {
        print("Strrt");
    }
    public void ToMap(string m)
    {
        print("It work");
        SceneManager.LoadScene(m);

    }




}
