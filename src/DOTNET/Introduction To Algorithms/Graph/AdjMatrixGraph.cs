using System.Collections.Generic;

namespace Graph
{
    public class AdjMatrixGraph : GraphBase
    {
        private byte[,] _matrix;

        public AdjMatrixGraph(int v) : base(v)
        {
            _matrix = new byte[v, v];
        }

        public override void AddEdge(int i, int j)
        {
            base.AddEdge(i, j);
            _matrix[i, j] = 1;
            _matrix[j, i] = 1;
        }

        public override ICollection<int> Adj(int i)
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
    }
}