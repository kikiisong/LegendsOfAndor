using UnityEngine;
using System.Collections;

public class Vertex : ScriptableObject
{
    public int label;

    public Vertex Init(int label)
    {
        this.label = label;
        return this;
    }

    public override string ToString()
    {
        return "Vertex " + label;
    }
}
