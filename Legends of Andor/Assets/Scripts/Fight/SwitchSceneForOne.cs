using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneForOne : MonoBehaviour
{

    private void OnMouseDown()
    {
        SceneManager.LoadScene("FightScene");
    }

    public void ToMap(string m)
    {
        SceneManager.LoadScene(m);
    }

}
