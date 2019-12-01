using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneForOne : MonoBehaviour
{

    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //RaycastHit hit;
    private void OnMouseDown()
    {
        SceneManager.LoadScene("FightScene");
    }

    //public void Awake()
    //{
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        if (hit.transform.name == "Gor1")
    //        {
    //            Debug.Log("This is a Player");
    //            SceneManager.LoadScene("FightScene");
    //        }
    //        else
    //        {
    //            Debug.Log("This isn't a Player");
    //        }
    //    }


    //}
}
