using System;

namespace Digraph
{
    class Program
    {
        static void Main(string[] args)
        {
            // PathSample path = new PathSample();
            // path.Run();

            // CycleSample cycle = new CycleSample();
            // cycle.Run();

            // OrderSample order = new OrderSample();
            // order.Run();

            // SCCSample scc = new SCCSample();
            // scc.Run();

            TopologicalSample topological = new TopologicalSample();
            topological.Run();
        }
    }
}