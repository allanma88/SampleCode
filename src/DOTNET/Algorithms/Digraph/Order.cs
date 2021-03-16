using System;
using System.Collections.Generic;

namespace Digraph
{
    public class OrderSample
    {
        public void Run()
        {
            Digraph g = BuildGraph();
            Order order = new Order(g);
            Console.WriteLine("Pre: " +  string.Join(",", order.Pre()));
            Console.WriteLine("Reverse Pre: " +  string.Join(",", order.ReversePre()));
            Console.WriteLine("Post: " +  string.Join(",", order.Post()));
            Console.WriteLine("Reverse Post: " +  string.Join(",", order.ReversePost()));
        }

        private Digraph BuildGraph()
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

    public class Order
    {
        private Queue<int> _pre;
        private Queue<int> _post;
        private Stack<int> _reversePre;
        private Stack<int> _reversePost;
        private Digraph _g;
        private bool[] _marked;

        public Order(Digraph g)
        {
            _g = g;
            _marked = new bool[g.Vertexes.Count];
            _pre = new Queue<int>();
            _reversePre = new Stack<int>();
            _post = new Queue<int>();
            _reversePost = new Stack<int>();
            foreach (int v in _g.Vertexes)
            {
                if (!_marked[v])
                {
                    Dfs(v);
                }
            }
        }

        private void Dfs(int v)
        {
            _marked[v] = true;
            _pre.Enqueue(v);
            _reversePre.Push(v);
            foreach (var w in _g.Adj(v))
            {
                if (!_marked[w])
                {
                    Dfs(w);
                }
            }
            _post.Enqueue(v);
            _reversePost.Push(v);
        }

        public Queue<int> Pre()
        {
            return _pre;
        }

        public Queue<int> Post()
        {
            return _post;
        }

        public Stack<int> ReversePre()
        {
            return _reversePre;
        }

        public Stack<int> ReversePost()
        {
            return _reversePost;
        }
    }
}