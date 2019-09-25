using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private PlayerListing _playerListing;

    [SerializeField]
    private Text _readyUpNext; 

    private List<PlayerListing> _listings = new List<PlayerListing>();
    private RoomsCanvases _roomCanvases;
    private bool _ready = false;

    public override void OnEnable()
    {
        base.OnEnable();
        SetReadyUp(false);
        GetCurrentRoomPlayers(); 
    }

    public override void OnDisable()
    {
        base.OnDisable();
        for(int i = 0; i < _listings.Count; i++)
        {
            Destroy(_listings[i].gameObject);
        }
        _listings.Clear();
    }

    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomCanvases = canvases;
    }

    private void SetReadyUp(bool state)
    {
        _ready = state;
        if (_ready)
        {
            _readyUpNext.text = "R";
        }
        else
        {
            _readyUpNext.text = "N";
        }

    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected || PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null) return;
        foreach(KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Player newPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == newPlayer);
        if (index != -1)
        {
            _listings[index].SetPlayerInfo(newPlayer);
        }
        else
        {
            PlayerListing listing = Instantiate(_playerListing, _content);
            if (listing != null)
            {
                listing.SetPlayerInfo(newPlayer);
                _listings.Add(listing);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    public void OnClick_StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
