﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class Witch : MonoBehaviourPun, TurnManager.IOnTurnCompleted, TurnManager.IOnEndDay, TurnManager.IOnMove
{
    public int region;
    public Button brewButton;
    public Renderer witchIcon;
    public bool found;
    public int left;
    public int price;
    public GameObject askWindow;
       
    // Start is called before the first frame update
    void Start()
    {
        askWindow.SetActive(false);
        witchIcon = GetComponent<Renderer>();
        witchIcon.enabled = false;
        brewButton.gameObject.SetActive(false);
        TurnManager.Register(this);
        found = false;
        left = 5;
        Player[] players = PhotonNetwork.PlayerList;
        if(players.Length == 2)
        {
            price = 3;
        }else if(players.Length == 3)
        {
            price = 4;
        }else
        {
            price = 5; //will be 5 when testing with 1 player
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(Player player, Region currentRegion)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            askWindow.SetActive(false);
            brewButton.gameObject.SetActive(false);
        }
    }


    public void OnTurnCompleted(Player player)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            if (found)
            {

                Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();//photonView.Owner is the Scene
                var r = player.GetCurrentRegion();
                List<Witch> witchOnRegion = GameGraph.Instance.FindObjectsOnRegion<Witch>(r.label);

                if (witchOnRegion.Count > 0 && left > 0)
                {
                    brewButton.gameObject.SetActive(true);
                    brewButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    brewButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        int displayPrice = price;
                        if (hero.type == Hero.Type.ARCHER)
                        {
                            displayPrice = price - 1;
                        }
                        Text t = askWindow.transform.GetChild(1).GetComponent<Text>();
                        t.text = "Do you want to buy witch's brew for with  " + displayPrice + " Gold";

                        askWindow.SetActive(true);


                        if (hero.data.gold < displayPrice)
                        {
                            t = askWindow.transform.GetChild(1).GetComponent<Text>();
                            t.text = "You don't have enough gold.";
                            askWindow.transform.GetChild(2).gameObject.SetActive(false);
                        }
                        else
                        {
                            askWindow.SetActive(true);
                            GameObject yesButton = askWindow.transform.GetChild(2).gameObject;
                            yesButton.GetComponent<Button>().onClick.RemoveAllListeners();
                            yesButton.GetComponent<Button>().onClick.AddListener(() =>
                            {
                                photonView.RPC("buyBrew", RpcTarget.AllBuffered, r.label, (int)hero.type, displayPrice);

                                askWindow.SetActive(false);

                            });
                        }

                    });


                }
                else
                {
                    brewButton.gameObject.SetActive(false);
                }

            }

        }

    }

    public void OnEndDay(Player player)
    {

        if (PhotonNetwork.LocalPlayer == player)
        {
            if (found)
            {
                
                    Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();//photonView.Owner is the Scene
                    var r = player.GetCurrentRegion();
                    List<Witch> witchOnRegion = GameGraph.Instance.FindObjectsOnRegion<Witch>(r.label);

                    if (witchOnRegion.Count > 0 && left > 0)
                    {
                        brewButton.gameObject.SetActive(true);
                        brewButton.GetComponent<Button>().onClick.RemoveAllListeners();
                        brewButton.GetComponent<Button>().onClick.AddListener(() =>
                        {
                            int displayPrice = price;
                            if (hero.type == Hero.Type.ARCHER)
                            {
                                displayPrice = price - 1;
                            }
                            Text t = askWindow.transform.GetChild(1).GetComponent<Text>();
                            t.text = "Do you want to buy witch's brew for with  "+ displayPrice + " Gold";

                            askWindow.SetActive(true);
                            

                            if (hero.data.gold < displayPrice)
                            {
                                t = askWindow.transform.GetChild(1).GetComponent<Text>();
                                t.text = "You don't have enough gold.";
                                askWindow.transform.GetChild(2).gameObject.SetActive(false);
                            }
                            else
                            {
                                askWindow.SetActive(true);
                                GameObject yesButton = askWindow.transform.GetChild(2).gameObject;
                                yesButton.GetComponent<Button>().onClick.RemoveAllListeners();
                                yesButton.GetComponent<Button>().onClick.AddListener(() =>
                                {
                                        photonView.RPC("buyBrew", RpcTarget.AllBuffered, r.label, (int)hero.type, displayPrice);

                                        askWindow.SetActive(false);
                                    
                                });
                            }

                        });


                    }
                    else
                    {
                        brewButton.gameObject.SetActive(false);
                    }
                
            }

        }
    }

    [PunRPC]
    public void buyBrew(int currentRegion, int heroType, int paid)
    {
        List<Witch> witchOnRegion = GameGraph.Instance.FindObjectsOnRegion<Witch>(currentRegion);

        Witch temp = witchOnRegion[0];

        temp.left -= 1;

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Hero hero = (Hero)players[i].GetHero();
            if ((int)hero.type == heroType)
            {
                hero.data.gold -= paid;
                hero.data.brew += 2;
                break;
            }
        }
    }

    

    public void locate(int currReg)
    {
        GameGraph.Instance.PlaceAt(gameObject, currReg);
    }

    
}
