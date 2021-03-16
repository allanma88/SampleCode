using System;
using System.Collections.Generic;

namespace Graph
{
    public class BipartiteSample
    {
        public void Run()
        {
            Graph g = BuildGraph();
            Bipartite bipartite = new Bipartite(g);
            bool isBipartite = bipartite.IsBipartite();
            Console.WriteLine(isBipartite);
        }

        public Graph BuildGraph()
        {
            List<int> vertexes = new List<int>() { 0, 1, 2, 3, 4, 5 };
            Graph g = new Graph(vertexes);
            g.AddEdge(0, 1);
            g.AddEdge(0, 5);
            g.AddEdge(0, 2);
            g.AddEdge(1, 2);
            g.AddEdge(1, 4);
            g.AddEdge(2, 3);
            g.AddEdge(2, 5);
            g.AddEdge(3, 4);
            return g;
        }
    }

    public class Bipartite
    {
        private Graph _g;
        private bool[] _marked;
        private bool[] _color;
        private bool _isBipartite = true;

        public Bipartite(Graph g)
        {
            _g = g;
            _marked = new bool[g.Vertexes.Count];
            _color = new bool[g.Vertexes.Count];
            foreach (var v in g.Vertexes)
            {
                if (!_marked[v])
                {
                    Dfs(v);
                }
            }
        }

        public bool IsBipartite()
        {
            return _isBipartite;
        }

        private void Dfs(int v)
        {
            _marked[v] = true;
            foreach (var w in _g.Adj(v))
            {
                if (!_marked[w])
                {
                    _color[w] = !_color[v];
                    Dfs(w);
                }
                else if (_color[w] == _color[v])
                {
                    _isBipartite = false;
                }
            }
        }
    }
}