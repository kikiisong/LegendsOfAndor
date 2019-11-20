using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Graph
{
    [System.Serializable]
    public class Graph<V, E>  : MonoBehaviour where V : Vertex where E : Edge<V>
    {
        public List<V> vertices;
        public List<E> edges; //Use 2d array later

        public void Add(V newVertex)
        {
            vertices.Add(newVertex);
        }


        public void Add(E edge)
        {
           edges.Add(edge);
        }

        public V Find(int label)
        {
            V v = null;
            foreach(V vertex in vertices)
            {
                if(vertex.label == label)
                {
                    //Debug.Log(label.ToString());
                    v = vertex;
                }
            }
            Debug.Log("Vertex not found");
            return v;
        }

        private bool IsUnique(V newVertex)
        {
            return true;
        }
    }
    /*
    [System.Serializable]
    public class Vertex
    {
        public int label;

        public Vertex(int label)
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
    }*/
    /*
    [System.Serializable]
    public class Edge<V> : ScriptableObject where V : Vertex
    {
        public V v1;
        public V v2;

        public Edge(V v1, V v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public override string ToString()
        {
            return "" + v1.label + "-" + v2.label;
        }

        /*public static E<V> Create<V, E>(V v1, V v2) where V:Vertex where E:Edge<V>
        {
            Edge<V> edge = ScriptableObject.CreateInstance<Edge<V>>();
            edge.v1 = v1;
            edge.v2 = v2;
            return edge;
        }*
    }*/
}

