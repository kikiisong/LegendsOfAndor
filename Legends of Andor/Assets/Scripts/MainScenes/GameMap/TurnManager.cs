using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviourPun, TurnManager.IOnMove, TurnManager.IOnTurnCompleted
{
    public static TurnManager Instance;

    //Listeners
    public List<IOnMove> onMoves = new List<IOnMove>();

    void Awake()
    {
        if(Instance == null)
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
        Register(onMove: this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void HeroMoved(Player player, int currentRegion)
    {
        foreach(IOnMove onMove in onMoves)
        {
            onMove.OnMove(player, GameGraph.Instance.Find(currentRegion));
        }
    }

    public void MoveRPC(Region currentRegion)
    {
        photonView.RPC("HeroMoved", RpcTarget.All, PhotonNetwork.LocalPlayer, currentRegion.label);
    }


    public static void Register(IOnMove onMove)
    {
        Instance.onMoves.Add(onMove);
    }

    public static void Register(IOnTurnCompleted onTurnCompleted)
    {
        //Instance.
    }

    public void OnMove(Player player, Region currentRegion)
    {
        print("Move " + player.NickName);
    }

    public void OnTurnCompleted()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// Hero moves once
    /// </summary>
    public interface IOnMove
    {
        void OnMove(Player player, Region currentRegion);
    }

    public interface IOnTurnCompleted
    {
        void OnTurnCompleted();
    }
}
