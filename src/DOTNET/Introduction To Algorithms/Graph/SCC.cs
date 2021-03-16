using System.Collections.Generic;

namespace Graph
{
    public class SCC
    {
        private int[] _ids;
        private Vertex[] _vertices;

        public SCC(DigraphBase g)
        {
            _ids = new int[g.V];
            _vertices = new Vertex[g.V];
            for (int i = 0; i < g.V; i++)
            {
                _vertices[i] = new Vertex();
            }

            Stack<int> vertices = DfsOrder(g);
            DigraphBase gt = g.Transpose();
            Dfs(gt, vertices);
            //call DFS.G/ to compute finishing times u:f for each vertex u
            //compute GT
            //call DFS.GT/, but in the main loop of DFS, consider the vertices in order of decreasing u:f (as computed in line 1)
            //output the vertices of each tree in the depth-first forest formed in line 3 as a separate strongly connected component
        }

        public int[] Ids
        {
            get
            {
                return _ids;
            }
        }

        private int _time;
        private Stack<int> DfsOrder(DigraphBase g)
        {
            foreach (var vertex in _vertices)
            {
                vertex.Color = Color.White;
            }

            Stack<int> stack = new Stack<int>();

            _time = 0;
            for (int i = 0; i < g.V; i++)
            {
                if (_vertices[i].Color == Color.White)
                {
                    DfsOrderVisit(g, i, stack);
                }
            }

            return stack;
        }

        private void DfsOrderVisit(DigraphBase g, int u, Stack<int> stack)
        {
            _vertices[u].Color = Color.Gray;
            _vertices[u].D = ++_time;
            foreach (var v in g.Adj(u))
            {
                if (_vertices[v].Color == Color.White)
                {
                    DfsOrderVisit(g, v, stack);
                }
            }
            _vertices[u].F = ++_time;
            _vertices[u].Color = Color.Black;
            stack.Push(u);
        }

        private int _count;
        private void Dfs(DigraphBase g, Stack<int> vertices)
        {
            foreach (var vertex in _vertices)
            {
                vertex.Color = Color.White;
            }

            foreach (int i in vertices)
            {
                if (_vertices[i].Color == Color.White)
                {
                    DfsVisit(g, i);
                    _count++;
                }
            }
        }

        private void DfsVisit(DigraphBase g, int u)
        {
            _ids[u] = _count;
            _vertices[u].Color = Color.Gray;
            foreach (var v in g.Adj(u))
            {
                if (_vertices[v].Color == Color.White)
                {
                    DfsVisit(g, v);
                }
            }
            _vertices[u].Color = Color.Black;
        }
    }
}