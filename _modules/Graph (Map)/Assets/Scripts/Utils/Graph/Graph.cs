using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor;

namespace Graph
{
    public class Graph<V, E>  : MonoBehaviour where V : Vertex where E : Edge<V>
    {
        public V[] vertices = new V[0];
        //Use 2d array later
        public E[] edges = new E[0];

        /// <summary>
        /// Adds a vertex. Label needs to be unique.
        /// </summary>
        public void Add(V newVertex)
        {
            if (!IsUnique(newVertex))
            {
                Debug.Log(newVertex.ToString());
                //return;
            }

            //vertices.Add(newVertex);
            //ArrayUtility.Add<V>(ref vertices, newVertex);
        }

        /// <summary>
        /// Adds a vertex.
        /// </summary>
        public void Add(V from, V to)
        {
           // edges.Add((E) new Edge(from, to));
        }

        public void Add(E edge)
        {
            //edges.Add(edge);
            //edge.
            if(Utils.Contains<V>(vertices, (V) edge.v1, (V)edge.v2))
            {
                //ArrayUtility.Add<E>(ref edges, edge);
            }
            else
            {
                Debug.Log("Couldnt add edge");
            }
            
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

