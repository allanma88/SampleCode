using System.Collections.Generic;
using System.Text;

namespace Graph
{
    public class Graph1
    {
        private int _v;
        private int _e;

        private int[,] _matrix;

        public Graph1(int v)
        {
            _v = v;
            _matrix = new int[v, v];
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
            _matrix[i, j] = 1;
            _matrix[j, i] = 1;
        }

        public ICollection<int> Adj(int i)
        {
            List<int> adj = new List<int>();
            for (int j = 0; j < V; j++)
            {
                if (_matrix[i, j] == 1)
                {
                    adj.Add(j);
                }
            }
            return adj;
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