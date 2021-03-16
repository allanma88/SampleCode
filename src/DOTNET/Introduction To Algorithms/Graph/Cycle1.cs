using System.Collections.Generic;

namespace Graph
{
    public class Cycle1
    {
        private bool[] _marked;
        private int[] _parents;
        private bool _hasCycle;
        private Stack<int> _cycle;

        public Cycle1(GraphBase g)
        {
            _marked = new bool[g.V];
            _parents = new int[g.V];

            for (int i = 0; i < g.V; i++)
            {
                if (_hasCycle)
                {
                    break;
                }
                if (!_marked[i])
                {
                    DfsVisit(g, i, i);
                }
            }
        }

        private void DfsVisit(GraphBase g, int u, int p)
        {
            _marked[u] = true;
            foreach (var v in g.Adj(u))
            {
                if (_hasCycle)
                {
                    break;
                }
                if (!_marked[v])
                {
                    _parents[v] = u;
                    DfsVisit(g, v, u);
                }
                else if (v != p)
                {
                    _cycle = new Stack<int>();
                    _cycle.Push(v);
                    while (u != v)
                    {
                        _cycle.Push(u);
                        u = _parents[u];
                    }
                    _cycle.Push(v);
                    _hasCycle = true;
                    break;
                }
            }
        }

        public bool HasCycle
        {
            get
            {
                return _hasCycle;
            }
        }

        public Stack<int> CyclePath
        {
            get
            {
                return _cycle;
            }
        }
    }
}