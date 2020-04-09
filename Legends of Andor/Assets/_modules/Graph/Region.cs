using UnityEngine;
using System.Collections;

[System.Serializable]
public class Region : Vertex
{
    public string regionName;
    public Vector3 position;
    public Data data;
    //Easy to serialize and sync accross all players
    [System.Serializable]
    public struct Data
    {
        public int numOfItems;
        public int gold;

        //small item
        public int numWineskin;
        public int brew;

        //number of herb can be used in two way
        public int herb;

        //big item
        public int sheild;
        public int helm;

        //2 means full, 1 means half, 0 means nothing
        public int bow;
        public int falcon;


    }
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

    public override string ToString()
    {
        return "Region " + label;
    }
}
