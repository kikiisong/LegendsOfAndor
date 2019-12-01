using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUp : MonoBehaviour
{

	private bool showPopUp = false;

    private int coin;

	public void testA()
	{
       
        showPopUp = true;
        Init();
    
        // put your if statement here and set showPopUp to true if the condition is true
        // put an else statement to set the bolean to false if it is not true
    }

    void Init()
    {
        coin = Random.Range(1, 4);
    }

    void OnGUI()
	{
		if (showPopUp)
		{
			GUILayout.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75
				   , 300, 250), ShowGUI, "Buy an Item"); 
		}
	}

	void ShowGUI(int windowID)
	{
        // You may put a label to show a message to the player
        //TODO: coin
        GUILayout.BeginHorizontal();
		GUILayout.Label("Buying this object will cost u " + coin + " coins. Do you confirm?");
        GUILayout.EndHorizontal();
        
        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Confirm"))
		{
			showPopUp = false;
			// you may put other code to run according to your game too
		}
        else if (GUILayout.Button("Cancel"))
        {
            showPopUp = false;
            // you may put other code to run according to your game too
        }
        GUILayout.EndHorizontal();
	}
}
