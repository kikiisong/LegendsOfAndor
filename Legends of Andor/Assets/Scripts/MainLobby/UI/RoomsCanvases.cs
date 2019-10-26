using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvases : MonoBehaviour
{
    public CreateOrJoinRoomCanvas CreateOrJoinRoomCanvas;
    public CurrentRoomCanvas CurrentRoomCanvas;

    public void Awake()
    {
        CreateOrJoinRoomCanvas.Initialize(this);
        CurrentRoomCanvas.Initialize(this);
    }
}
