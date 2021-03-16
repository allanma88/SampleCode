using System;
using System.Collections.Generic;

namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            // PathsSample paths = new PathsSample();
            // paths.Run();

            // PathSample path = new PathSample();
            // path.Run();

            // BipartiteSample bipartite = new BipartiteSample();
            // bipartite.Run();
            
            // CycleSample cycle = new CycleSample();
            // cycle.Run();

            CCSample cc = new CCSample();
            cc.Run();
        }
    }
}