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
    public static TurnHelper helper = new TurnHelper();

    static Hero CurrentHero
    {
        get
        {
            return PhotonNetwork.LocalPlayer.GetHero();
        }
    }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.LogWarning("TurnManager not singleton");

        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            players.Add(player);
        }

        players.Sort((p1, p2) =>
        {
            Hero h1 = p1.GetHero();
            Hero h2 = p2.GetHero();
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
       
        GameObject movePrinceButton = GameObject.Find("Actions").transform.Find("MovePrince").gameObject;
        movePrinceButton.SetActive(Prince.Instance != null && IsMyTurn()&&CanMove());

        endTurn.gameObject.SetActive(CanMove());
        endDay.gameObject.SetActive(IsMyTurn());
    }

    //works?
    public int GetWaitIndex(Player player)
    {
        List<Player> inSunriseBox = new List<Player>();
        foreach (Player p in players)
        {
            Hero hero = p.GetHero();
            if (hero.data.NumHours == 0) inSunriseBox.Add(p);
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
        if(CurrentHero.data.NumHoursEffective >= 7)
        {
            return IsMyTurn() && CurrentHero.data.WP >= 2;
        }
        return IsMyTurn() && CurrentHero.data.NumHoursEffective < 10;
    }

    public static bool IsMyTurn()
    {
        if (Instance.players.Count == 0) return false;
        return Instance.players[Instance.turnIndex] == PhotonNetwork.LocalPlayer;
    }

    [PunRPC]
    public void NextTurn(Player player)
    {
        turnIndex = Helper.Mod(turnIndex + 1, players.Count);
        var hero = player.GetHero();

        //Reset move prince, and consume one more hour if needed
        HeroMoveController[] controllers = GameObject.FindObjectsOfType<HeroMoveController>();
        foreach (HeroMoveController controller in controllers)
        {
            if (controller.photonView.IsMine)
            {
                if (controller.PrinceMoveCounter > 0) hero.data.ConsumeHour();
                controller.IsControllingPrince = false;
            }
        }

        //Consume hour if Pass
        if (hero.data.HoursConsumed == 0)
        {
            hero.data.ConsumeHour();
        }
        hero.data.ResetHoursConsumed();

        //Notify
        foreach (IOnTurnCompleted onTurnCompleted in onTurnCompleteds)
        {
            onTurnCompleted.OnTurnCompleted(player);
        }
    }

    public static void TriggerEvent_EndTurn()
    {
        Instance.photonView.RPC("NextTurn", RpcTarget.All, PhotonNetwork.LocalPlayer);        
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

        player.GetHero().data.ResetNumHours();

        //Notify
        foreach (IOnEndDay onEndDay in onEndDays)
        {
            onEndDay.OnEndDay(player);
        }

        player.GetHero().data.usedFalcon = false;
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
        Hero hero = player.GetHero();

        if(hero.data.NumHours == hero.data.NumHoursEffective)
        {
            hero.data.ConsumeHour();

            if (hero.data.NumHours > 7)
            {
                hero.data.WP -= 2;
            }
        }
        else
        {
            hero.data.wineskinStacked--;
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
