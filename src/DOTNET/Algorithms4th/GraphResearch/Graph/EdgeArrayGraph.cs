using System.Collections.Generic;

namespace GraphResearch
{
    public class EdgeArrayGraph : Graph
    {
        private List<Edge> _edges = new List<Edge>();

        public EdgeArrayGraph(int V)
            : base(V)
        {
        }

        public EdgeArrayGraph(string path)
            : base(path)
        {
        }

        public override void AddEdge(int v, int w)
        {
            base.AddEdge(v, w);
            _edges.Add(new Edge(v, w));
        }

        public override IEnumerable<int> Adj(int v)
        {
            List<int> adjacencies = new List<int>();
            foreach (var edge in _edges)
            {
                if (edge.Source == v)
                {
                    adjacencies.Add(edge.Dest);
                }
                else if (edge.Dest == v)
                {
                    adjacencies.Add(edge.Source);
                }
            }
            return adjacencies;
        }

        protected override void BuildGraph(int v)
        {

        }

        private class Edge
        {
            public Edge(int source, int dest)
            {
                Source = source;
                Dest = dest;
            }

            public int Source { get; set; }

            public int Dest { get; set; }
        }
    }
}