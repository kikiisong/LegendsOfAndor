using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graph;
using Routines;
using System;

public class LocationGraph : Graph<Location, Border>
{
    public static LocationGraph Instance;

    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        //Tests
        DrawTest();
        Location nearest = FindNearest(gameObject.transform.position);
        PlaceAt(test, nearest, 10);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            Location nearest = FindNearest(Input.mousePosition);
            //StartCoroutine(CommonRoutines.MoveTo(gameObjectTest.transform, nearest.position, 10f));
        }*/
    }

    public void PlaceAt(GameObject gameObject, Location location, float time)
    {
        StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, location.position, time));
    }

    public void PlaceAt(GameObject gameObject, int label)
    {
        Location target = Find(label);
        Debug.Log(target.position.ToString());
        gameObject.transform.position = target.position;
        Debug.Log(gameObject.transform.position.ToString());
    }

    public void PlaceAt(GameObject gameObject, int label, float timeTaken)
    {
        Location target = Find(label);
        StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, target.position, timeTaken));
    }


    public Location FindNearest(Vector3 mouse)
    {
        //First find possible moves ?
        float min = Mathf.Infinity;
        float distance = Mathf.Infinity;

        Location closest = null; //Carefull with current location

        foreach(Location location in vertices)
        {
            distance = (mouse - location.position).sqrMagnitude;
            if(distance < min)
            {
                min = distance;
                closest = location;
            }
        }

        //Array.Sort()
        return closest;
    }

    public void ShowPossibleMoves()
    {

    }

    public void DrawTest()
    {
        foreach(Location location in vertices)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = location.position;
        }
    }
}
/*
[System.Serializable]
public class Location : Vertex
{
    public string name;
    public Vector3 position;

    public Location(int label, Vector3 vector) : base(label)
    {
        position = vector;
    }
}
*/
/*
public class Border : Edge<Location>
{
    public int a;
    public Border(Location v1, Location v2) : base(v1, v2)
    {
    }

    
    public void Draw()
    {
       // Debug.DrawLine(v1.position, );
        //Handles
    }

    public static Border Create(Location v1, Location v2)
    {
        Border edge = ScriptableObject.CreateInstance<Border>();
        edge.v1 = v1;
        edge.v2 = v2;
        return edge;
    }
}*/