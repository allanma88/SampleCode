using System;
using System.Collections.Generic;

namespace Graph
{
    public class CycleSample
    {
        public void Run()
        {
            Graph g = BuildGraph();
            Cycle c = new Cycle(g);
            bool hasCycle = c.HasCycle();
            if (hasCycle)
            {
                var cycle = c.GetCycle();
                string cycleStr = string.Join("->", cycle);
                Console.WriteLine($"has cycle, the cycle is {cycleStr}");
            }
            else
            {
                Console.WriteLine("hasn't cycle");
            }
        }

        private Graph BuildGraph()
        {
            List<int> vertexes = new List<int>() { 0, 1, 2, 3 };
            Graph g = new Graph(vertexes);
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 0);
            g.AddEdge(3, 0);
            return g;
        }
    }

    //TODO: 包含self-loops or parallel edges怎么办
    public class Cycle
    {
        private Graph _g;
        private bool[] _marked;
        private bool _hasCycle;
        private int[] _pathTo;
        private Stack<int> _cycle = new Stack<int>();

        public Cycle(Graph g)
        {
            _g = g;
            _marked = new bool[g.Vertexes.Count];
            _pathTo = new int[g.Vertexes.Count];
            foreach (int v in g.Vertexes)
            {
                if (!_marked[v] && !_hasCycle)
                {
                    Dfs(v, v);
                }
            }
        }

        private void Dfs(int v, int s)
        {
            _marked[v] = true;
            foreach (int w in _g.Adj(v))
            {
                if (_hasCycle)
                {
                    break;
                }
                if (!_marked[w])
                {
                    _pathTo[w] = v;
                    Dfs(w, v);
                }
                else if (w != s)
                {
                    _hasCycle = true;
                    _cycle.Push(w);
                    while (v != w)
                    {
                        _cycle.Push(v);
                        v = _pathTo[v];
                    }
                    _cycle.Push(w);
                }
            }
        }

        public bool HasCycle()
        {
            return _hasCycle;
        }

        public Stack<int> GetCycle()
        {
            return _cycle;
        }
    }
}