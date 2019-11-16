using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private PlayerListingsMenu _playerListingsMenu;
    [SerializeField]
    private LeaveRoomMenu _leaveRoomMenu;

    private RoomsCanvases _roomsCanvases;

    public void FirstInitialize(RoomsCanvases roomsCanvases)
    {
        _roomsCanvases = roomsCanvases;
        _playerListingsMenu.FirstInitialize(roomsCanvases);
        _leaveRoomMenu.FirstInitialize(roomsCanvases);
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
