using System;
using System.Collections.Generic;

namespace GraphResearch
{
    public class KruskalMST : MST
    {
        private MinPQ<Edge> _pq;

        private Queue<Edge> _mst;

        private UF _uf;

        public KruskalMST(EdgeWeightedGraph G) : base(G)
        {
            _pq = new MinPQ<Edge>(G.E());
            _mst = new Queue<Edge>();
            _uf = new UF(G.V());
            foreach (var e in G.Edges())
            {
                _pq.Insert(e);
            }
            while(!_pq.IsEmpty() && _mst.Count < G.V() - 1) 
            {
                var e = _pq.RemoveMin();
                var v = e.Either();
                var w = e.Other(v);
                if(_uf.Connected(v, w)) 
                {
                    continue;
                }
                _uf.Union(v, w);
                _mst.Enqueue(e);
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
    }
}