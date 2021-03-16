using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;

namespace GraphResearch
{
    class Program
    {
        static void Main(string[] args)
        {
            // RunUndirected();
            RunDirected();
            // RunMst();
            //RunSp();
        }

        private static void RunUndirected()
        {
            UndirectedSampleClient sampleClient = new UndirectedSampleClient();
            //sampleClient.RunSearch("tinyG.txt");
            //sampleClient.RunPath("sample.txt");
            //sampleClient.RunCC("tinyG.txt");
            sampleClient.RunCycle("cycle.txt");
            //sampleClient.RunTwoColor("cycle.txt");
            //sampleClient.RunSymbolGraph("routes.txt", ' ');
            // sampleClient.RunDegreeOfSeperation("routes.txt", ' ', "JFK");

            // UndirectedPerfClient perfClient = new UndirectedPerfClient();
            // perfClient.Run("mediumG.txt");
        }

        private static void RunDirected()
        {
            DirectedSampleClient sampleClient = new DirectedSampleClient();
            //sampleClient.RunSearch("tinyDG.txt", new int[] { 1,2,6 });
            sampleClient.RunCycle("sampleDG1.txt");
            //sampleClient.RunCycle("tinyDG.txt");
            //sampleClient.RunTopological("tinyDG.txt");
            //sampleClient.RunTopological("tinyDAG.txt");
            //sampleClient.RunScc("scc.txt");
            // sampleClient.RunTransitiveClosure("tinyDG.txt", 6, 0);
        }

        private static void RunMst()
        {
            MstSampleClient client = new MstSampleClient();
            // client.RunMst("tinyEWG.txt");

            MstPerfClient perfClient = new MstPerfClient();
            // perfClient.Run("tinyEWG.txt");
            // perfClient.Run("mediumEWG.txt");
            perfClient.Run("http://algs4.cs.princeton.edu/43mst/largeEWG.txt");
        }

        private static void RunSp()
        {
            SpSampleClient sampleClient = new SpSampleClient();
            sampleClient.RunSp();
        }
    }

    public class SpSampleClient
    {
        public void RunSp()
        {
            EdgeWeightedDigraph G = new EdgeWeightedDigraph("tinyEWD.txt");
            int s = 0;
            SP sp = Factory.GetSp(G, s);
            for (int t = 0; t < G.V(); t++)
            {
                Console.Write(s + " to " + t);
                Console.Write(" ({0:F2}): ", sp.DistTo(t));
                if (sp.HasPathTo(t))
                {
                    foreach (DirectedEdge e in sp.PathTo(t))
                    {
                        Console.Write(e + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }

    public class MstSampleClient
    {
        public void RunMst(string path)
        {
            EdgeWeightedGraph G = new EdgeWeightedGraph(path);
            MST mst = Factory.GetMST(G);
            foreach (var e in mst.Edges())
            {
                Console.WriteLine(e);
            }
            Console.WriteLine(mst.Weight());
        }
    }

    public class DirectedSampleClient
    {
        public void RunSearch(string path, IEnumerable<int> sources)
        {
            Digraph G = new Digraph(path);
            DirectedDFS dfs = new DirectedDFS(G, sources);
            for (int v = 0; v < G.V(); v++)
            {
                if (dfs.Marked(v))
                {
                    Console.Write("{0} ", v);
                }
            }
            Console.WriteLine();
        }

        public void RunCycle(string path)
        {
            Digraph G = new Digraph(path);
            DirectedCycle c = new DirectedCycle(G);
            if (c.HasCycle())
            {
                Console.WriteLine(string.Join("->", c.Cycle()));
            }
            else
            {
                Console.WriteLine("No cycle");
            }
        }

        public void RunTopological(string path)
        {
            Digraph G = new Digraph(path);
            Topological topo = new Topological(G);
            if (topo.IsDAG())
            {
                Console.WriteLine(string.Join(",", topo.Order()));
            }
            else
            {
                Console.WriteLine("Not DAG");
            }
        }

        public void RunScc(string path)
        {
            Digraph G = new Digraph(path);
            SCC scc = new KosarajuSCC(G);
            Bag<int>[] components = new Bag<int>[scc.Count()];
            for (int i = 0; i < scc.Count(); i++)
            {
                components[i] = new Bag<int>();
            }
            for (int i = 0; i < G.V(); i++)
            {
                components[scc.Id(i)].Add(i);
            }
            for (int i = 0; i < scc.Count(); i++)
            {
                Console.WriteLine(string.Join(",", components[i]));
            }
        }

        public void RunTransitiveClosure(string path, int v, int w)
        {
            Digraph G = new Digraph(path);
            TransitiveClosure closure = new TransitiveClosure(G);
            if (closure.Reachable(v, w))
            {
                Console.WriteLine("reach");
            }
            else
            {
                Console.WriteLine("not reach");
            }
        }
    }

    public class UndirectedSampleClient
    {
        public void RunSearch(string path)
        {
            Graph G = Factory.GetGraph(path);
            //for (int s = 0; s < G.V(); s++)
            {
                int s = 2;
                Console.Write("{0}: ", s);
                Search search = Factory.GetSearch(G, s);
                for (int v = 0; v < G.V(); v++)
                {
                    if (search.Marked(v))
                    {
                        Console.Write("{0} ", v);
                    }
                }
                Console.WriteLine();

                if (search.Count() != G.V())
                {
                    Console.Write("Not ");
                }
                Console.WriteLine("Connected");
            }
        }

        public void RunPath(string path)
        {
            Graph G = Factory.GetGraph(path);
            int s = 0;
            Paths search = Factory.GetPaths(G, s);
            for (int v = 0; v < G.V(); v++)
            {
                Console.Write(s + " to " + v + ": ");

                if (search.HasPathTo(v))
                {
                    foreach (int x in search.PathTo(v))
                    {
                        if (x == s)
                        {
                            Console.Write(x);
                        }
                        else
                        {
                            Console.Write("-" + x);
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        public void RunCC(string path)
        {
            Graph G = Factory.GetGraph(path);
            CC cc = Factory.GetCC(G);
            int M = cc.Count();
            Console.WriteLine(M + " components");
            Bag<int>[] components = new Bag<int>[M];
            for (int i = 0; i < M; i++)
            {
                components[i] = new Bag<int>();
            }

            for (int v = 0; v < G.V(); v++)
            {
                components[cc.Id(v)].Add(v);
            }

            for (int i = 0; i < M; i++)
            {
                foreach (int v in components[i])
                {
                    Console.Write(v + " ");
                }
                Console.WriteLine();
            }
        }

        public void RunCycle(string path)
        {
            Graph G = Factory.GetGraph(path);
            Cycle c = Factory.GetCycle(G);
            Console.WriteLine(c.HasCycle());
        }

        public void RunTwoColor(string path)
        {
            Graph G = Factory.GetGraph(path);
            TwoColor c = Factory.GetTwoColor(G);
            Console.WriteLine(c.IsBipartite());
        }

        public void RunSymbolGraph(string filename, char delim)
        {
            SymbolGraph sg = Factory.GetSymbolGraph(filename, delim);
            Graph G = sg.G();
            string source;
            while (!string.IsNullOrEmpty(source = Console.ReadLine()))
            {
                foreach (int w in G.Adj(sg.Index(source)))
                {
                    Console.WriteLine(" " + sg.Name(w));
                }
            }
        }

        public void RunDegreeOfSeperation(string filename, char delim, string source)
        {
            SymbolGraph sg = new SymbolGraph(filename, delim);
            Graph G = sg.G();
            int s = sg.Index(source);
            BreadthFirstPaths paths = new BreadthFirstPaths(G, s);
            string target;
            while (!string.IsNullOrEmpty(target = Console.ReadLine()))
            {
                if (sg.Contains(target))
                {
                    int t = sg.Index(target);
                    if (paths.HasPathTo(t))
                    {
                        var path = paths.PathTo(t);
                        foreach (var v in path)
                        {
                            var vertice = sg.Name(v);
                            Console.WriteLine(" {0}", vertice);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Not Connected");
                    }
                }
                else
                {
                    Console.WriteLine("Not in database");
                }
            }
        }
    }

    public class MstPerfClient
    {
        public void Run(string path)
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromHours(1);
            using (var stream = client.GetAsync(path).Result.Content.ReadAsStreamAsync().Result)
            {
                Stopwatch watch1 = Stopwatch.StartNew();
                EdgeWeightedGraph G = new EdgeWeightedGraph(stream);
                Console.WriteLine(watch1.Elapsed);

                Stopwatch watch = Stopwatch.StartNew();
                MST mst = Factory.GetMST(G);
                Console.WriteLine(watch.Elapsed);
            }
        }
    }

    public class UndirectedPerfClient
    {
        public void Run(string path)
        {
            Graph G = Factory.GetGraph(path);
            Stopwatch watch = Stopwatch.StartNew();
            for (int s = 0; s < G.V(); s++)
            {
                //Console.Write("{0}: ", s);
                Search search = Factory.GetSearch(G, s);
                for (int v = 0; v < G.V(); v++)
                {
                    if (search.Marked(v))
                    {
                        //Console.Write("{0} ", v);
                    }
                }
                //Console.WriteLine();

                if (search.Count() != G.V())
                {
                    //Console.Write("Not ");
                }
                //Console.WriteLine("Connected");
            }
            Console.WriteLine(watch.Elapsed);
        }
    }

    public class Factory
    {
        public static Graph GetGraph(string path)
        {
            Graph G;
            //G = new AdjacencyMatricGraph(path);
            //G = new EdgeArrayGraph(path);
            G = new AdjacencyListGraph(path);
            return G;
        }

        public static Search GetSearch(Graph G, int s)
        {
            Search search;
            //search = new NormalSearch(G, s);
            search = new DepthFirstSearch(G, s);
            return search;
        }

        public static Paths GetPaths(Graph G, int s)
        {
            Paths p;
            //p = new DepthFirstPaths(G, s);
            p = new BreadthFirstPaths(G, s);
            return p;
        }

        public static CC GetCC(Graph G)
        {
            CC c;
            c = new DepthFirstCC(G);
            return c;
        }

        public static Cycle GetCycle(Graph G)
        {
            Cycle c;
            c = new DepthFirstCycle(G);
            return c;
        }

        public static TwoColor GetTwoColor(Graph G)
        {
            TwoColor t;
            t = new DepthFirstTwoColor(G);
            return t;
        }

        public static SymbolGraph GetSymbolGraph(string filename, char delim)
        {
            SymbolGraph sg = new SymbolGraph(filename, delim);
            return sg;
        }

        public static MST GetMST(EdgeWeightedGraph G)
        {
            MST mst;
            // mst = new LazyPrimMST(G);
            // mst = new PrimMST(G);
            // mst = new SimplePrimMST(G);
            mst = new KruskalMST(G);
            return mst;
        }

        public static SP GetSp(EdgeWeightedDigraph G, int s)
        {
            SP sp;
            sp = new DijkstraSP(G, s);
            return sp;
        }
    }
}