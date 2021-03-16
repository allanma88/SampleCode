namespace GraphResearch
{
    public abstract class TwoColor
    {
        public TwoColor(Graph G)
        {

        }

        public abstract bool IsBipartite();
    }

    public class DepthFirstTwoColor : TwoColor
    {
        private bool[] _marked;

        private bool[] _color;

        private bool _isTwoColorable = true;

        public DepthFirstTwoColor(Graph G) : base(G)
        {
            _marked = new bool[G.V()];
            _color = new bool[G.V()];
            for (int s = 0; s < G.V(); s++)
            {
                if (!_marked[s])
                {
                    Dfs(G, s);
                }
            }
        }

        private void Dfs(Graph G, int v)
        {
            _marked[v] = true;
            foreach (int w in G.Adj(v))
            {
                if (!_marked[w])
                {
                    _color[w] = !_color[v];
                    Dfs(G, w);
                }
                else if (_color[w] == _color[v])
                {
                    _isTwoColorable = false;
                }
            }
        }

        public override bool IsBipartite()
        {
            return _isTwoColorable;
        }
    }
}
