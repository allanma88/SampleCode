using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public class Cycle
    {
        private Vertex[] _vertices;
        private bool _hasCycle;
        private Stack<int> _cycle;

        public Cycle(GraphBase g)
        {
            _vertices = new Vertex[g.V];
            for (int i = 0; i < g.V; i++)
            {
                _vertices[i] = new Vertex { Color = Color.White, Val = i };
            }

            for (int i = 0; i < g.V; i++)
            {
                if (_hasCycle)
                {
                    break;
                }
                if (_vertices[i].Color == Color.White)
                {
                    DfsVisit(g, i, i);
                }
            }
        }

        private void DfsVisit(GraphBase g, int u, int p)
        {
            _vertices[u].Color = Color.Gray;
            foreach (var v in g.Adj(u))
            {
                if (_hasCycle)
                {
                    break;
                }
                if (_vertices[v].Color != Color.Gray)
                {
                    _vertices[v].Parent = _vertices[u];
                    DfsVisit(g, v, u);
                }
                else if (v != p)
                {
                    _cycle = new Stack<int>();
                    _cycle.Push(v);
                    while (u != v)
                    {
                        _cycle.Push(u);
                        u = _vertices[u].Parent.Val;
                    }
                    _cycle.Push(v);
                    _hasCycle = true;
                    break;
                }
            }
        }

        public bool HasCycle
        {
            get
            {
                return _hasCycle;
            }
        }

        public Stack<int> CyclePath
        {
            get
            {
                return _cycle;
            }
        }
    }
}