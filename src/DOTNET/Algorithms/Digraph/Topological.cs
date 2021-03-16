using System;
using System.Collections.Generic;
using System.Linq;

namespace Digraph
{
    public class TopologicalSample
    {
        public void Run()
        {
            Digraph g = BuildGraph();
            Topological topological = GetTopological(g);
            var order = topological.Order();
            if (order != null)
            {
                string orderStr = string.Join(",", order);
                Console.WriteLine($"Topological order is {orderStr}");
            }
            else
            {
                Console.WriteLine("Not a dag");
            }
        }

        private Topological GetTopological(Digraph g)
        {
            // return new DfsTopological(g);
            return new BfsTopological(g);
        }

        private Digraph BuildGraph()
        {
            List<int> vertexes = new List<int>() { 0, 1, 2, 3 };
            Digraph g = new Digraph(vertexes);
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(0, 3);
            return g.Reverse();
        }
    }

    public abstract class Topological
    {
        public Topological(Digraph g)
        {

        }

        public abstract bool IsDag();

        public abstract IEnumerable<int> Order();
    }

    public class BfsTopological : Topological
    {
        private Cycle _cycle;
        private Digraph _g;

        public BfsTopological(Digraph g) : base(g)
        {
            _g = g;
            _cycle = new BfsCycle(g);
        }

        public override bool IsDag()
        {
            return !_cycle.HasCycle();
        }

        public override IEnumerable<int> Order()
        {
            if (IsDag())
            {
                Stack<int> stack = new Stack<int>();
                var inDegrees = new int[_g.Vertexes.Max() + 1];
                foreach (var v in _g.Vertexes)
                {
                    foreach (var w in _g.Adj(v))
                    {
                        inDegrees[w]++;
                    }
                }
                Queue<int> queue = new Queue<int>();
                foreach (var v in _g.Vertexes)
                {
                    if (inDegrees[v] == 0)
                    {
                        queue.Enqueue(v);
                    }
                }
                while (queue.Count > 0)
                {
                    int v = queue.Dequeue();
                    stack.Push(v);
                    foreach (var w in _g.Adj(v))
                    {
                        if (--inDegrees[w] == 0)
                        {
                            queue.Enqueue(w);
                        }
                    }
                }
                return stack;
            }
            else
            {
                return null;
            }
        }
    }

    public class BfsTopological1 : Topological
    {
        private Cycle _cycle;
        private Digraph _g;

        public BfsTopological1(Digraph g) : base(g)
        {
            _g = g;
            _cycle = new BfsCycle(g);
        }

        public override bool IsDag()
        {
            return !_cycle.HasCycle();
        }

        public override IEnumerable<int> Order()
        {
            if (IsDag())
            {
                Stack<int> stack = new Stack<int>();
                var gReverse = _g.Reverse();
                var outDegrees = new int[_g.Vertexes.Max() + 1];
                foreach (var v in _g.Vertexes)
                {
                    outDegrees[v] = _g.Adj(v).Count;
                }
                Queue<int> queue = new Queue<int>();
                foreach (var v in _g.Vertexes)
                {
                    if (outDegrees[v] == 0)
                    {
                        queue.Enqueue(v);
                    }
                }
                while (queue.Count > 0)
                {
                    int v = queue.Dequeue();
                    stack.Push(v);
                    foreach (var w in gReverse.Adj(v))
                    {
                        if (--outDegrees[w] == 0)
                        {
                            queue.Enqueue(w);
                        }
                    }
                }
                return stack;
            }
            else
            {
                return null;
            }
        }
    }

    public class DfsTopological : Topological
    {
        private Cycle _cycle;
        private Order _order;

        public DfsTopological(Digraph g) : base(g)
        {
            _cycle = new DfsCycle(g);
            _order = new Order(g);
        }

        public override bool IsDag()
        {
            return !_cycle.HasCycle();
        }

        public override IEnumerable<int> Order()
        {
            if (IsDag())
            {
                return _order.ReversePost();
            }
            else
            {
                return null;
            }
        }
    }
}