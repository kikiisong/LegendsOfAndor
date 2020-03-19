using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoomerVM : MonoBehaviour
{
    public float sensitivity = 1;
    public float dragSpeed = 2;

    public CinemachineVirtualCamera vcam;
    public GameObject follow;
    public PolygonCollider2D polygonCollider;

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
        if (Event.current.type == EventType.ScrollWheel)
        {
            float scrollWheelChange = Event.current.delta.y * sensitivity;
            vcam.m_Lens.OrthographicSize = Helper.Constrain(Camera.main.orthographicSize + scrollWheelChange, minSize, maxSize);
        }
    }

    void Drag()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            var x = Camera.main.transform.position.x;
            var y = Camera.main.transform.position.y;
            follow.transform.position = new Vector3(x, y, 0);
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            float factor = dragSpeed;
            Vector3 move = new Vector3(-pos.x * factor, -pos.y * factor, 0);
            Vector3 next = follow.transform.position + move;
            dragOrigin = Input.mousePosition;
            if (polygonCollider.bounds.Contains(next))
            {
                follow.transform.position = next;
            }
        }
    }
}
