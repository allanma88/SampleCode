using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSSP
{
    public class Digraph
    {
        private int[] _v;
        private int _e;
        private bool[] _marked;
        private int[,] _matrix;
        private int[] _d;
        private int[] _pi;

        public Digraph(int v)
        {
            _v = new int[v];
            for (int i = 0; i < v; i++)
            {
                _v[i] = i;
            }
            _marked = new bool[v];
            _d = new int[v];
            _pi = new int[v];
            _matrix = new int[v, v];
        }

        public int[] V
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

        public int[] D
        {
            get { return _d; }
        }

        public int[] Pi
        {
            get { return _pi; }
        }

        public void AddEdge(int u, int v, int w)
        {
            _e++;
            _matrix[u, v] = w;
        }

        public ICollection<int> Adj(int u)
        {
            List<int> adj = new List<int>();
            foreach (int v in V)
            {
                if (u != v && _matrix[u, v] != 0)
                {
                    adj.Add(v);
                }
            }
            return adj;
        }

        public int W(int u, int v)
        {
            return _matrix[u, v];
        }

        public List<int> TopologicalSort()
        {
            Stack<int> topologicals = new Stack<int>();

            foreach (int i in V)
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
            for (int i = 0; i < V.Length; i++)
            {
                ICollection<int> adj = Adj(i);
                builder.AppendLine($"{i} -> {string.Join(',', adj)}");
            }
            return builder.ToString();
        }
    }
}