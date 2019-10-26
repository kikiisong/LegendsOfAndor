using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private RoomListing _roomListing;

    private List<RoomListing> _listings = new List<RoomListing>();
    private RoomsCanvases roomsCanvases;

    public void Initialize(RoomsCanvases roomsCanvases)
    {
        this.roomsCanvases = roomsCanvases;
    }
    public override void OnJoinedRoom()
    {
        roomsCanvases.CurrentRoomCanvas.Show();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if(index != -1)
                {
                    Destroy(_listings[index].gameObject);
                }
            }
            else
            {
                RoomListing listing = Instantiate(_roomListing, _content);
                listing.SetRoomInfo(info);
                _listings.Add(listing);
            }
            
        }
    }
}
