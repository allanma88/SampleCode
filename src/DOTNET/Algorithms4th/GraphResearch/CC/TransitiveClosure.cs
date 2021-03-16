namespace GraphResearch
{
    public class TransitiveClosure
    {
        private DirectedDFS[] _dfs;

        public TransitiveClosure(Digraph G)
        {
            _dfs = new DirectedDFS[G.V()];
            for (int i = 0; i < G.V(); i++)
            {
                _dfs[i] = new DirectedDFS(G, i);
            }
        }

        public bool Reachable(int v, int w)
        {
            return _dfs[v].Marked(w);
        }
    }
}