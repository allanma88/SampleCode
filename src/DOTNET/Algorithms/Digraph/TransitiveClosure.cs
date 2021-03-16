using System;
using System.Collections.Generic;

namespace Digraph
{
    public class TransitiveClosure
    {
        private Search[] _searchs;

        public TransitiveClosure(Digraph g)
        {
            _searchs = new Search[g.Vertexes.Count];
        }

        public bool Reachable(int v, int w)
        {
            return _searchs[v].Marked(w);
        }
    }
}