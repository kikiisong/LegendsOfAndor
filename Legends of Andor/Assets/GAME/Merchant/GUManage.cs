using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUManage : MonoBehaviour
{


    public void ToMap()
    {

        SceneManager.UnloadSceneAsync("MerchantScene");

    }
}
