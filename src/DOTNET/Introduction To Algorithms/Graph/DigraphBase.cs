using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    public abstract class DigraphBase
    {
        private int _v;
        private int _e;
        private Vertex[] _vertices;

        public DigraphBase(int v)
        {
            _v = v;
            _vertices = new Vertex[v];
            for (int i = 0; i < v; i++)
            {
                _vertices[i] = new Vertex { Val = i };
            }
        }

        public int V
        {
            get
            {
                return _v;
            }
        }

        public int E
        {
            get { return _e; }
        }

        public virtual void AddEdge(int i, int j)
        {
            _e++;
        }

        public abstract ICollection<int> Adj(int i);

        public ICollection<int> WhiteAdj(int i)
        {
            List<int> whiteAdj = new List<int>();

            var adj = Adj(i);
            foreach (var j in adj)
            {
                if (_vertices[j].Color == Color.White)
                {
                    whiteAdj.Add(j);
                }
            }

            return whiteAdj;
        }

        public bool Exist(int i, int j)
        {
            return Adj(i).Contains(j);
        }

        public List<int> Bfs(int s)
        {
            List<int> bfs = new List<int>();

            foreach (var vertex in _vertices)
            {
                vertex.Color = Color.White;
                vertex.D = -1;
                vertex.Parent = null;
            }

            _vertices[s].Color = Color.Gray;
            _vertices[s].D = 0;
            _vertices[s].Parent = null;

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);
            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                bfs.Add(u);
                foreach (var v in Adj(u))
                {
                    if (_vertices[v].Color == Color.White)
                    {
                        _vertices[v].Color = Color.Gray;
                        _vertices[v].D = _vertices[u].D + 1;
                        _vertices[v].Parent = _vertices[u];

                        queue.Enqueue(v);
                    }
                }
                _vertices[u].Color = Color.Black;
            }

            return bfs;
        }

        public IEnumerable<int> BfsPath(int s, int v)
        {
            Stack<int> path = new Stack<int>();

            while (v != s)
            {
                path.Push(v);
                v = _vertices[v].Parent.Val;
            }
            path.Push(s);

            return path;
        }

        private int _time;
        public List<int> Dfs()
        {
            List<int> dfs = new List<int>();

            foreach (var vertex in _vertices)
            {
                vertex.Color = Color.White;
            }

            _time = 0;
            for (int i = 0; i < V; i++)
            {
                if (_vertices[i].Color == Color.White)
                {
                    DfsVisit1(i, dfs);
                }
            }

            return dfs;
        }

        public void DfsVisit(int u, List<int> dfs)
        {
            dfs.Add(u);
            _vertices[u].Color = Color.Gray;
            _vertices[u].D = ++_time;
            foreach (var v in Adj(u))
            {
                if (_vertices[v].Color == Color.White)
                {
                    DfsVisit(v, dfs);
                }
            }
            _vertices[u].F = ++_time;
            _vertices[u].Color = Color.Black;
        }

        public void DfsVisit1(int w, List<int> dfs)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(w);

            while (stack.Count > 0)
            {
                int u = stack.Peek();
                if (_vertices[u].Color == Color.White)
                {
                    dfs.Add(u);
                }
                _vertices[u].Color = Color.Gray;
                _vertices[u].D = ++_time;

                bool hasWhite = false;
                foreach (var v in WhiteAdj(u))
                {
                    hasWhite = true;
                    stack.Push(v);
                    break;
                }

                if (!hasWhite)
                {
                    _vertices[u].F = ++_time;
                    _vertices[u].Color = Color.Black;
                    stack.Pop();
                }
            }
        }

        public List<int> TopologicalSort()
        {
            Stack<int> topologicals = new Stack<int>();

            foreach (var vertex in _vertices)
            {
                vertex.Color = Color.White;
            }

            _time = 0;
            for (int i = 0; i < V; i++)
            {
                if (_vertices[i].Color == Color.White)
                {
                    Stack<int> stack = new Stack<int>();
                    stack.Push(i);

                    while (stack.Count > 0)
                    {
                        int u = stack.Peek();
                        _vertices[u].Color = Color.Gray;
                        _vertices[u].D = ++_time;

                        bool hasWhite = false;
                        foreach (var v in WhiteAdj(u))
                        {
                            hasWhite = true;
                            stack.Push(v);
                            break;
                        }

                        if (!hasWhite)
                        {
                            _vertices[u].F = ++_time;
                            _vertices[u].Color = Color.Black;
                            topologicals.Push(u);
                            stack.Pop();
                        }
                    }
                }
            }

            return topologicals.ToList();
        }

        public DigraphBase Deduplicate()
        {
            DigraphBase g = new AdjListDigraph(V);
            for (int i = 0; i < V; i++)
            {
                HashSet<int> hashset = new HashSet<int>();
                ICollection<int> adj = Adj(i);
                foreach (int j in adj)
                {
                    if (j != i && !hashset.Contains(j))
                    {
                        g.AddEdge(i, j);
                        hashset.Add(j);
                    }
                }
            }
            return g;
        }

        public DigraphBase Square()
        {
            DigraphBase g = new AdjListDigraph(V);
            for (int i = 0; i < V; i++)
            {
                foreach (var j in Adj(i))
                {
                    g.AddEdge(i, j);
                    foreach (var k in Adj(j))
                    {
                        g.AddEdge(i, k);
                    }
                }
            }
            return g;
        }

        public DigraphBase Transpose()
        {
            DigraphBase g = new AdjListDigraph(V);
            for (int i = 0; i < V; i++)
            {
                ICollection<int> adj = Adj(i);
                foreach (var j in adj)
                {
                    g.AddEdge(j, i);
                }
            }
            return g;
        }

        public List<int> UniversalSink()
        {
            List<int> universalSink = new List<int>();

            for (int i = 0; i < V; i++)
            {
                if (Adj(i).Count == 0)
                {
                    int indegree = 0;
                    for (int j = 0; j < V; j++)
                    {
                        if (Adj(j).Contains(i))
                        {
                            indegree++;
                        }
                    }
                    if (indegree == V - 1)
                    {
                        universalSink.Add(i);
                    }
                }
            }

            return universalSink;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < V; i++)
            {
                ICollection<int> adj = Adj(i);
                builder.AppendLine($"{i} -> {string.Join(',', adj)}");
            }
            return builder.ToString();
        }
    }
}