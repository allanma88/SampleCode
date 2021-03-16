using System;
using System.Collections.Generic;

namespace Graph
{
    public class Search
    {
        private bool[] _marked;
        private Graph _g;
        private int _count;

        public Search(Graph g, int s)
        {
            _marked = new bool[g.Vertexes.Count];
            _g = g;
            Dfs(s);
        }

        private void Dfs(int v)
        {
            _count++;
            _marked[v] = true;
            foreach (var w in _g.Adj(v))
            {
                if (!_marked[w])
                {
                    Dfs(w);
                }
            }
        }

        public bool Marked(int v)
        {
            return _marked[v];
        }

        public int Count()
        {
            return _count;
        }
    }
}