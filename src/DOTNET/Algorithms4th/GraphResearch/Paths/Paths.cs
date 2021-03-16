using System.Collections.Generic;

namespace GraphResearch
{
    public abstract class Paths
    {
        public Paths(Graph G, int s)
        {

        }

        public abstract bool HasPathTo(int v);

        public abstract IEnumerable<int> PathTo(int v);
    }
}
