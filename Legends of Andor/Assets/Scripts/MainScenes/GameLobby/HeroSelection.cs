using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Used to select a hero in GameLobby.
/// </summary>
public class HeroSelection : MonoBehaviourPun, IPunObservable
{
    [Header("List of heroes")]
    public List<Hero> heroes;

    [Header("UI")]
    public Image image; //change image based on current hero

    private int selectedHeroIndex; 
    
    public Hero CurrentHero {
        get
        {
            return heroes[selectedHeroIndex];
        }
    }

    public static List<Hero> Heroes = new List<Hero>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(KeyValuePair<int, Player> pair in PhotonNetwork.CurrentRoom.Players)
        {
            if (pair.Value.Equals(photonView.Owner)){
                transform.position = SetUp.Instance.spawnPoints[pair.Key - 1].position;
            }
        }
        Display();
        if (photonView.IsMine)
        {
            foreach(Hero hero in heroes)
            {
                Heroes.Add(hero);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        bool amReady = false;
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey(K.Player.isReady))
        {
            amReady = (bool)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.isReady];
        }
        if (!amReady && photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Next();
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Previous();
            }else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                photonView.RPC("ToggleGender", RpcTarget.AllBuffered);
            }
        }
    }

    [PunRPC]
    public void ToggleGender()
    {
        CurrentHero.ui.ToggleGender();
        Display();
    }


    public void Display()
    {
        image.sprite = heroes[selectedHeroIndex].ui.GetSprite();
    }

    public void Switch()
    {
        heroes[selectedHeroIndex].ui.ToggleGender();
        Display();
    }

    private void Next()
    {
        selectedHeroIndex = (selectedHeroIndex + 1) % heroes.Count;
        Display();
    }

    private void Previous()
    {
        selectedHeroIndex = Helper.Mod(selectedHeroIndex - 1, heroes.Count);
        Display();
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(selectedHeroIndex);
        }
        else
        {
            int i = (int) stream.ReceiveNext();
            if(i != selectedHeroIndex) //don't display if already correct
            {
                selectedHeroIndex = i;
                Display();
            }
        }
    }
}
