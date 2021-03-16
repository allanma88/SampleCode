using System.Collections.Generic;

namespace GraphResearch
{
    public class LazyPrimMST : MST
    {
        private bool[] _marked;

        private Queue<Edge> _mst;

        private MinPQ<Edge> _pq;

        public LazyPrimMST(EdgeWeightedGraph G)
            : base(G)
        {
            _marked = new bool[G.V()];
            _mst = new Queue<Edge>();
            _pq = new MinPQ<Edge>(G.E());
            Visit(G, 0);
            while (!_pq.IsEmpty())
            {
                Edge e = _pq.RemoveMin();
                var v = e.Either();
                var w = e.Other(v);
                if (_marked[v] && _marked[w])
                {
                    continue;
                }
                _mst.Enqueue(e);
                if (!_marked[v])
                {
                    Visit(G, v);
                }
                if (!_marked[w])
                {
                    Visit(G, w);
                }
            }
        }

        public override IEnumerable<Edge> Edges()
        {
            return _mst;
        }

        public override double Weight()
        {
            double weight = 0;
            foreach (var e in _mst)
            {
                weight += e.Weight();
            }
            return weight;
        }

        private void Visit(EdgeWeightedGraph G, int v)
        {
            _marked[v] = true;
            foreach (var e in G.Adj(v))
            {
                var w = e.Other(v);
                if (!_marked[w])
                {
                    _pq.Insert(e);
                }
            }
        }
    }
}