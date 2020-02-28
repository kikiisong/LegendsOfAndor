using UnityEngine;
using System.Collections;


public class FightManager : MonoBehaviour
{
    Fight fight;
    public Fight getFight()
    {

        return this.fight;
    }
    public void heroAttack(Hero h) {
        /*if (fight.getFightState() != FightState.HERO) {
            return;
        }
        int max = 0;
        if (h.getcharacterType() == CharacterType.ARCHER)
        {
            //TODO: something different  getdice

            
                    while
                        displayone
                        if (nexxt)
                            displaynext
                        else
                        {
                            break;
                        }
                }
                else {
                    //max update
                    //displayResult
                }
            

            //ask for technique
            //notice the constraints
            //update the attack numebr
            //other constaints should be in hero and u can get


            //check any coop wanna help current hero



        }*/
    }
 


    public static bool calculateRound(int HeroAttack, int MonsterAttack) {

        //true is return if hero win, false if monster win 
        //TODO:? where to update GUI
        return true;

    }


    public void UpdateGUI() {

        //loop thorought all the hero
        //also the monster
        //to update the information 
    }
    
    /*
     *    
    bool rollAll = true;

    void Hero Attack(){
    //Step1: check if the correct state
    //Step2: perform the rolling, slient different for Archer and other profession
    //Step3: ask for applying the rolling 
    
    }

     

    //Character character
    void Start()
    {

        Character character = (Character)photonView.Owner.CustomProperties["character"];
        print(character.type);
        if (character.type==CharacterType.ARCHER) {
            rollAll = false;
        }
        
        //hero type
         // TODO: Need add some attriute in the hero class
         //For now just assume row 3 for everyone
         
        numberRolling = 3;
        
    }

    ArrayList result = new ArrayList();
    private bool showPopUp = false;
    // Update is called once per frame



    

    void OnGUI()
    {
        if (showPopUp)
        {
            GUILayout.Window(0, new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 75
                   , 250, 200), ShowGUI, "RollDice");
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

    */
}
