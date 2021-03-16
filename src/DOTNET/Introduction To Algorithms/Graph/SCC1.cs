using System.Collections.Generic;

namespace Graph
{
    public class SCC1
    {
        private int[] _ids;
        private bool[] _marked;

        public SCC1(Digraph1 g)
        {
            _ids = new int[g.V];
            _marked = new bool[g.V];

            Stack<int> vertices = DfsOrder(g);
            Digraph1 gt = g.Transpose();
            Dfs(gt, vertices);
        }

        public int[] Ids
        {
            get
            {
                return _ids;
            }
        }

        private Stack<int> DfsOrder(Digraph1 g)
        {
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < g.V; i++)
            {
                if (!_marked[i])
                {
                    _marked[i] = true;
                    DfsOrderVisit(g, i, stack);
                }
            }

            return stack;
        }

        private void DfsOrderVisit(Digraph1 g, int u, Stack<int> stack)
        {
            foreach (var v in g.Adj(u))
            {
                if (!_marked[v])
                {
                    _marked[v] = true;
                    DfsOrderVisit(g, v, stack);
                }
            }
            stack.Push(u);
        }

        private int _count;
        private void Dfs(Digraph1 g, Stack<int> vertices)
        {
            for (int i = 0; i < g.V; i++)
            {
                _marked[i] = false;
            }
            foreach (int i in vertices)
            {
                if (!_marked[i])
                {
                    _marked[i] = true;
                    DfsVisit(g, i);
                    _count++;
                }
            }
        }

        private void DfsVisit(Digraph1 g, int u)
        {
            _ids[u] = _count;
            foreach (var v in g.Adj(u))
            {
                if (!_marked[v])
                {
                    _marked[v] = true;
                    DfsVisit(g, v);
                }
            }
        }
    }
}