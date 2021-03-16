namespace SSSP
{
    public class SP
    {
        public void Initialize(Digraph g, int s)
        {
            for (int v = 0; v < g.V.Length; v++)
            {
                g.D[v] = -1;
                g.Pi[v] = -1;
            }
            g.D[s] = 0;
        }

        public bool Relax(Digraph g, int u, int v)
        {
            if(g.D[u] == -1) return false;
            if (g.D[v] == -1 || g.D[v] > g.D[u] + g.W(u, v))
            {
                g.D[v] = g.D[u] + g.W(u, v);
                g.Pi[v] = u;
                return true;
            }
            return false;
        }
    }
}