﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graph;
using Routines;
using System;
using UnityEngine.EventSystems;

[System.Serializable]
public class GameGraph : Graph<Region, Border>
{
    public static GameGraph Instance;

    void Awake()
    {
        Instance = this;
        Transform();
    }

    private void Transform()
    {
        foreach(Region region in vertices)
        {
            region.position = transform.TransformPoint(region.position); //or TransformPoint?
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.cyan;
            foreach (Region region in vertices)
            {
                Gizmos.DrawSphere(transform.TransformPoint(region.position), 1);
            }
            foreach (Border border in edges)
            {
                Vector3 from = transform.TransformPoint(border.from.position);
                Vector3 to = transform.TransformPoint(border.to.position);
                if (border.isDirected)
                {
                    Gizmos.color = Color.red;
                    DrawArrow.ForGizmo(from, to - from);
                }
                else
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(from, to);
                }

            }
        }
        else
        {
            Gizmos.color = Color.cyan;
            foreach (Region region in vertices)
            {
                Gizmos.DrawSphere(region.position, 1);
            }
            foreach (Border border in edges)
            {
                Vector3 from = border.from.position;
                Vector3 to = border.to.position;
                if (border.isDirected)
                {
                    Gizmos.color = Color.red;
                    DrawArrow.ForGizmo(from, to - from);
                }
                else
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawLine(from, to);
                }

            }
        }
    }

    public void PlaceAt(GameObject gameObject, Region location, float time)
    {
        StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, location.position, time));
    }

    public void PlaceAt(GameObject gameObject, int label)
    {
        Region target = Find(label);
        gameObject.transform.position = target.position;
    }

    public void PlaceAt(GameObject gameObject, int label, Vector3 offset)
    {
        Region target = Find(label);
        if(FindNearest(target.position) != FindNearest(target.position + offset)){
            throw new Exception("Offset changed the region of the object.");
        }
        gameObject.transform.position = target.position;
    }

    public void PlaceAt(GameObject gameObject, int label, float timeTaken)
    {
        Region target = Find(label);
        StartCoroutine(CommonRoutines.MoveTo(gameObject.transform, target.position, timeTaken));
    }


    public Region FindNearest(Vector3 position)
    {
        float min = Mathf.Infinity;

        Region closest = null; //Carefull with current location
        foreach(Region region in vertices)
        {
            float distance = (position - region.position).sqrMagnitude;
            if(distance < min)
            {
                min = distance;
                closest = region;
            }
        }

        return closest;
    }

    public Region FindNearest(GameObject gameObject)
    {
        return FindNearest(gameObject.transform.position);
    }


    private bool isPointingUI()
    {
        PointerEventData currentPos = new PointerEventData(EventSystem.current);
        currentPos.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(currentPos, results);
        return results.Count > 0;
    }

    public Vector3 CastRay(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("Graph")) && !isPointingUI()){
            return hit.point;
        }
        else
        {
            throw new Exception("Missed");
;        }
    }

    public List<Region> AdjacentRegions(Region region)
    {
        List<Region> adjacentRegions = new List<Region>();
        foreach(Border border in edges)
        {
            if(border.PartOf(region, out Region other))
            {
                adjacentRegions.Add(other);
            }
        }
        return adjacentRegions;
    }


    public void DrawTest()
    {
        foreach(Region location in vertices)
        {
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = location.position;
        }
    }


    public List<M> FindObjectsOnRegion<M>(Region region) where M : MonoBehaviour
    {
        List<M> list = new List<M>();
        foreach(M m in GameObject.FindObjectsOfType<M>())
        {
            Region r = FindNearest(m.transform.position);
            if (r.label == region.label)
            {
                list.Add(m);
            }           
        }
        return list;
    }


    public List<M> FindObjectsOnRegion<M>(int regionNumber) where M : MonoBehaviour
    {
        List<M> list = new List<M>();
        foreach (M m in GameObject.FindObjectsOfType<M>())
        {
            Region region = FindNearest(m.transform.position);
            if (regionNumber == region.label)
            {
                list.Add(m);
            }
        }
        return list;
    }

    
    public Region NextEnemyRegion(Region currentRegion) 
    {
        foreach(Border border in edges)
        {
            if (border.isDirected)
            {
                if (border.from.label == currentRegion.label)
                {
                    return border.to;
                }
            }
        }
        throw  new NoNextRegionException();
    }



    public class NoNextRegionException : System.Exception
    {

    }

    
}