using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;


public class FightManager : MonoBehaviour
{
    public GameObject archerPrefabs;
    public GameObject warriorPrefabs;
    public GameObject dwarfPrefabs;
    public GameObject wizardPrefabs;


    public Transform[] transforms = new Transform[4];


    public void Start()
    {
       
        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (player.CustomProperties.ContainsKey(K.Player.isFight))
            {
                Hero hero = (Hero)player.CustomProperties[K.Player.hero];

                switch (hero.type)
                {
                    case Hero.Type.ARCHER:
                        GameObject go1 = PhotonNetwork.Instantiate(archerPrefabs);
                        go1.transform.position = transforms[1].position;
                        go1.SetActive(true);
                        break;
                    case Hero.Type.WARRIOR:
                        GameObject go2 = PhotonNetwork.Instantiate(warriorPrefabs);
                        go2.transform.position = transforms[0].position;
                        go2.SetActive(true);
                        break;
                    case Hero.Type.DWARF:
                        GameObject go3 = PhotonNetwork.Instantiate(dwarfPrefabs);
                        go3.transform.position = transforms[2].position;
                        go3.SetActive(true);
                        break;
                    case Hero.Type.WIZARD:
                        GameObject go4 = PhotonNetwork.Instantiate(wizardPrefabs);
                        go4.transform.position = transforms[3].position;
                        go4.SetActive(true);
                        break;
                }
            }
        }
      
    }

}
