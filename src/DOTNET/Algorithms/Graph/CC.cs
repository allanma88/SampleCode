using System;
using System.Collections.Generic;

namespace Graph
{
    public class CCSample
    {
        public void Run()
        {
            Graph g = BuildGraph();
            CC cc = new CC(g);
            Console.WriteLine($"Is a connected graph: {cc.IsConnected()}");
            int s = 0, t = 3;
            Console.WriteLine($"Is {s} and {t} connected: {cc.IsConnected(s, t)}");
            s = 1; t = 5;
            Console.WriteLine($"Is {s} and {t} connected: {cc.IsConnected(s, t)}");
            Console.WriteLine($"number of connected graph: {cc.Count()}");
        }

        private Graph BuildGraph()
        {
            List<int> vertexes = new List<int>() { 0, 1, 2, 3, 4, 5 };
            Graph g = new Graph(vertexes);
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(3, 0);
            g.AddEdge(4, 5);
            return g;
        }
    }

    public class CC
    {
        private int[] _id;
        private int _count;
        private Graph _g;
        private bool[] _marked;

        public CC(Graph g)
        {
            _id = new int[g.Vertexes.Count];
            _marked = new bool[g.Vertexes.Count];
            _g = g;
            foreach (var v in _g.Vertexes)
            {
                if (!_marked[v])
                {
                    Dfs(v);
                    _count++;
                }
            }
        }

        private void Dfs(int v)
        {
            _id[v] = _count;
            _marked[v] = true;
            foreach (var w in _g.Adj(v))
            {
                if (!_marked[w])
                {
                    Dfs(w);
                }
            }
        }

        public bool IsConnected(int v, int w)
        {
            return _id[v] == _id[w];
        }

        public int Count()
        {
            return _count;
        }

        public int Id(int v)
        {
            return _id[v];
        }

        public bool IsConnected()
        {
            return _count == 1;
        }
    }
}