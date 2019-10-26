using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvases : MonoBehaviour
{
    public CreateOrJoinRoomCanvas createOrJoinRoomCanvas;
    public CurrentRoomCanvas currentRoomCanvas;

    public void Awake()
    {
        createOrJoinRoomCanvas.Initialize(this);
        currentRoomCanvas.Initialize(this);
    }
}
