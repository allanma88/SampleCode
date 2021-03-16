using System;
using System.Collections.Generic;
using System.Text;

namespace GraphResearch
{
    public abstract class SCC
    {
        public SCC(Digraph G)
        {

        }

        public abstract bool StronglyConnected(int v, int w);

        public abstract int Count();

        public abstract int Id(int v);
    }
}