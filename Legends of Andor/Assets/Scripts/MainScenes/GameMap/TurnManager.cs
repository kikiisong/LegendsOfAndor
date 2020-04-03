using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviourPun
{
    public static TurnManager Instance;

    public Button endTurn;
    public Button endDay;

    //Turn
    private List<Player> players = new List<Player>();
    private List<Player> waiting = new List<Player>();
    private int turnIndex = 0;

    //Listeners
    List<IOnMove> onMoves = new List<IOnMove>();
    List<IOnTurnCompleted> onTurnCompleteds = new List<IOnTurnCompleted>();
    List<IOnEndDay> onEndDays = new List<IOnEndDay>();
    List<IOnSunrise> onSunrises = new List<IOnSunrise>();

    //Helper
    static TurnHelper helper = new TurnHelper();

    static Hero CurrentHero
    {
        get
        {
            return (Hero)PhotonNetwork.LocalPlayer.CustomProperties[K.Player.hero];
        }
    }

    void Awake()
    {

        if (Instance == null) Instance = this;
        else Debug.LogWarning("TurnManager not singleton");

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
        Register(new TestTurn());
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
            return h2.constants.rank - h1.constants.rank;
        });
    }


    private void Start()
    {
        Register(helper);
        endTurn.onClick.AddListener(() => TriggerEvent_EndTurn());
        endDay.onClick.AddListener(() => TriggerEvent_EndDay());
    }


    private void Update()
    {
        endTurn.gameObject.SetActive(CanMove());
        endDay.gameObject.SetActive(IsMyTurn());
    }


    //works?
    public int GetWaitIndex(Player player)
    {
        List<Player> inSunriseBox = new List<Player>();
        foreach (Player p in players)
        {
            Hero hero = (Hero)p.CustomProperties[K.Player.hero];
            if (hero.data.numHours == 0) inSunriseBox.Add(p);
        }
        foreach (Player p in waiting)
        {
            inSunriseBox.Add(p);
        }
        return inSunriseBox.IndexOf(player);
    }


    //Turn
    public static bool CanMove()
    {
        if(CurrentHero.data.numHours >= 7)
        {
            return IsMyTurn() && CurrentHero.data.WP >= 2;
        }
        return IsMyTurn() && CurrentHero.data.numHours < 10;
    }

    public static bool IsMyTurn()
    {
        if (Instance.players.Count == 0) return false;
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

    public static void TriggerEvent_EndTurn()
    {
        Instance.photonView.RPC("NextTurn", RpcTarget.All, PhotonNetwork.LocalPlayer);

        if (!helper.hasMoved)
        {
            TriggerEvent_Move(HeroMoveController.CurrentRegion());
        }
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

        Hero hero = (Hero)player.CustomProperties[K.Player.hero];
        hero.data.numHours = 0;

        //Notify
        foreach (IOnEndDay onEndDay in onEndDays)
        foreach(IOnEndDay onEndDay in onEndDays)
        {
            onEndDay.OnEndDay(player);
        }

        players.Remove(player);
        waiting.Add(player);
    }

    public static void TriggerEvent_EndDay()
    {
        Instance.photonView.RPC("EndDay", RpcTarget.All, PhotonNetwork.LocalPlayer);
        if(Instance.players.Count == 0)
        {
            TriggerEvent_Sunrise();
        }
    }

    //Sunrise
    [PunRPC]
    public void Sunrise()
    {
        foreach(IOnSunrise onSunrise in onSunrises)
        {
            onSunrise.OnSunrise();
        }

        //Reset
        turnIndex = 0;
        foreach(Player player in waiting)
        {
            players.Add(player);
        }
        waiting.Clear();
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

        if(hero.data.numHours > 7)
        foreach(IOnMove onMove in onMoves)
     {
            hero.data.WP -= 2;
        }


        //Notify
        foreach (IOnMove onMove in onMoves)
        {
            onMove.OnMove(player, GameGraph.Instance.Find(currentRegion));
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

public class TurnHelper: TurnManager.IOnMove, TurnManager.IOnTurnCompleted, TurnManager.IOnEndDay, TurnManager.IOnSunrise
{
    public bool hasMoved = false;

    public void OnMove(Player player, Region currentRegion)
    {
        hasMoved = true;
        Debug.Log("Move " + player.NickName);
    }

    public void OnTurnCompleted(Player player)
    {
        hasMoved = false;
        Debug.Log("Turn completed " + player.NickName);
    }

    public void OnEndDay(Player player)
    {
        hasMoved = false;
        Debug.Log("End day " + player.NickName);
    }

    public void OnSunrise()
    {
        Debug.Log("Sunrise");
    }
}
