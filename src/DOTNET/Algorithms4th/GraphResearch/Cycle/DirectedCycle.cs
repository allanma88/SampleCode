using System.Collections.Generic;

namespace GraphResearch
{
    public class DirectedCycle
    {
        private bool[] _marked;
        
        private int[] _edgeTo;

        private Stack<int> _cycle;

        private bool[] _onStack;

        public DirectedCycle(Digraph G)
        {
            _marked = new bool[G.V()];
            _onStack = new bool[G.V()];
            _edgeTo = new int[G.V()];
            for (int v = 0; v < G.V(); v++)
            {
                if (!_marked[v] && !HasCycle())
                {
                    Dfs(G, v);
                }
            }
        }

        public bool HasCycle()
        {
            return _cycle != null;
        }

        public IEnumerable<int> Cycle()
        {
            return _cycle;
        }

        private void Dfs(Digraph G, int v)
        {
            _onStack[v] = true;
            _marked[v] = true;
            foreach (var w in G.Adj(v))
            {
                if (HasCycle())
                {
                    return;
                }
                else if (!_marked[w])
                {
                    _edgeTo[w] = v;
                    Dfs(G, w);
                }
                else if (_onStack[w])
                {
                    _cycle = new Stack<int>();
                    for (int x = v; x != w; x = _edgeTo[x])
                    {
                        _cycle.Push(x);
                    }
                    _cycle.Push(w);
                    _cycle.Push(v);
                }
            }
            _onStack[v] = false;
        }
    }
}