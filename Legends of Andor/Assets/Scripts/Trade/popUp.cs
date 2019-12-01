using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUp : MonoBehaviour
{

	private bool showPopUp = false;

	public void testA()
	{
       
       showPopUp = true;
    
        // put your if statement here and set showPopUp to true if the condition is true
        // put an else statement to set the bolean to false if it is not true

    }

	void OnGUI()
	{
		if (showPopUp)
		{
			GUI.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75
				   , 300, 250), ShowGUI, "Buy an Item");

		}
	}

	void ShowGUI(int windowID)
	{
        // You may put a label to show a message to the player
        //TODO: coin
        int coin = 2;
		GUI.Label(new Rect(65, 40, 200, 50), "Buying this object will cost u " +coin+ " coins. Do you confirm?");

		// You may put a button to close the pop up too

		if (GUI.Button(new Rect(50, 150, 75, 30), "OK"))
		{
			showPopUp = false;
			// you may put other code to run according to your game too
		}

	}
}
