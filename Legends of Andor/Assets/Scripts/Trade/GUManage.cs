using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUManage : MonoBehaviour
{
    // Start is called before the first frame update
    public void ToMap(string m)
    {
        SceneManager.LoadScene(m);
    }


    private bool check = false;

    public void doubleConfirm(Button b) {
        if (check)
        {
            //change the text in button to "Trade"
        }
        else {
            //check if the the player confirm
            //any change should cancel the confirm
            
        }

    }
}
