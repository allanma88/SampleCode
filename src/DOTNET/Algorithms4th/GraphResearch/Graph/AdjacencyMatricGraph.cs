using System.Collections.Generic;

namespace GraphResearch
{
    public class AdjacencyMatricGraph : Graph
    {
        private bool[][] _edges;

        public AdjacencyMatricGraph(int V)
            : base(V)
        {

        }

        public AdjacencyMatricGraph(string path)
            : base(path)
        {

        }

        public override void AddEdge(int v, int w)
        {
            base.AddEdge(v, w);
            _edges[v][w] = true;
            _edges[w][v] = true;
        }

        public override IEnumerable<int> Adj(int v)
        {
            List<int> adjacencies = new List<int>();
            for (int i = 0; i < _edges[v].Length; i++)
            {
                if (_edges[v][i] && i != v)
                {
                    adjacencies.Add(i);
                }
            }
            return adjacencies;
        }

        protected override void BuildGraph(int v)
        {
            _edges = new bool[v][];
            for (int i = 0; i < v; i++)
            {
                _edges[i] = new bool[v];
            }
            for (int i = 0; i < v; i++)
            {
                _edges[i][i] = true;
            }
        }
    }
}