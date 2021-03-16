using System;
using System.Collections.Generic;

namespace Digraph
{
    public class PathSample
    {
        public void Run()
        {
            Digraph g = BuildGraph();
            int s = 0;
            int t = 3;
            Path p = GetPath(g, s);
            bool hasPath = p.HasPathTo(t);
            if (hasPath)
            {
                var path = p.PathTo(3);
                Console.WriteLine($"The path from {s} to {t} is {string.Join("->", path)}");
            }
            else
            {
                Console.WriteLine($"No path from {s} to {t}");
            }
        }

        private Path GetPath(Digraph g, int s)
        {
            // return new DfsPath(g, s);
            return new BfsPath(g, s);
        }

        private Digraph BuildGraph()
        {
            List<int> vertexes = new List<int>() { 0, 1, 2, 3 };
            Digraph g = new Digraph(vertexes);
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(0, 3);
            return g;
        }

    }

    public abstract class Path
    {
        public Path(Digraph g, int s)
        {

        }

        public abstract bool HasPathTo(int t);

        public abstract IEnumerable<int> PathTo(int t);
    }

    public class DfsPath : Path
    {
        private Digraph _g;
        private int _s;
        private bool[] _marked;
        private int[] _pathTo;

        public DfsPath(Digraph g, int s) : base(g, s)
        {
            _g = g;
            _s = s;
            int n = g.Vertexes.Count;
            _marked = new bool[n];
            _pathTo = new int[n];
            Dfs(s);
        }

        private void Dfs(int v)
        {
            _marked[v] = true;
            foreach (var w in _g.Adj(v))
            {
                if (!_marked[w])
                {
                    _pathTo[w] = v;
                    Dfs(w);
                }
            }
        }

        public override bool HasPathTo(int t)
        {
            return _marked[t];
        }

        public override IEnumerable<int> PathTo(int t)
        {
            if (HasPathTo(t))
            {
                Stack<int> path = new Stack<int>();
                while (t != _s)
                {
                    path.Push(t);
                    t = _pathTo[t];
                }
                path.Push(_s);
                return path;
            }
            else
            {
                return null;
            }
        }
    }

    public class BfsPath : Path
    {
        private Digraph _g;
        private int _s;
        private bool[] _marked;
        private int[] _pathTo;

        public BfsPath(Digraph g, int s) : base(g, s)
        {
            _g = g;
            _s = s;
            int n = g.Vertexes.Count;
            _marked = new bool[n];
            _pathTo = new int[n];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);
            while (queue.Count > 0)
            {
                int v = queue.Dequeue();
                _marked[v] = true;
                foreach (var w in g.Adj(v))
                {
                    if (!_marked[w])
                    {
                        _pathTo[w] = v;
                        queue.Enqueue(w);
                    }
                }
            }
        }

        public override bool HasPathTo(int t)
        {
            return _marked[t];
        }

        public override IEnumerable<int> PathTo(int t)
        {
            if (HasPathTo(t))
            {
                Stack<int> path = new Stack<int>();
                while (t != _s)
                {
                    path.Push(t);
                    t = _pathTo[t];
                }
                path.Push(_s);
                return path;
            }
            else
            {
                return null;
            }
        }
    }
}