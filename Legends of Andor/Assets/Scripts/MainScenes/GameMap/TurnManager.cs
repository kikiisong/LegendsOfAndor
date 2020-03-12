using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviourPun
{
    public static TurnManager Instance;

    private List<Player> players = new List<Player>();
    private List<Player> waiting = new List<Player>();
    private int turnIndex = 0;

    //Listeners
    List<IOnMove> onMoves = new List<IOnMove>();
    List<IOnTurnCompleted> onTurnCompleteds = new List<IOnTurnCompleted>();
    List<IOnEndDay> onEndDays = new List<IOnEndDay>();
    List<IOnSunrise> onSunrises = new List<IOnSunrise>();

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("TurnManager not singleton");

        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            players.Add(player);
        }

        players.Sort((p1, p2) =>
        {
            Hero h1 = (Hero)p1.CustomProperties[K.Player.hero];
            Hero h2 = (Hero)p2.CustomProperties[K.Player.hero];
            return h2.constants.rank - h1.constants.rank;
        });
    }

    private void Start()
    {
        Register(new TestTurn());
    }

    private void Update()
    {
        //better way?
        if (IsMyTurn() && Input.GetKey(KeyCode.Return))
        {
            TriggerEvent_EndTurn(PhotonNetwork.LocalPlayer);
        }
    }

    //works?
    public int GetWaitIndex(Player player)
    {
        List<Player> inSunriseBox = new List<Player>();
        foreach(Player p in players)
        {
            Hero hero = (Hero)p.CustomProperties[K.Player.hero];
            if (hero.data.numHours == 0) inSunriseBox.Add(p);
        }
        foreach(Player p in waiting)
        {
            inSunriseBox.Add(p);
        }
        return inSunriseBox.IndexOf(player);
    }

    //Turn
    public static bool IsMyTurn()
    {
        if (Instance.players.Count == 0) return false;
        return Instance.players[Instance.turnIndex] == PhotonNetwork.LocalPlayer;
    }

    [PunRPC]
    public void NextTurn(Player player)
    {
        turnIndex = Helper.Mod(turnIndex + 1, players.Count);

        //Notify
        foreach(IOnTurnCompleted onTurnCompleted in onTurnCompleteds)
        {
            onTurnCompleted.OnTurnCompleted(player);
        }       
    }

    public static void TriggerEvent_EndTurn(Player player)
    {
        Instance.photonView.RPC("NextTurn", RpcTarget.All, player);
    }

    //Day
    [PunRPC]
    public void EndDay(Player player)
    {
        //Reset index, better solution?
        int i = players.IndexOf(player);
        Player next = players[Helper.Mod(i + 1, players.Count)];

        players.Remove(player);
        turnIndex = players.IndexOf(next);
        waiting.Add(player);

        //Notify
        foreach (IOnEndDay onEndDay in onEndDays)
        {
            onEndDay.OnEndDay(player);
        }
    }

    public static void TriggerEvent_EndDay(Player player)
    {
        Instance.photonView.RPC("EndDay", RpcTarget.All, player);
        if(Instance.players.Count == 0)
        {
            TriggerEvent_Sunrise();
        }
    }

    //Sunrise
    [PunRPC]
    public void Sunrise()
    {
        //Reset
        turnIndex = 0;
        foreach (Player player in waiting)
        {
            players.Add(player);
        }
        waiting.Clear();

        //Notify
        foreach (IOnSunrise onSunrise in onSunrises)
        {
            onSunrise.OnSunrise();
        }
    }

    public static void TriggerEvent_Sunrise()
    {
        Instance.photonView.RPC("Sunrise", RpcTarget.All);
    }


    //Move
    [PunRPC]
    public void HeroMoved(Player player, int currentRegion)
    {
        Hero hero = (Hero)player.CustomProperties[K.Player.hero];
        hero.data.numHours++;

        //Notify
        foreach (IOnMove onMove in onMoves)
        {
            onMove.OnMove(player, GameGraph.Instance.Find(currentRegion));
        }

        if (hero.data.numHours == 7)
        {
            //or buy more hours, do it in OnEndTurn instead
            TriggerEvent_EndTurn(player);
        }
    }

    public static void TriggerEvent_Move(Region currentRegion)
    {
        Instance.photonView.RPC("HeroMoved", RpcTarget.All, PhotonNetwork.LocalPlayer, currentRegion.label);
    }

    //Register
    static void AddNotNull<I>(I i, List<I> list)
    {
        if (i != null) list.Add(i);
    }

    public interface IEvent { }
    public static void Register(IEvent e)
    {
        AddNotNull(e as IOnMove, Instance.onMoves);
        AddNotNull(e as IOnTurnCompleted, Instance.onTurnCompleteds);
        AddNotNull(e as IOnEndDay, Instance.onEndDays);
        AddNotNull(e as IOnSunrise, Instance.onSunrises);
    }

    //Interfaces
    public interface IOnMove : IEvent
    {
        void OnMove(Player player, Region currentRegion);
    }

    public interface IOnTurnCompleted : IEvent
    {
        void OnTurnCompleted(Player player);
    }

    public interface IOnEndDay : IEvent
    {
        void OnEndDay(Player player);
    }

    public interface IOnSunrise : IEvent
    {
        void OnSunrise();
    }
}

public class TestTurnFIGHT: TurnManager.IOnMove, TurnManager.IOnTurnCompleted, TurnManager.IOnEndDay, TurnManager.IOnSunrise
{
    public void OnMove(Player player, Region currentRegion)
    {
        Debug.Log("Move " + player.NickName);
    }

    public void OnTurnCompleted(Player player)
    {
        Debug.Log("Turn completed " + player.NickName);
    }

    public void OnEndDay(Player player)
    {
        Debug.Log("End day " + player.NickName);
    }

    public void OnSunrise()
    {
        Debug.Log("Sunrise");
    }
}
