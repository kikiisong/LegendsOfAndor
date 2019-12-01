using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{

    // Start is called before the first frame update
    int numberRolling = 0;
    bool rollAll = true;
    bool rollingState = true;

    private void OnMouseDown()
    {
        rollDice();
    }

    //Character character
    void Start()
    {
        //initialize dice number
        //hero type
       /* if (character.type.Equals("ARCHER"))
        {
            rollAll = false;
        }
        // TODO: Need add some attriute in the hero class
        //For now just assume row 3 for everyone
        */
        numberRolling = 3;
        
    }

    ArrayList result = new ArrayList();
    private bool showPopUp = false;
    // Update is called once per frame
    private void rollDice() {
        for (int i = this.numberRolling; i > 0; i--)
        {
            result.Add(randGenerator());
        }
        Debug.Log(result);
        showPopUp = true;
        
    }

    private static int randGenerator()
    {
        int min = 1;
        int max = 6;
        return Random.Range(min, max);
    }

    

    void OnGUI()
    {
        if (showPopUp)
        {
            GUILayout.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75
                   , 300, 250), ShowGUI, "RollDice");
        }
    }

    void ShowGUI(int windowID)
    {
        // You may put a label to show a message to the player
        GUILayout.BeginHorizontal();
        GUILayout.Label("Roll Dice result " + printArrayList(result) +
            " You now can choose to to apply skill, or Confirm to attack!");
        
        GUILayout.EndHorizontal();

        GUILayout.FlexibleSpace();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Confirm"))
        {
            showPopUp = false;
            // you may put other code to run according to your game too
        }

        GUILayout.EndHorizontal();
    }

    
    string printArrayList(ArrayList a) {
        string r = "";
        int max = 0;
        for (int i = 0; i < numberRolling; i++) {
            r += a[i] + " ";
            if ((int)a[i] > max)
            {
                max = (int)a[i];

            }
        }
        r += ". The max is " + max + " .";
        return r;
    }


    //TODO: consider that change result how to update GUI

    //TODO: many check condition
    //1. check for resources
    //2. check for prefession
    //3. check for conflict

    bool Magic = false;
    public void useMagic() {
        //check for prefession
        Magic = true;
    }

    bool HerbS = false;
    public void useHerbStrength()
    { 
        HerbS = true;
        //TODO: need to get the herbs number of hero
        //assmu 3 now.
    }

    bool Brew = false;
    public void useBrew()
    {
        if (Helm != true)
        {
            Brew = true;
        }
        else
        {
            //Maybe pop some warning message
        }
      
    }

    bool Helm = false;
    public void useHelm()
    {
        if (Brew != true)
        {
            Helm = true;
        }
        else {
            // maybe pop some warning
        }
        
    }

    private void initial() {
        //after each round initialize everything
        Magic = false;
        Brew = false;
        Helm = false;
        HerbS = false;

            }

    
}
