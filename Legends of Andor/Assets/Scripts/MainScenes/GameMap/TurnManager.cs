using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviourPun
{
    public static TurnManager Instance;

    private List<Player> players = new List<Player>();
    private int turnIndex = 0;

    //Listeners
    List<IOnMove> onMoves = new List<IOnMove>();
    List<IOnTurnCompleted> onTurnCompleteds = new List<IOnTurnCompleted>();

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

    private void Test()
    {
        Register(onMove: new TestTurn());
        Register(onTurnCompleted: new TestTurn());
    }

    private void Start()
    {
        Test();
        foreach(Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            players.Add(player);
        }

        players.Sort((p1, p2) =>
        {
            Hero h1 = (Hero)p1.CustomProperties[K.Player.hero];
            Hero h2 = (Hero)p2.CustomProperties[K.Player.hero];
            return h1.constants.rank - h2.constants.rank;
        });
    }

    private void Update()
    {
        //better way?
        if (IsMyTurn() && Input.GetKey(KeyCode.Return))
        {
            TriggerEvent_EndTurn(photonView.Owner);
        }
    }

    //Turn
    public static bool IsMyTurn()
    {
        return Instance.players[Instance.turnIndex] == PhotonNetwork.LocalPlayer;
    }

    [PunRPC]
    public void NextTurn(Player player)
    {
        foreach(IOnTurnCompleted onTurnCompleted in onTurnCompleteds)
        {
            onTurnCompleted.OnTurnCompleted(player);
        }
        turnIndex = Helper.Mod(turnIndex + 1, players.Count);
    }

    public void TriggerEvent_EndTurn(Player player)
    {
        Instance.photonView.RPC("NextTurn", RpcTarget.All, player);
    }


    //Move
    [PunRPC]
    public void HeroMoved(Player player, int currentRegion)
    {
        foreach(IOnMove onMove in onMoves)
        {
            onMove.OnMove(player, GameGraph.Instance.Find(currentRegion));
        }
    }

    public static void TriggerEvent_Move(Region currentRegion)
    {
        Instance.photonView.RPC("HeroMoved", RpcTarget.All, PhotonNetwork.LocalPlayer, currentRegion.label);
    }

    //Register
    public static void Register(IOnMove onMove)
    {
        Instance.onMoves.Add(onMove);
    }

    public static void Register(IOnTurnCompleted onTurnCompleted)
    {
        Instance.onTurnCompleteds.Add(onTurnCompleted);
    }


    //Interfaces
    public interface IOnMove
    {
        void OnMove(Player player, Region currentRegion);
    }

    public interface IOnTurnCompleted
    {
        void OnTurnCompleted(Player player);
    }
}

public class TestTurn: TurnManager.IOnMove, TurnManager.IOnTurnCompleted
{
    public void OnMove(Player player, Region currentRegion)
    {
        Debug.Log("Move " + player.NickName);
    }

    public void OnTurnCompleted(Player player)
    {
        Debug.Log("Turn completed " + PhotonNetwork.LocalPlayer.NickName);
    }
}
