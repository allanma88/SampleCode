using System.Collections.Generic;

namespace GraphResearch
{
    public class DepthFirstPaths : Paths
    {
        private bool[] _marked;

        private int[] _edgeTo;

        private int _s;
        
        public DepthFirstPaths(Graph G, int s)
            : base(G, s)
        {
            _marked = new bool[G.V()];
            _edgeTo = new int[G.V()];
            _s = s;
            Dfs(G, s);
        }

        private void Dfs(Graph G, int v)
        {
            _marked[v] = true;
            foreach (var s in G.Adj(v))
            {
                if (!_marked[s])
                {
                    _edgeTo[s] = v;
                    Dfs(G, s);
                }
            }
        }

        public override bool HasPathTo(int v)
        {
            return _marked[v];
        }

        public override IEnumerable<int> PathTo(int v)
        {
            if (!HasPathTo(v))
            {
                return null;
            }
            Stack<int> stack = new Stack<int>();
            for (int i = v; i != _s; i = _edgeTo[i])
            {
                stack.Push(i);
            }
            stack.Push(_s);
            return stack;
        }
    }
}