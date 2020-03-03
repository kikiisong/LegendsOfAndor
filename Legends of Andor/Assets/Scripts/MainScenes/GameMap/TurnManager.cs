using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviourPun, TurnManager.IOnMove
{
    public static TurnManager Instance;

    //Listeners
    public List<IOnMove> onMoves = new List<IOnMove>();

    void Awake()
    {
        if(Instance != null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("TurnManager not singleton");
        }
    }

    private void Start()
    {
        Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void HeroMoved(Player player)
    {
        foreach(IOnMove onMove in onMoves)
        {
            onMove.OnMove(player);
        }
    }

    public void MoveRPC()
    {
        photonView.RPC("HeroMoved", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }


    public static void Register(IOnMove onMove)
    {
        Instance.onMoves.Add(onMove);
    }

    public void OnMove(Player player)
    {
        print("Turn");
    }

    public interface IOnMove
    {
        void OnMove(Player player);
    }
}
