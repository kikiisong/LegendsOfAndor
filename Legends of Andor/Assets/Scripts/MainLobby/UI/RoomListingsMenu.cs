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

    [SceneName]
    public string nextScene;

    public override void OnJoinedRoom()
    {
        print("Joined room");
        if (PhotonNetwork.IsMasterClient)
        {
            print("Loading " + nextScene);
            PhotonNetwork.LoadLevel(nextScene);
        }
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
