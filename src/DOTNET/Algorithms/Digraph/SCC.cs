using System;
using System.Collections.Generic;

namespace Digraph
{
    public class SCCSample
    {
        public void Run()
        {
            Digraph g = BuildGraph(false);
            SCC scc = GetSCC(g);
            Console.WriteLine($"The digraph connected is {scc.StrongConnected()}, the cc count is {scc.Count()}");

            g = BuildGraph(true);
            scc = GetSCC(g);
            Console.WriteLine($"The digraph connected is {scc.StrongConnected()}, the cc count is {scc.Count()}");
            Console.WriteLine(scc.StrongConnected(0, 3));
        }

        private SCC GetSCC(Digraph g)
        {
            // return new DfsSCC(g);
            return new BfsSCC(g);
        }

        private Digraph BuildGraph(bool connected)
        {
            if (connected)
            {
                List<int> vertexes = new List<int>() { 0, 1, 2, 3 };
                Digraph g = new Digraph(vertexes);
                g.AddEdge(0, 1);
                g.AddEdge(1, 2);
                g.AddEdge(2, 3);
                g.AddEdge(3, 0);
                return g;
            }
            else
            {
                List<int> vertexes = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
                Digraph g = new Digraph(vertexes);
                g.AddEdge(0, 5);
                g.AddEdge(0, 1);
                g.AddEdge(0, 6);
                g.AddEdge(5, 4);
                g.AddEdge(6, 4);
                g.AddEdge(2, 0);
                g.AddEdge(2, 3);
                g.AddEdge(3, 5);
                return g;
            }
        }
    }

    public abstract class SCC
    {
        public SCC(Digraph g)
        {
        }

        public abstract bool StrongConnected(int v, int w);
        public abstract bool StrongConnected();

        public abstract int Count();

        public abstract int Id(int v);
    }

    public class DfsSCC : SCC
    {
        private bool[] _marked;
        private int _count;
        private int[] _id;

        public DfsSCC(Digraph g) : base(g)
        {
            _marked = new bool[g.Vertexes.Count];
            _id = new int[g.Vertexes.Count];
            Digraph gReverse = g.Reverse();
            Order order = new Order(gReverse);
            foreach (int v in order.ReversePost())
            {
                if (!_marked[v])
                {
                    Dfs(g, v);
                    _count++;
                }
            }
        }

        private void Dfs(Digraph g, int v)
        {
            _marked[v] = true;
            _id[v] = _count;
            foreach (int w in g.Adj(v))
            {
                if (!_marked[w])
                {
                    Dfs(g, w);
                }
            }
        }

        public override bool StrongConnected(int v, int w)
        {
            return _id[v] == _id[w];
        }

        public override bool StrongConnected()
        {
            return _count == 1;
        }

        public override int Count()
        {
            return _count;
        }

        public override int Id(int v)
        {
            return _id[v];
        }
    }

    public class BfsSCC : SCC
    {
        private bool[] _marked;
        private int _count;
        private int[] _id;

        public BfsSCC(Digraph g) : base(g)
        {
            _marked = new bool[g.Vertexes.Count];
            _id = new int[g.Vertexes.Count];
            Digraph gReverse = g.Reverse();
            Order order = new Order(gReverse);
            foreach (int v in order.ReversePost())
            {
                if (!_marked[v])
                {
                    Bfs(g, v);
                    _count++;
                }
            }
        }

        private void Bfs(Digraph g, int s)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);
            while (queue.Count > 0)
            {
                int v = queue.Dequeue();
                _marked[v] = true;
                _id[v] = _count;
                foreach (int w in g.Adj(v))
                {
                    if (!_marked[w])
                    {
                        queue.Enqueue(w);
                    }
                }
            }
        }

        public override bool StrongConnected(int v, int w)
        {
            return _id[v] == _id[w];
        }

        public override bool StrongConnected()
        {
            return _count == 1;
        }

        public override int Count()
        {
            return _count;
        }

        public override int Id(int v)
        {
            return _id[v];
        }
    }
}