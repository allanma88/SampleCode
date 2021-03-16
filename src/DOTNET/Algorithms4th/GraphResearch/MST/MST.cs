using System;
using System.Collections.Generic;

namespace GraphResearch
{
    public abstract class MST
    {
        public MST(EdgeWeightedGraph G)
        {

        }

        public abstract IEnumerable<Edge> Edges();

        public abstract double Weight();
    }
}