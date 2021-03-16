using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Graph
{
    public class Digraph1
    {
        private int _v;
        private int _e;
        private bool[] _marked;
        private List<int>[] _adjacencies;

        public Digraph1(int v)
        {
            _v = v;
            _marked = new bool[v];
            _adjacencies = new List<int>[v];
            for (int i = 0; i < v; i++)
            {
                _adjacencies[i] = new List<int>();
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

        public void AddEdge(int i, int j)
        {
            _e++;
            _adjacencies[i].Add(j);
        }

        public ICollection<int> Adj(int i)
        {
            return _adjacencies[i];
        }

        public Digraph1 Transpose()
        {
            Digraph1 g = new Digraph1(V);
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

        public List<int> Bfs(int s)
        {
            List<int> bfs = new List<int>();

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);
            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                bfs.Add(u);
                foreach (var v in Adj(u))
                {
                    if (!_marked[v])
                    {
                        _marked[v] = true;
                        queue.Enqueue(v);
                    }
                }
            }

            return bfs;
        }

        public List<int> Dfs()
        {
            List<int> dfs = new List<int>();

            for (int i = 0; i < V; i++)
            {
                if (!_marked[i])
                {
                    _marked[i] = true;
                    DfsVisit1(i, dfs);
                }
            }

            return dfs;
        }

        public void DfsVisit(int u, List<int> dfs)
        {
            dfs.Add(u);
            foreach (var v in Adj(u))
            {
                if (!_marked[v])
                {
                    _marked[v] = true;
                    DfsVisit(v, dfs);
                }
            }
        }

        public void DfsVisit1(int w, List<int> dfs)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(w);
            dfs.Add(w);

            while (stack.Count > 0)
            {
                int u = stack.Peek();

                bool hasUnmarked = false;
                foreach (var v in Adj(u))
                {
                    if (!_marked[v])
                    {
                        _marked[v] = true;
                        hasUnmarked = true;
                        dfs.Add(v);
                        stack.Push(v);
                        break;
                    }
                }

                if (!hasUnmarked)
                {
                    stack.Pop();
                }
            }
        }

        public List<int> TopologicalSort()
        {
            Stack<int> topologicals = new Stack<int>();

            for (int i = 0; i < V; i++)
            {
                if (!_marked[i])
                {
                    Stack<int> stack = new Stack<int>();
                    stack.Push(i);

                    while (stack.Count > 0)
                    {
                        int u = stack.Peek();

                        bool hasUnmarked = false;
                        foreach (var v in Adj(u))
                        {
                            if (!_marked[v])
                            {
                                _marked[v] = true;
                                hasUnmarked = true;
                                stack.Push(v);
                                break;
                            }
                        }

                        if (!hasUnmarked)
                        {
                            topologicals.Push(u);
                            stack.Pop();
                        }
                    }
                }
            }

            return topologicals.ToList();
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