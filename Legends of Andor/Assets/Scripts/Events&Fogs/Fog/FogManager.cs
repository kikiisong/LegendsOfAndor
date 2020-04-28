using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using Card;

public class FogManager : MonoBehaviourPun, TurnManager.IOnTurnCompleted, TurnManager.IOnEndDay
{
    public MonsterMoveController gorPrefab;
    public Button brewButton;
    public GameObject fogInfo;
    public GameObject herbDice;
    public GameObject herbGorPrefab;
    public GameObject narrator;
    public GameObject fogPrefab;
    public GameObject witchPrefab;

    // Start is called before the first frame update
    void Start()
    {
        fogInfo.SetActive(false);
        herbDice.SetActive(false);
        TurnManager.Register(this);
        if (!Room.IsSaved)
        {
            int[] regions = { 8, 11, 12, 13, 16, 32, 46, 44, 42, 64, 63, 56, 47, 48, 49 };
            Shuffle(regions);
            //TODO assign types to fogs
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("Typing", RpcTarget.AllBuffered, regions);
            }
        }
    }

    void Shuffle(int[] list)
    {
        System.Random rand = new System.Random();
        int n = list.Length;
        while (n > 1)
        {
            n--;
            int k = rand.Next(n + 1);
            int temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTurnCompleted(Player player)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            Hero hero = player.GetHero();
            var r = player.GetCurrentRegion();
            List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(r);

            if (fogOnRegion.Count > 0)
            {

                showInfo(fogOnRegion[0].type);
                Uncover(r.label, (int)hero.type);

            }

        }

    }

    public void showInfo(FogType ft)
    {
        if (ft == FogType.SP)
        {
            Text t = fogInfo.transform.GetChild(1).GetComponent<Text>();
            t.text = "You gained 1 Strength Point!";
            fogInfo.SetActive(true);
        }
        else if (ft == FogType.TwoWP)
        {
            Text t = fogInfo.transform.GetChild(1).GetComponent<Text>();
            t.text = "You gained 2 Willpower!";
            fogInfo.SetActive(true);
        }
        else if (ft == FogType.ThreeWP)
        {
            Text t = fogInfo.transform.GetChild(1).GetComponent<Text>();
            t.text = "You gained 3 Willpower!";
            fogInfo.SetActive(true);
        }
        else if (ft == FogType.Gold)
        {
            Text t = fogInfo.transform.GetChild(1).GetComponent<Text>();
            t.text = "You gained 1 gold!";
            fogInfo.SetActive(true);
        }
        else if (ft == FogType.Wineskin)
        {
            Text t = fogInfo.transform.GetChild(1).GetComponent<Text>();
            t.text = "You found 1 wineskin!";
            fogInfo.SetActive(true);
        }


    }

    public void OnEndDay(Player player)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            Hero hero = player.GetHero();
            var r = player.GetCurrentRegion();
            List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(r);

            if (fogOnRegion.Count > 0)
            {
                showInfo(fogOnRegion[0].type);
                Uncover(r.label, (int)hero.type);
             


            }

        }

    }

    public void Uncover(int currentRegion, int heroType)
    {
        List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(GameGraph.Instance.Find(currentRegion));
        Fog curr = fogOnRegion[0];

        if (curr.type == FogType.SP)//SP+1
        {
            photonView.RPC("SP", RpcTarget.AllBuffered, currentRegion, heroType);

        }
        else if (curr.type == FogType.TwoWP)//WP+2
        {
            photonView.RPC("TwoWP", RpcTarget.AllBuffered, currentRegion, heroType);
        }
        else if (curr.type == FogType.ThreeWP)//WP+3
        {
            photonView.RPC("ThreeWP", RpcTarget.AllBuffered, currentRegion, heroType);
        }
        else if (curr.type == FogType.Gold)//Gold
        {
            photonView.RPC("Gold", RpcTarget.AllBuffered, currentRegion, heroType);
        }
        else if (curr.type == FogType.Event)//Event
        {
            // Matt's part
            if (PhotonNetwork.IsMasterClient)
            {
                int[] temp = narrator.GetComponent<Narrator>().temp;
                photonView.RPC("FogEventCard", RpcTarget.AllBuffered, currentRegion, temp);
            }

        }
        else if (curr.type == FogType.Wineskin)//Wineskin
        {
            photonView.RPC("Wineskin", RpcTarget.AllBuffered, currentRegion, heroType);
        }
        else if (curr.type == FogType.Witch)//Witch
        {
            photonView.RPC("Witch", RpcTarget.AllBuffered, currentRegion, heroType);
        }
        else if (curr.type == FogType.Monster)//Gor
        {
            photonView.RPC("Gor", RpcTarget.AllBuffered, currentRegion, heroType);
        }
    }

    // trigger the event card on narrrator
    [PunRPC]
    public void FogEventCard(int currentRegion, int[] temp)
    {
        List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(GameGraph.Instance.Find(currentRegion));
        Fog curr = fogOnRegion[0];
        curr.fogIcon.enabled = false;
        Destroy(curr);
        narrator.GetComponent<Narrator>().realaseNewEventCardFog(temp);
    }


    [PunRPC]
    public void SP(int currentRegion, int whichHero)
    {
        List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(GameGraph.Instance.Find(currentRegion));
        Fog curr = fogOnRegion[0];

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Hero hero = (Hero)players[i].GetHero();
            if ((int)hero.type == whichHero)
            {
                hero.data.SP += 1;
                break;
            }
        }
        //make sure fog is removed
        curr.fogIcon.enabled = false;
        Destroy(curr);

        //Testing
        /*for (int i = 0; i < players.Length; i++)
        {
            Hero h = (Hero)players[i].GetHero();
            Debug.Log(h.type + " " + h.data.SP + " " + h.data.WP + " " + h.data.gold);

        }*/
    }

    [PunRPC]
    public void TwoWP(int currentRegion, int whichHero)
    {
        List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(GameGraph.Instance.Find(currentRegion));
        Fog curr = fogOnRegion[0];

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Hero hero = (Hero)players[i].GetHero();
            if ((int)hero.type == whichHero)
            {
                hero.data.WP += 2;
                break;
            }
        }
        //make sure fog is removed
        curr.fogIcon.enabled = false;
        Destroy(curr);



    }

    [PunRPC]
    public void ThreeWP(int currentRegion, int whichHero)
    {
        List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(GameGraph.Instance.Find(currentRegion));
        Fog curr = fogOnRegion[0];

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Hero hero = (Hero)players[i].GetHero();
            if ((int)hero.type == whichHero)
            {
                hero.data.WP += 3;
                break;
            }
        }
        //make sure fog is removed
        curr.fogIcon.enabled = false;
        Destroy(curr);



    }

    [PunRPC]
    public void Gold(int currentRegion, int whichHero)
    {
        List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(GameGraph.Instance.Find(currentRegion));
        Fog curr = fogOnRegion[0];

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Hero hero = (Hero)players[i].GetHero();
            if ((int)hero.type == whichHero)
            {
                hero.data.gold += 1;
                break;
            }
        }
        //make sure fog is removed
        curr.fogIcon.enabled = false;
        Destroy(curr);


    }

    [PunRPC]
    public void Wineskin(int currentRegion, int whichHero)
    {
        List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(GameGraph.Instance.Find(currentRegion));
        Fog curr = fogOnRegion[0];

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Hero hero = (Hero)players[i].GetHero();
            if ((int)hero.type == whichHero)
            {
                hero.data.wineskin += 2;
                break;
            }
        }
        //make sure fog is removed
        curr.fogIcon.enabled = false;
        Destroy(curr);


    }



    [PunRPC]
    public void Witch(int currentRegion, int whichHero)
    {
        List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(GameGraph.Instance.Find(currentRegion));
        Fog curr = fogOnRegion[0];

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Hero hero = (Hero)players[i].GetHero();
            if ((int)hero.type == whichHero)
            {
                hero.data.brew += 2;
                break;
            }
        }

        GameObject w = Instantiate(witchPrefab, GameGraph.Instance.Find(currentRegion).position, Quaternion.identity);
        Witch myWitch = w.GetComponent<Witch>();
        myWitch.region = currentRegion;

        Text t = fogInfo.transform.GetChild(1).GetComponent<Text>();
        t.text = (Hero.Type)whichHero + " discovered the witch Reka and got her brew for free! You can now buy brew from Reka.";
        fogInfo.SetActive(true);
        fogInfo.transform.GetChild(2).gameObject.SetActive(false);

        herbDice.SetActive(true);
        herbDice.transform.GetChild(0).gameObject.SetActive(false);
        herbDice.transform.GetChild(2).gameObject.SetActive(false);

        if (PhotonNetwork.IsMasterClient)
        {
            herbDice.transform.GetChild(0).gameObject.SetActive(true);
            GameObject rollButton = herbDice.transform.GetChild(0).gameObject;
            rollButton.GetComponent<Button>().onClick.RemoveAllListeners();
            rollButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                rollDice();
            });

        }

        //make sure fog is removed
        curr.fogIcon.enabled = false;
        Destroy(curr);

        brewButton.gameObject.SetActive(true);
    }

    public void rollDice()
    {
        herbDice.transform.GetChild(0).gameObject.SetActive(false);
        System.Random rand = new System.Random();
        int rInt = rand.Next(1, 7);
        photonView.RPC("updateDice", RpcTarget.AllBuffered, rInt);
    }

    [PunRPC]
    public void updateDice(int dice)
    {
        Text t = herbDice.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>();
        t.text = dice.ToString();

        int herbAt = 37;
        if (dice == 3 || dice == 4)
        {
            herbAt = 67;
        }
        else if (dice == 5 || dice == 6)
        {
            herbAt = 61;
        }
      
        Text t2 = fogInfo.transform.GetChild(1).GetComponent<Text>();
        t2.text = "Medical herb is placed at Region " + herbAt + ".";

        fogInfo.transform.GetChild(2).gameObject.SetActive(true);
        herbDice.transform.GetChild(2).gameObject.SetActive(true);

        Region target = GameGraph.Instance.Find(herbAt);
        Instantiate(herbGorPrefab, target.position, Quaternion.identity);
       
    }

    [PunRPC]
    public void Gor(int currentRegion, int whichHero)
    {
        List<Fog> fogOnRegion = GameGraph.Instance.FindObjectsOnRegion<Fog>(GameGraph.Instance.Find(currentRegion));
        Fog curr = fogOnRegion[0];

        Instantiate(gorPrefab, curr.transform.position, curr.transform.rotation);

        Text t = fogInfo.transform.GetChild(1).GetComponent<Text>();
        t.text = "A Gor was hiding behind the fog...";
        fogInfo.SetActive(true);

        //make sure fog is removed
        curr.fogIcon.enabled = false;
        Destroy(curr);

    }

    [PunRPC]
    public void Typing(int[] whereTo)
    {
        int i;
        Fog curr;

        for (i = 0; i < 15; i++)
        {
            GameObject myFog = Instantiate(fogPrefab, GameGraph.Instance.Find(whereTo[i]).position, Quaternion.identity);
            curr = myFog.GetComponent<Fog>();
            curr.region = whereTo[i];

            if (i < 5)
            {
                curr.type = FogType.Event;
            }
            else if (i == 5)
            {
                curr.type = FogType.SP;
            }
            else if (i == 6)
            {
                curr.type = FogType.TwoWP;
            }
            else if (i == 7)
            {
                curr.type = FogType.ThreeWP;
            }
            else if (i < 11)
            {
                curr.type = FogType.Gold;
            }else if (i<13)
            {
                curr.type = FogType.Monster;
            }else if (i == 13)
            {
                curr.type = FogType.Wineskin;
            }
            else
            {
                curr.type = FogType.Witch;
            }
            
        }
              

    }


}
