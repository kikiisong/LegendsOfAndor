﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightTurnManager : MonoBehaviourPun
{
    public static FightTurnManager Instance;

    public List<Player> players = new List<Player>();
    private List<Player> waiting = new List<Player>();
    private List<Player> finished = new List<Player>();
    private int turnIndex = 0;

    //Listeners
    List<IOnSkillCompleted> onSkillCompleteds = new List<IOnSkillCompleted>();
    List<IOnMonsterTurn> onMonsterTurns = new List<IOnMonsterTurn>();
    List<IOnShield> onShields = new List<IOnShield>();
    List<IOnSunrise> onSunrises = new List<IOnSunrise>();
    List<IOnLeave> onLeaves = new List<IOnLeave>();


    public static Hero CurrentHero
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

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.CustomProperties.ContainsKey(P.K.isFight))
            {
                print("In fight" + player.NickName);
                players.Add(player);

                //if (((Hero)player.GetHero()).type == Hero.Type.WIZARD) {

                //    wizardJoined = true;
                //}

            }
        }

    }
    
    private void Start()
    {
        Register(new TestFightTurn());
    }

    //Turn
    public static bool CanFight()
    {
        if (CurrentHero.data.NumHours >= 7)
        {
            return IsMyTurn() && CurrentHero.data.WP >= 2;
        }
        return IsMyTurn() && CurrentHero.data.NumHours < 10;
    }

    public static bool enoughTime()
    {
        if (CurrentHero.data.NumHours >= 7)
        {
            return  CurrentHero.data.WP >= 2;
        }
        return  CurrentHero.data.NumHours < 10;
    }


    //Turn
    public static bool IsMyTurn()
    {
        if (Instance.players.Count == 0)
        {
            print("??");
            return false;
        }
        
        return Instance.players[Instance.turnIndex] == PhotonNetwork.LocalPlayer;
    }

    public static bool IsMyProtectedTurn()
    {
        if (Instance.waiting.Count == 0) return false;
        return Instance.waiting[Instance.turnIndex] == PhotonNetwork.LocalPlayer;
    }

    [PunRPC]
    public void removeFighter(Player player) {
        players.Remove(player);
        if (players.Count == 0) {
            //no fighter in side the fight
            //the monster is not yet dead
            //we should restore the mosnter
            foreach (IOnLeave onLeave in onLeaves)
            {
                onLeave.OnLastLeave();
            }
        }
    }


    public static void TriggerRemove(Player player) {
        Instance.photonView.RPC("removeFighter", RpcTarget.All,player);
    }

    public int sizeOfPlayer() {
        return players.Count;
    }

    //Day
    [PunRPC]
    public void EndFightRound(Player player)
    {
        //Reset index, better solution?
        int diceNum = player.GetHero().data.diceNum;
        int i = players.IndexOf(player);
        Player next = players[Helper.Mod(i + 1, players.Count)];

        players.Remove(player);
        turnIndex = players.IndexOf(next);
        waiting.Add(player);
        print("Next Player" + next.NickName);
        //Notify
        foreach (IOnSkillCompleted onSkillCompleted in onSkillCompleteds)
        {
            onSkillCompleted.OnSkillCompleted(player, diceNum);
        }
    }

    public static void TriggerEvent_NewFightRound(Player player)
    { 
        Instance.photonView.RPC("EndFightRound", RpcTarget.All, player);
        if (Instance.players.Count == 0)
        {
            TriggerEvent_EndFightRound();
        }
    }

    //Sunrise
    [PunRPC]
    public void MonsterTurn()
    {
        //Notify
        turnIndex = 0;
        foreach (IOnMonsterTurn onMonsterTurn in onMonsterTurns)
        {
            onMonsterTurn.OnMonsterTurn();
        }
    }

    public static void TriggerEvent_EndFightRound()
    {
        Instance.photonView.RPC("MonsterTurn", RpcTarget.All);
    }


    //Move
    [PunRPC]
    public void HeroFight(Player player)
    {
        Hero hero = player.GetHero();
        hero.data.ConsumeHour();

        if (hero.data.NumHours > 7)
        {
            hero.data.WP -= 2;
        }
    }

    public static void TriggerEvent_Fight()
    {
        Instance.photonView.RPC("HeroFight", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }


    //Sunrise
    [PunRPC]
    public void OnShield(Player player)
    {
        //Notifyd
        //all move to finished
        int i = waiting.IndexOf(player);
        Player next = waiting[Helper.Mod(i + 1, waiting.Count)];

        waiting.Remove(player);
        turnIndex = waiting.IndexOf(next);
        finished.Add(player);
        print("Next Player" + next.NickName);
        //Notify
        foreach (IOnShield onShield in onShields)
        {
            onShield.OnShield(player);
        }
    }

    public static void TriggerEvent_OnShield(Player player)
    {
        Instance.photonView.RPC("OnShield", RpcTarget.All,player);
        if (Instance.waiting.Count == 0)
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
        foreach (Player player in finished)
        {
            players.Add(player);
        }
        finished.Clear();

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

    //Register
    static void AddNotNull<I>(I i, List<I> list)
    {
        if (i != null) list.Add(i);
    }

    public interface IEvent { }
    public static void Register(IEvent e)
    {
        AddNotNull(e as IOnSkillCompleted, Instance.onSkillCompleteds);
        AddNotNull(e as IOnMonsterTurn, Instance.onMonsterTurns);
        AddNotNull(e as IOnShield, Instance.onShields);
        AddNotNull(e as IOnSunrise, Instance.onSunrises);
        AddNotNull(e as IOnLeave, Instance.onLeaves);
    }
    

    //Interfaces

    public interface IOnSkillCompleted : IEvent
    {
        void OnSkillCompleted(Player player, int diceNum);
    }

    public interface IOnMonsterTurn : IEvent
    {
        void OnMonsterTurn();
    }

    public interface IOnShield : IEvent {
        void OnShield(Player player);
    }

    public interface IOnSunrise : IEvent
    {
        void OnSunrise();
    }
    public interface IOnLeave : IEvent {
        void OnLastLeave();
    }

}

public class TestFightTurn :  FightTurnManager.IOnSkillCompleted,
FightTurnManager.IOnMonsterTurn, FightTurnManager.IOnShield,
    FightTurnManager.IOnSunrise
{


    public void OnSkillCompleted(Player player, int diceNum)
    {
        Debug.Log("Skill completed " + player.NickName + " "+ diceNum);
    }

    public void OnMonsterTurn()
    {
        Debug.Log("Monster Rolling");
    }

    public void OnShield(Player player) {
        Debug.Log("Applied Shield" + player.NickName);
    }

    public void OnSunrise()
    {
        Debug.Log("Sunrise");
    }
}