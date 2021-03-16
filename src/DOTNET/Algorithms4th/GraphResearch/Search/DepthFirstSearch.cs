namespace GraphResearch
{
    public class DepthFirstSearch : Search
    {
        private bool[] _marked;

        private int[] _edgeTo;

        private int _count;

        public DepthFirstSearch(Graph G, int s)
            : base(G, s)
        {
            _marked = new bool[G.V()];
            _edgeTo = new int[G.V()];
            Dfs(G, s);
        }

        private void Dfs(Graph G, int v)
        {
            _marked[v] = true;
            _count++;
            foreach (var s in G.Adj(v))
            {
                if (!_marked[s])
                {
                    _edgeTo[s] = v;
                    Dfs(G, s);
                }
            }
        }

        public override bool Marked(int v)
        {
            return _marked[v];
        }

        public override int Count()
        {
            return _count;
        }
    }
}