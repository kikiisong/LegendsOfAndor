using UnityEngine;
using System.Collections;

[System.Serializable]
public class Vertex : ScriptableObject
{
    public int label;

    public Vertex(int label)
    {
        this.label = label;
    }

    public void Init(int label)
    {
        this.label = label;
    }

    public override string ToString()
    {
        return "Vertex " + label;
    }

    public bool Equals(Vertex vertex)
    {
        return label == vertex.label;
    }
}
