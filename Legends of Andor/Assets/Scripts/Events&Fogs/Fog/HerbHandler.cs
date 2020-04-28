using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;


public class HerbHandler : MonoBehaviourPun, TurnManager.IOnMove
{
    public Button dropButton, pickButton;
    public GameObject myHerb;
    public GameObject herbPrefab;

    // Start is called before the first frame update
    void Start()
    {
        dropButton.gameObject.SetActive(false);
        pickButton.gameObject.SetActive(false);
        //myHerb.GetComponent<Herb>().found = false;
        TurnManager.Register(this);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMove(Player player, Region currentRegion)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();//photonView.Owner is the Scene

            List<Herb> herbOnRegion = GameGraph.Instance.FindObjectsOnRegion<Herb>(currentRegion);

            if (herbOnRegion.Count > 0)
            {


                pickButton.gameObject.SetActive(true);
               


            }
            else if (hero.data.herb > 0)
            {
                dropButton.gameObject.SetActive(true);
                
            }
            else
            {
                dropButton.gameObject.SetActive(false);
                pickButton.gameObject.SetActive(false);
            }
        }

    }

    public void pickUpHerb()
    {
        Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();//photonView.Owner is the Scene
        photonView.RPC("HerbPicked", RpcTarget.AllBuffered, (int)hero.type);

        pickButton.gameObject.SetActive(false);
        dropButton.gameObject.SetActive(true);
    }

    public void dropHerb()
    {
        Hero hero = (Hero)PhotonNetwork.LocalPlayer.GetHero();//photonView.Owner is the Scene
        var r = PhotonNetwork.LocalPlayer.GetCurrentRegion();
        photonView.RPC("HerbDropped", RpcTarget.AllBuffered, r.label, (int)hero.type);

        pickButton.gameObject.SetActive(true);
        dropButton.gameObject.SetActive(false);
    }

    [PunRPC]
    public void HerbPicked(int whichHero)
    {

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Hero hero = (Hero)players[i].GetHero();
            if ((int)hero.type == whichHero)
            {
                //Debug.Log(hero.data.herb);
                hero.data.herb += 1;
                var r = hero.GetCurrentRegion();
                r.data.herb -= 1;
                break;
            }
        }

        Destroy(myHerb);
    }

    [PunRPC]
    public void HerbDropped(int currentRegion, int whichHero)
    {

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Hero hero = (Hero)players[i].GetHero();
            if ((int)hero.type == whichHero)
            {
                //Debug.Log(hero.data.herb);
                hero.data.herb -= 1;
                var r = hero.GetCurrentRegion();
                r.data.herb += 1;
                break;
            }
        }

        
        Region target = GameGraph.Instance.Find(currentRegion);
        myHerb = Instantiate(herbPrefab, target.position, Quaternion.identity);

    }
    /*private HerbHandler herbManager;
     Region target = GameGraph.Instance.Find(herbAt);
    GameObject herb = Instantiate(herbGorPrefab, target.position, Quaternion.identity);
    herbManager = GameObject.FindGameObjectWithTag("manager").GetComponent<HerbHandler>();
        herbManager.myHerb = herb;*/
}
