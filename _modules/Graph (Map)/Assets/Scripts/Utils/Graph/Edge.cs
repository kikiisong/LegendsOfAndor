using UnityEngine;
using System.Collections;

namespace Graph
{
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
        }*/
    }
}
