using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



using Photon.Pun;


public class popUp : MonoBehaviour
{

	private bool showPopUp = false;
    private bool check = false;
    private int coin;

    private Hero hero;
    public static int locationLabel;
    public static bool isDawrf;

    private string item = null;



    void Start()
    {
        //merchants = GameObject.FindObjectsOfType<Merchant>();

        hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();

    }


    public void testA()
	{
       
        showPopUp = true;
        Init();
    
        // put your if statement here and set showPopUp to true if the condition is true
        // put an else statement to set the bolean to false if it is not true
    }


    


    public void ToMap()
    {
        SceneManager.UnloadSceneAsync("MerchantScene");
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

        // is dawrf
        int amt = 2;
        if (hero.type == Hero.Type.DWARF)
        {
            amt = 1;
        }

        // need to check location
        int heroLocationLabel = hero.data.regionNumber;

        print("hero position" + heroLocationLabel);
        print("merchant position" + locationLabel);


        if (heroLocationLabel == locationLabel) //at the shop
        {
            bool isEnough = hero.data.gold >= 2 || (hero.type == Hero.Type.DWARF && isDawrf == true && hero.data.gold >= 1);
            print(isEnough);

            if (!isEnough)
            {

                print("not enough!");
                GUILayout.BeginHorizontal();
                GUILayout.Label("Buying this object will cost u " + amt + " coins. You don't have enough gold!");
                GUILayout.EndHorizontal();

                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("OK"))
                {
                    showPopUp = false;
                    check = false;
                    // you may put other code to run according to your game too
                }
                GUILayout.EndHorizontal();

            }
            else
            { //enough gold

                GUILayout.BeginHorizontal();
                GUILayout.Label("Buying this object will cost u " + amt + " coins. Do you confirm?");
                GUILayout.EndHorizontal();

                GUILayout.FlexibleSpace();
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Confirm"))
                {
                    showPopUp = false;
                    check = true;


                    // you may put other code to run according to your game
                  
                    buyItem(item);
                    
         


                }
                else if (GUILayout.Button("Cancel"))
                {
                    showPopUp = false;
                    check = false;
                    // you may put other code to run according to your game too
                }
                GUILayout.EndHorizontal();
            }

        }
        else // not at the shop
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("You are not here yet. You can purchase our items when you are at this store!");
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("OK"))
            {
                showPopUp = false;
                check = true;
                // you may put other code to run according to your game too

            }
            GUILayout.EndHorizontal();
        }
        
	}



    


    void buyItem(string item)
    {
        //dwarf hero type
        if (hero.type == Hero.Type.DWARF && isDawrf == true)
        {
            
            hero.data.gold -= 1;
            hero.data.SP += 1;
            print(hero.data.gold); 
        }
        //other hero type
        else
        {  
            hero.data.gold -= 2 ;
            print(hero.data.gold);   
        }


        if (item.Equals("SP"))
        {
            hero.data.SP += 1;
        }

        
        if (item.Equals("helm"))
        {
            hero.data.helm +=1;
        }

        if (item.Equals("brew"))
        {
            hero.data.brew +=1;
        }

        if (item.Equals("shield"))
        {
            hero.data.sheild +=1;
        }

        if (item.Equals("bow"))
        {
            hero.data.bow +=1;
        }



    }



    public void buySP()
    {
        item = "SP";
        showPopUp = true;
        Init();
        item = null;

    }

    public void buyHelm()
    {
        item = "helm";
        showPopUp = true;
        Init();
        item = null;

    }

    public void buyBrew()
    {
        item = "brew";
        showPopUp = true;
        Init();
        item = null;

    }

    public void buyShield()
    {
        item = "shield";
        showPopUp = true;
        Init();
        item = null;

    }

    public void buyBow()
    {
        showPopUp = true;
        item = "bow";
        Init();
        item = null;

    }







}
