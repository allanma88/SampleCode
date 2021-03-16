using System;
using System.Collections.Generic;

namespace GraphResearch
{
    public class SimplePrimMST : MST
    {
        private bool[] _marked;

        private Edge[] _edgeTo;

        private IndexMinPQ<double> _pq;

        public SimplePrimMST(EdgeWeightedGraph G)
            : base(G)
        {
            _marked = new bool[G.V()];
            _edgeTo = new Edge[G.V()];
            _pq = new IndexMinPQ<double>(G.E());
            _pq.Insert(0, 0d);
            while (!_pq.IsEmpty())
            {
                int v = _pq.RemoveMin();
                Visit(G, v);
            }
        }

        public override IEnumerable<Edge> Edges()
        {
            Edge[] edges = new Edge[_edgeTo.Length - 1];
            Array.Copy(_edgeTo, 1, edges, 0, edges.Length);
            return edges;
        }

        public override double Weight()
        {
            double weight = 0;
            for (int i = 1; i < _edgeTo.Length; i++)
            {
                weight += _edgeTo[i].Weight();
            }
            return weight;
        }

        private void Visit(EdgeWeightedGraph G, int v)
        {
            _marked[v] = true;
            foreach (var e in G.Adj(v))
            {
                var w = e.Other(v);
                if (_marked[w])
                {
                    continue;
                }
                if (Get(w) > e.Weight())
                {
                    _edgeTo[w] = e;
                    if (_pq.Contains(w))
                    {
                        _pq.Change(w, e.Weight());
                    }
                    else
                    {
                        _pq.Insert(w, e.Weight());
                    }
                }
            }
        }

        private double Get(int v)
        {
            if (_pq.Contains(v))
            {
                return _pq.Get(v);
            }
            else
            {
                return double.PositiveInfinity;
            }
        }
    }

    public class PrimMST : MST
    {
        private bool[] _marked;

        private Edge[] _edgeTo;

        private double[] _distTo;

        private IndexMinPQ<double> _pq;

        public PrimMST(EdgeWeightedGraph G)
            : base(G)
        {
            _marked = new bool[G.V()];
            _edgeTo = new Edge[G.V()];
            _distTo = new double[G.V()];
            _pq = new IndexMinPQ<double>(G.E());
            for (int v = 0; v < G.V(); v++)
            {
                _distTo[v] = double.PositiveInfinity;
            }
            _distTo[0] = 0d;
            _pq.Insert(0, 0d);
            while (!_pq.IsEmpty())
            {
                int v = _pq.RemoveMin();
                Visit(G, v);
            }
        }

        public override IEnumerable<Edge> Edges()
        {
            Edge[] edges = new Edge[_edgeTo.Length - 1];
            Array.Copy(_edgeTo, 1, edges, 0, edges.Length);
            return edges;
        }

        public override double Weight()
        {
            double weight = 0;
            for (int i = 1; i < _edgeTo.Length; i++)
            {
                weight += _edgeTo[i].Weight();
            }
            return weight;
        }

        private void Visit(EdgeWeightedGraph G, int v)
        {
            _marked[v] = true;
            foreach (var e in G.Adj(v))
            {
                var w = e.Other(v);
                if (_marked[w])
                {
                    continue;
                }
                if (_distTo[w] > e.Weight())
                {
                    _edgeTo[w] = e;
                    _distTo[w] = e.Weight();
                    if (_pq.Contains(w))
                    {
                        _pq.Change(w, e.Weight());
                    }
                    else
                    {
                        _pq.Insert(w, e.Weight());
                    }
                }
            }
        }
    }
}