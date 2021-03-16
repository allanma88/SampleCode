using System;
using System.Collections.Generic;

namespace SSSP
{
    public class DijkstraSP : SP
    {
        public void Dijkstra(Digraph g, int s)
        {
            Initialize(g, s);

            List<int> S = new List<int>();
            MinHeap heap = new MinHeap(g.V, g.D);

            while (!heap.IsEmpty)
            {
                int u = heap.ExtractMin();
                S.Add(u);
                foreach (var v in g.Adj(u))
                {
                    if (Relax(g, u, v))
                    {
                        heap.Decrease(v, g.D[v]);
                    }
                }
            }
        }
    }
}