using System.Collections.Generic;

namespace SSSP
{
    public class DAGSP : SP
    {
        public void DAG(Digraph g, int s)
        {
            Initialize(g, s);
            List<int> V = g.TopologicalSort();
            foreach (int u in V)
            {
                foreach (int v in g.Adj(u))
                {
                    Relax(g, u, v);
                }
            }
        }
    }
}