using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    public float sensitivity = 1;
    public float dragSpeed = 2;

    private Vector3 dragOrigin;

    //Constraints
    public float maxSize;
    public float minSize;

    // Update is called once per frame
    void Update()
    {
        Drag();
    }

    private void OnGUI()
    {
        if(Event.current.type == EventType.ScrollWheel)
        {
            float scrollWheelChange = Event.current.delta.y * sensitivity;
            Camera.main.orthographicSize = Helper.Constrain(Camera.main.orthographicSize + scrollWheelChange, minSize, maxSize);
        }
    }

    void Drag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
        }
        else if(Input.GetMouseButton(1))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            float factor = dragSpeed * Camera.main.fieldOfView;
            Vector3 move = new Vector3(-pos.x * factor, -pos.y * factor, 0);
            Camera.main.transform.position += move;
            dragOrigin = Input.mousePosition;
        }
    }
}
