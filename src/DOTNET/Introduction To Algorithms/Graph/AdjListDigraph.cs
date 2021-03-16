using System.Collections.Generic;

namespace Graph
{
    public class AdjListDigraph : DigraphBase
    {
        private Node[] _nodes;

        public AdjListDigraph(int v) : base(v)
        {
            _nodes = new Node[v];
            for (int i = 0; i < v; i++)
            {
                _nodes[i] = new Node();
            }
        }

        public override void AddEdge(int i, int j)
        {
            base.AddEdge(i, j);

            Node n = new Node { Val = j };
            n.Next = _nodes[i].Next;
            _nodes[i].Next = n;
        }

        public override ICollection<int> Adj(int i)
        {
            List<int> adj = new List<int>();
            Node cur = _nodes[i].Next;
            while (cur != null)
            {
                adj.Add(cur.Val);
                cur = cur.Next;
            }
            return adj;
        }

        public bool Exist1(int i, int j)
        {
            Node cur = _nodes[i].Next;
            while (cur != null)
            {
                if (cur.Val == j)
                {
                    return true;
                }
                cur = cur.Next;
            }
            return false;
        }

        public AdjListDigraph Transpose1()
        {
            AdjListDigraph g = new AdjListDigraph(V);
            for (int i = 0; i < V; i++)
            {
                Node cur = _nodes[i].Next;
                while (cur != null)
                {
                    g.AddEdge(cur.Val, i);
                    cur = cur.Next;
                }
            }
            return g;
        }

        public AdjListDigraph Deduplicate1()
        {
            AdjListDigraph g = new AdjListDigraph(V);
            for (int i = 0; i < V; i++)
            {
                HashSet<int> hashset = new HashSet<int>();
                Node cur = _nodes[i].Next;
                while (cur != null)
                {
                    if (cur.Val != i && !hashset.Contains(cur.Val))
                    {
                        g.AddEdge(i, cur.Val);
                        hashset.Add(cur.Val);
                    }
                    cur = cur.Next;
                }
            }
            return g;
        }

        public AdjListDigraph Square1()
        {
            AdjListDigraph g = new AdjListDigraph(V);
            for (int i = 0; i < V; i++)
            {
                Node cur = _nodes[i].Next;
                while (cur != null)
                {
                    g.AddEdge(i, cur.Val);
                    Node cur1 = _nodes[cur.Val];
                    while (cur1 != null)
                    {
                        g.AddEdge(i, cur1.Val);
                        cur1 = cur1.Next;
                    }
                    cur = cur.Next;
                }
            }
            return g;
        }
    }
}