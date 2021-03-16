using System;
using System.Collections.Generic;

namespace Digraph
{
    public class Search
    {
        private bool[] _marked;
        private int _count;
        private Digraph _g;

        public Search(Digraph g, int s)
        {
            int n = g.Vertexes.Count;
            _marked = new bool[n];
            _g = g;
            Dfs(s);
        }

        private void Dfs(int v)
        {
            _marked[v] = true;
            foreach (int w in _g.Adj(v))
            {
                if (!_marked[w])
                {
                    Dfs(w);
                    _count++;
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