using System;
using System.Collections.Generic;
using System.IO;

namespace GraphResearch
{
    public abstract class SP
    {
        protected EdgeWeightedDigraph _G;

        protected int _s;

        protected DirectedEdge[] _edgeTo;

        protected double[] _distTo;

        public SP(EdgeWeightedDigraph G, int s)
        {
            _G = G;
            _s = s;
            _distTo = new double[G.V()];
            _edgeTo = new DirectedEdge[G.V()];
        }

        public double DistTo(int v)
        {
            return _distTo[v];
        }

        public bool HasPathTo(int v)
        {
            return _distTo[v] != double.PositiveInfinity;
        }

        public IEnumerable<DirectedEdge> PathTo(int v)
        {
            if (HasPathTo(v))
            {
                Stack<DirectedEdge> path = new Stack<DirectedEdge>();
                while (_edgeTo[v] != null)
                {
                    path.Push(_edgeTo[v]);
                    v = _edgeTo[v].From();
                }
                // for (DirectedEdge e = _edgeTo[v]; e != null; e = _edgeTo[e.From()])
                // {
                //     path.Push(e);
                // }
                return path;
            }
            else
            {
                return null;
            }
        }

        private void Relax(DirectedEdge e)
        {
            var v = e.From();
            var w = e.To();
            if (_distTo[w] > _distTo[v] + e.Weight())
            {
                _distTo[w] = _distTo[v] + e.Weight();
                _edgeTo[w] = e;
            }
        }

        private void Relax(EdgeWeightedDigraph G, int v)
        {
            foreach (var e in G.Adj(v))
            {
                var w = e.To();
                if (_distTo[w] > _distTo[v] + e.Weight())
                {
                    _distTo[w] = _distTo[v] + e.Weight();
                    _edgeTo[w] = e;
                }
            }
        }
    }

    public class DijkstraSP : SP
    {
        private IndexMinPQ<double> _pq;

        public DijkstraSP(EdgeWeightedDigraph G, int s) : base(G, s)
        {
            _pq = new IndexMinPQ<double>(G.E());
            for (int i = 0; i < G.V(); i++)
            {
                _distTo[i] = double.PositiveInfinity;
            }
            _distTo[s] = 0d;
            _pq.Insert(0, 0d);
            while (!_pq.IsEmpty())
            {
                var w = _pq.RemoveMin();
                Relax(G, w);
            }
        }

        private void Relax(EdgeWeightedDigraph G, int v)
        {
            foreach (var e in G.Adj(v))
            {
                var w = e.To();
                if (_distTo[w] > _distTo[v] + e.Weight())
                {
                    _distTo[w] = _distTo[v] + e.Weight();
                    _edgeTo[w] = e;
                    if (_pq.Contains(w))
                    {
                        _pq.Change(w, _distTo[w]);
                    }
                    else
                    {
                        _pq.Insert(w, _distTo[w]);
                    }
                }
            }
        }
    }
}