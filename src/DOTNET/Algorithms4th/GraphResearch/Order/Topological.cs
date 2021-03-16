using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace GraphResearch
{
    public class Topological
    {
        private IEnumerable<int> _order;

        public Topological(Digraph G)
        {
            DirectedCycle cycle = new DirectedCycle(G);
            if (!cycle.HasCycle())
            {
                DepthFirstOrder dfo = new DepthFirstOrder(G);
                _order = dfo.ReversePost();
            }
            else
            {
                Console.WriteLine(string.Join(",", cycle.Cycle()));
            }
        }

        public bool IsDAG()
        {
            return _order != null;
        }

        public IEnumerable<int> Order()
        {
            return _order;
        }
    }
}
