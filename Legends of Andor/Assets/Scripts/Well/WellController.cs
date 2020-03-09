using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class WellController : MonoBehaviourPun, TurnManager.IOnMove
{
    public GameObject drinkButton;
    public GameGraph gameGraph;

    // Start is called before the first frame update
    void Start()
    {
         TurnManager.Register(this);
         drinkButton = GameObject.Find("drinkButton");
         drinkButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMove(Player player, Region currentRegion)
    {
        if (PhotonNetwork.LocalPlayer == player)
        {
            Hero hero = (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];//photonView.Owner is the Scene

            List<Well> wellOnRegion = gameGraph.FindObjectsOnRegion<Well>(currentRegion);

            if(wellOnRegion.Count > 0)
            {

                if(wellOnRegion[0].isFilled)
                {
                    drinkButton.SetActive(true);
                    drinkButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    drinkButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        hero.data.WP += 3;

                        photonView.RPC("Empty", RpcTarget.AllBuffered, currentRegion.label);

                        drinkButton.SetActive(false);
                    });
                }

            }
        }

    }

    [PunRPC]
    public void Empty(int currentRegion)
    {
        List<Well> wellOnRegion = gameGraph.FindObjectsOnRegion <Well>(GameGraph.Instance.Find(currentRegion));

        Well temp = wellOnRegion[0];

        temp.drunk();
    }

}
