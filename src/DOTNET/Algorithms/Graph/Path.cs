using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class PathSample
    {
        public void Run()
        {
            var g = BuildGraph();
            int s = 0, t = 4;
            PathUseDfs pathUseDfs = new PathUseDfs(g, s);
            PathUseBfs pathUseBfs = new PathUseBfs(g, s);
            bool hasPathTo = pathUseDfs.HasPathTo(t);
            if (hasPathTo)
            {
                Console.WriteLine($"{s} has path to {t}");
                var paths = pathUseDfs.PathTo(t);
                string pathStr = string.Join("->", paths);
                Console.WriteLine($"{s} to {t} path is : {pathStr}");
                var shortestPaths = pathUseBfs.ShortestPathTo(t);
                string shortestPathStr = string.Join("->", shortestPaths);
                Console.WriteLine($"{s} to {t} shortest path is : {shortestPathStr}");
            }
            else
            {
                Console.WriteLine($"{s} has not path to {t}");
            }
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
            return g;
        }
    }

    public class PathUseDfs
    {
        private Graph _g;
        private bool[] _marked;
        private int[] _pathTo;
        private int _s;

        public PathUseDfs(Graph g, int s)
        {
            _marked = new bool[g.Vertexes.Count];
            _pathTo = new int[g.Vertexes.Count];
            _g = g;
            _s = s;
            Dfs(s);
        }

        public bool HasPathTo(int t)
        {
            return _marked[t];
        }

        public Stack<int> PathTo(int t)
        {
            Stack<int> stack = new Stack<int>();
            int v = t;
            while (v != _s)
            {
                stack.Push(v);
                v = _pathTo[v];
            }
            stack.Push(_s);
            return stack;
        }

        private void Dfs(int v)
        {
            foreach (var w in _g.Adj(v))
            {
                if (!_marked[w])
                {
                    _marked[w] = true;
                    _pathTo[w] = v;
                    Dfs(w);
                }
            }
        }

    }

    public class PathUseBfs
    {
        private Graph _g;
        private bool[] _marked;
        private int[] _pathTo;
        private int _s;

        public PathUseBfs(Graph g, int s)
        {
            _marked = new bool[g.Vertexes.Count];
            _pathTo = new int[g.Vertexes.Count];
            _g = g;
            _s = s;
        }

        private void Bfs()
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(_s);
            while (queue.Count > 0)
            {
                int q = queue.Dequeue();
                foreach (var w in _g.Adj(q))
                {
                    if (!_marked[w])
                    {
                        _pathTo[w] = q;
                        queue.Enqueue(w);
                        _marked[w] = true;
                    }
                }
            }
        }

        public bool HasPathTo(int t)
        {
            return _marked[t];
        }

        public Stack<int> ShortestPathTo(int t)
        {
            Stack<int> stack = new Stack<int>();
            int v = t;
            while (v != _s)
            {
                stack.Push(v);
                v = _pathTo[v];
            }
            stack.Push(_s);
            return stack;
        }
    }
}