using System.Collections.Generic;

namespace Graph
{
    public class AdjMatrixDigraph : DigraphBase
    {
        private byte[,] _matrix;

        public AdjMatrixDigraph(int v) : base(v)
        {
            _matrix = new byte[v, v];
        }

        public override void AddEdge(int i, int j)
        {
            base.AddEdge(i, j);
            _matrix[i, j] = 1;
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

        public bool Exist1(int i, int j)
        {
            return _matrix[i, j] == 1;
        }

        public AdjMatrixDigraph Transpose1()
        {
            AdjMatrixDigraph g = new AdjMatrixDigraph(V);
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    if (_matrix[i, j] == 1)
                    {
                        g.AddEdge(j, i);
                    }
                }
            }
            return g;
        }

        public AdjMatrixDigraph Square1()
        {
            AdjMatrixDigraph g = new AdjMatrixDigraph(V);
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    if (_matrix[i, j] == 1)
                    {
                        g.AddEdge(i, j);
                        for (int k = 0; k < V; k++)
                        {
                            if (_matrix[j, k] == 1)
                            {
                                g.AddEdge(i, k);
                            }
                        }
                    }
                }
            }
            return g;
        }

        public List<int> UniversalSink1()
        {
            List<int> universalSink = new List<int>();

            for (int i = 0; i < V; i++)
            {
                bool universal = true;

                for (int j = 0; j < V; j++)
                {
                    if (_matrix[i, j] != 0)
                    {
                        universal = false;
                        break;
                    }
                }

                if (universal)
                {
                    for (int j = 0; j < V; j++)
                    {
                        if (_matrix[j, i] != 1)
                        {
                            universal = false;
                            break;
                        }
                    }
                }

                if (universal)
                {
                    universalSink.Add(i);
                }
            }

            return universalSink;
        }
    }
}