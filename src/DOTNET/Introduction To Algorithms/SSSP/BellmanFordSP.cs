namespace SSSP
{
    public class BellmanFordSP : SP
    {
        public bool BellmanFord(Digraph g, int s)
        {
            Initialize(g, s);
         
            for (int i = 0; i < g.V.Length - 1; i++)
            {
                foreach (var u in g.V)
                {
                    foreach (var v in g.Adj(u))
                    {
                        Relax(g, u, v);
                    }
                }
            }

            foreach (var u in g.V)
            {
                foreach (var v in g.Adj(u))
                {
                    if (g.D[v] > g.D[u] + g.W(u, v))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}