using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to select a hero in GameLobby.
/// </summary> 
public class HeroSelection : MonoBehaviourPun, IPunObservable
{
    [Header("UI")]
    public Image image; //change image based on current hero

    private List<Hero> heroes;
    private int selectedHeroIndex = 0; 
    
    public Hero CurrentHero {
        get
        {
            return heroes[selectedHeroIndex];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        heroes = Hero.FindAllInResources();
        transform.position = SetUp.Instance.spawnPoints[photonView.Owner.ActorNumber - 1].position;
        Display();
    }


    // Update is called once per frame
    void Update()
    {
        Display();
        if (!PhotonNetwork.LocalPlayer.IsReady() && photonView.IsMine)
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
            selectedHeroIndex = i;
        }
    }
}
