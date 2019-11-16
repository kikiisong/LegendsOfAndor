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
    private RoomsCanvases _roomsCanvases;

    public void FirstInitialize(RoomsCanvases roomsCanvases)
    {
        _roomsCanvases = roomsCanvases;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("update");
        foreach (RoomInfo info in roomList)
        {
            print(info.Name);
            if (info.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if(index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {
                    RoomListing roomListing = Instantiate(_roomListing, _content);
                    if (roomListing != null)
                    {
                        roomListing.SetRoomInfo(info);
                        _listings.Add(roomListing);
                    }
                }
            }
        }
        print("end");
    }

    public override void OnJoinedRoom()
    {
        _roomsCanvases.CurrentRoomCanvas.Show();
        _content.DestroyChildren();
        _listings.Clear();              
    }
}
