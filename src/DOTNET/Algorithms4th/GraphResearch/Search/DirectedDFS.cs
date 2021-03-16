using System.Collections.Generic;

namespace GraphResearch
{
    public class DirectedDFS
    {
        private bool[] _marked;

        public DirectedDFS(Digraph G, int s) : this(G, (IEnumerable<int>)new[] { s })
        {

        }

        public DirectedDFS(Digraph G, IEnumerable<int> sources)
        {
            _marked = new bool[G.V()];
            foreach (var s in sources)
            {
                if (!_marked[s])
                {
                    Dfs(G, s);
                }
            }
        }

        private void Dfs(Digraph G, int v)
        {
            _marked[v] = true;
            foreach (var w in G.Adj(v))
            {
                if (!_marked[w])
                {
                    Dfs(G, w);
                }
            }
        }

        public bool Marked(int v)
        {
            return _marked[v];
        }
    }
}