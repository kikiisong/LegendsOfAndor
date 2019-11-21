using UnityEngine;
using System.Collections;
using Graph;

[System.Serializable]
public class Border : Edge<Region>
{
    public int a;
    public bool isDirected;

    public Border(Region v1, Region v2) : base(v1, v2)
    {
    }

    public Border(Region v1, Region v2, bool isDirected) : this(v1, v2)
    {
        this.isDirected = isDirected;
    }

    public override string ToString()
    {
        return from.label + "-" + to.label;
    }
}
