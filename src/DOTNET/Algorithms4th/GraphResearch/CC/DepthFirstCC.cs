namespace GraphResearch
{
    public class DepthFirstCC : CC
    {
        private bool[] _marked;

        private int[] _id;

        private int _count;

        public DepthFirstCC(Graph G)
            : base(G)
        {
            _marked = new bool[G.V()];
            _id = new int[G.V()];
            for (int i = 0; i < G.V(); i++)
            {
                if (!_marked[i])
                {
                    Dfs(G, i);
                    _count++;
                }
            }
        }

        private void Dfs(Graph G, int v)
        {
            _marked[v] = true;
            _id[v] = _count;
            foreach (int i in G.Adj(v))
            {
                if (!_marked[i])
                {
                    Dfs(G, i);
                }
            }
        }

        public override bool Connected(int v, int w)
        {
            return _id[v] == _id[w];
        }

        public override int Count()
        {
            return _count;
        }

        public override int Id(int v)
        {
            return _id[v];
        }
    }
}