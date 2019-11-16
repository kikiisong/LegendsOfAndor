using UnityEngine;
using System.Collections;
using Graph;

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
}
