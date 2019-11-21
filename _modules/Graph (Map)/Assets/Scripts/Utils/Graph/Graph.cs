using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Graph
{
    [System.Serializable]
    public class Graph<V, E>  : MonoBehaviour where V : Vertex where E : Edge<V>
    {
        public List<V> vertices = new List<V>();
        public List<E> edges = new List<E>(); //Use 2d array later

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
                    return vertex;
                }
            }
            Debug.Log("Vertex not found");
            return v;
        }

        private bool IsUnique(V newVertex)
        {
            return true;
        }

        public void Remove(V vertex)
        {
            vertices.Remove(vertex);
        }

        public void Remove(E edge)
        {
            edges.Remove(edge);
        }
    }
}

