﻿using UnityEngine;
using System.Collections;

namespace Graph
{
    [System.Serializable]
    public class Edge<V> where V : Vertex
    {
        public V from;
        public V to;

        public bool isDirected;

        public Edge(V v1, V v2)
        {
            from = v1;
            to = v2;
        }

        public override string ToString()
        {
            return "Edge: " + from.label + "-" + to.label;
        }

        public bool PartOf(V current, out V other)
        {
            if (current.Equals(from))
            {
                other = to;
                return true;
            }
            else if (current.Equals(to))
            {
                other = from;
                return true;
            }
            else
            {
                other = current;//?
                return false;
            }
        }
    }
}
