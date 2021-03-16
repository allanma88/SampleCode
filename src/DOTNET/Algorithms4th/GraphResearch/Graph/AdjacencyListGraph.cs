using System.Collections.Generic;

namespace GraphResearch
{
    public class AdjacencyListGraph : Graph
    {
        private Bag<int>[] _adjacencies;

        public AdjacencyListGraph(int V)
            : base(V)
        {
        }

        public AdjacencyListGraph(string path)
            : base(path)
        {
            
        }

        public override void AddEdge(int v, int w)
        {
            base.AddEdge(v, w);
            _adjacencies[v].Add(w);
            _adjacencies[w].Add(v);
        }

        public override IEnumerable<int> Adj(int v)
        {
            return _adjacencies[v];
        }

        protected override void BuildGraph(int v)
        {
            _adjacencies = new Bag<int>[v];
            for (int i = 0; i < v; i++)
            {
                _adjacencies[i] = new Bag<int>();
            }
        }
    }
}