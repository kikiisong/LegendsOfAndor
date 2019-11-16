using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoinRoomCanvas : MonoBehaviour
{
    [SerializeField]
    private CreateRoomMenu _createRoomMenu;
    [SerializeField]
    private RoomListingsMenu _roomListingsMenu;

    private RoomsCanvases _roomsCanvases;

    public void FirstInitialize(RoomsCanvases roomsCanvases)
    {
        _roomsCanvases = roomsCanvases;
        _createRoomMenu.FirstInitialize(roomsCanvases);
        _roomListingsMenu.FirstInitialize(roomsCanvases);
    }
}
