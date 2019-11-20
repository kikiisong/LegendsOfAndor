using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graph;
using Routines;
using System;

public class GameGraph : Graph<Region, Border>
{
    public static GameGraph Instance;

    void Awake()
    {
        Instance = this;
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

    public void PlaceAt(GameObject gameObject, Region location, float time)
    {
        StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, location.position, time));
    }

    public void PlaceAt(GameObject gameObject, int label)
    {
        Region target = Find(label);
        Debug.Log(target.position.ToString());
        gameObject.transform.position = target.position;
        Debug.Log(gameObject.transform.position.ToString());
    }

    public void PlaceAt(GameObject gameObject, int label, float timeTaken)
    {
        Region target = Find(label);
        StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, target.position, timeTaken));
    }


    public Region FindNearest(Vector3 mouse)
    {
        //First find possible moves ?
        float min = Mathf.Infinity;
        float distance = Mathf.Infinity;

        Region closest = null; //Carefull with current location

        foreach(Region location in vertices)
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
        foreach(Region location in vertices)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = location.position;
        }
    }
}