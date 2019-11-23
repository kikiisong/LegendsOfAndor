using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    public float sensitivity = 10;
    public float dragSpeed = 2;

    private Vector3 dragOrigin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Zoom_FieldOfView();
        Drag();
    }

    void Zoom_FieldOfView()
    {
        float scrollWheelChange =  - Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        Camera.main.fieldOfView += scrollWheelChange;
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

    void Zoom()
    {
        float scrollWheelChange = Input.GetAxis("Mouse ScrollWheel");
        if(scrollWheelChange != 0)
        {
            float radius = scrollWheelChange * 15;
            float posX = Camera.main.transform.eulerAngles.x + 90;
            float posY = -(Camera.main.transform.eulerAngles.y - 90);
            posX = posX / 180 * Mathf.PI;
            posY = posY / 180 * Mathf.PI;

            float x = radius * Mathf.Sin(posX) * Mathf.Cos(posY);
            float y = radius * Mathf.Cos(posX);
            float z = radius = Mathf.Sin(posX) * Mathf.Sin(posY);

            float camX = Camera.main.transform.position.x;
            float camY = Camera.main.transform.position.y;
            float camZ = Camera.main.transform.position.z;

            Camera.main.transform.position = new Vector3(camX + x, camY + y, camZ + z);
        }
    }
}
