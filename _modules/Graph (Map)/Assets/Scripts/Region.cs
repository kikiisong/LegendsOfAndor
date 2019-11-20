using UnityEngine;
using System.Collections;

[System.Serializable]
public class Region : Vertex
{
    public string regionName;
    public Vector3 position;

    public Region Init(int label, Vector3 vector)
    {
        base.Init(label);
        position = vector;
        return this;
    }

    public static Region Create(int label, Vector3 vector)
    {
        return ScriptableObject.CreateInstance<Region>().Init(label, vector);
    }
}
