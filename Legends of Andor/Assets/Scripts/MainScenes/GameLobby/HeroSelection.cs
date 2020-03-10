using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
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

    private int selectedHeroIndex = 0; 
    
    public Hero CurrentHero {
        get
        {
            return Heroes[selectedHeroIndex];
        }
    }

    public static List<Hero> Heroes = new List<Hero>();

    void Awake()
    {
        if (photonView.IsMine)
        {
            foreach (Hero hero in heroes)
            {
                Heroes.Add(Instantiate(hero));
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = SetUp.Instance.spawnPoints[photonView.Owner.ActorNumber - 1].position;
        Display();
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
        print(photonView.Owner.NickName + " " + selectedHeroIndex + "," + Heroes.Count);
        image.sprite = heroes[selectedHeroIndex].ui.GetSprite();
    }

    public void Switch()
    {
        Heroes[selectedHeroIndex].ui.ToggleGender();
        Display();
    }

    private void Next()
    {
        selectedHeroIndex = (selectedHeroIndex + 1) % Heroes.Count;
        Display();
    }

    private void Previous()
    {
        selectedHeroIndex = Helper.Mod(selectedHeroIndex - 1, Heroes.Count);
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
            selectedHeroIndex = i;
            Display();
        }
    }
}
