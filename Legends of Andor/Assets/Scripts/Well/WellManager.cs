using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class WellManager : MonoBehaviourPun, TurnManager.IOnMove
{
    public Button drinkButton;

    // Start is called before the first frame update
    void Start()
    {
         drinkButton.gameObject.SetActive(false);
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

            List<Well> wellOnRegion = GameGraph.Instance.FindObjectsOnRegion<Well>(currentRegion);

            if(wellOnRegion.Count > 0)
            {

                if(wellOnRegion[0].IsFilled)
                {
                    drinkButton.gameObject.SetActive(true);
                    drinkButton.GetComponent<Button>().onClick.RemoveAllListeners();
                    drinkButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        //Debug.Log(hero.data.WP);
                        hero.data.WP += 3;
                        

                        photonView.RPC("Empty", RpcTarget.AllBuffered, currentRegion.label);

                        drinkButton.gameObject.SetActive(false);
                    });
                }

            }
            else
            {
                drinkButton.gameObject.SetActive(false);
            }
        }

    }

    [PunRPC]
    public void Empty(int currentRegion)
    {
        List<Well> wellOnRegion = GameGraph.Instance.FindObjectsOnRegion <Well>(GameGraph.Instance.Find(currentRegion));

        Well temp = wellOnRegion[0];

        temp.Drunk();
    }

}
