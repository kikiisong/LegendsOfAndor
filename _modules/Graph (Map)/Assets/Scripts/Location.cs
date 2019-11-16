using UnityEngine;
using System.Collections;

[System.Serializable]
public class Location : Vertex
{
    public new string name;
    public Vector3 position;

    public Location(int label, Vector3 vector) : base(label)
    {
        position = vector;
    }

    public void Init(int label, Vector3 vector)
    {
        Init(label);
        position = vector;
    }

    public static Location Create(int label, Vector3 vector)
    {
        Location location = ScriptableObject.CreateInstance<Location>();
        location.Init(label, vector);
        return location;
    }
}
