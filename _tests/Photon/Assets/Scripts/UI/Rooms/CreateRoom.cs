using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviour
{
    [SerializeField]
    private Text _roomName;

    public void OnClick_CreateRoom()
    {
        RoomOptions options = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, null);
    }
}
