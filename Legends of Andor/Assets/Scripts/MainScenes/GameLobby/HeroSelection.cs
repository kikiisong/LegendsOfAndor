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
    [ArrayElementTitle("type")]
    public List<HeroUIData> heroes;

    [Header("UI")]
    public Image image; //change image based on current hero

    private int selectedHeroIndex; 
    
    public HeroUIData CurrentHero {
        get
        {
            return heroes[selectedHeroIndex];
        }
    }

    //Careful with null
    public static List<HeroUIData> Heroes;

    // Start is called before the first frame update
    void Start()
    {
        foreach(KeyValuePair<int, Player> pair in PhotonNetwork.CurrentRoom.Players)
        {
            if (pair.Value.Equals(photonView.Owner)){
                transform.position = SetUp.Instance.spawnPoints[pair.Key - 1].position;
            }
        }
        transform.SetParent(GameObject.Find("CurrentRoomCanvas").transform);
        Display();
        Heroes = heroes; //Not single instance since multiple CharacterSelection 
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
                CurrentHero.ToggleGender();
                Display();
            }
        }
    }


    public void Display()
    {
        image.sprite = heroes[selectedHeroIndex].GetSprite();
    }

    public void Switch()
    {
        heroes[selectedHeroIndex].ToggleGender();
        Display();
    }

    private void Next()
    {
        selectedHeroIndex = (selectedHeroIndex + 1) % heroes.Count;
        Display();
    }

    private void Previous()
    {
        selectedHeroIndex = Helper.mod(selectedHeroIndex - 1, heroes.Count);
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


[System.Serializable]
public class HeroUIData
{
    public HeroType type;
    private bool gender; // how to implement gender?
    
    public Sprite female;
    public Sprite male;

    public Sprite GetSprite()
    {
        return gender ? male : female;
    }

    public void ToggleGender()
    {
        gender = !gender;
    }

}


public enum HeroType
{
    ARCHER, WARRIOR, WIZARD, DWARF
}
