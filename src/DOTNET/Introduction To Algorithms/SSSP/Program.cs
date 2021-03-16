using System;
using System.Collections.Generic;
using System.Text;

namespace SSSP
{
    class Program
    {
        static void Main(string[] args)
        {
            Digraph g = Make("DAG");

            // DijkstraSP dijkstra = new DijkstraSP();
            // dijkstra.Dijkstra(g, 1);

            DAGSP dag = new DAGSP();
            dag.DAG(g, 1);

            PrintSP(g, 1);
        }

        private static void PrintSP(Digraph g, int s)
        {
            foreach (int v in g.V)
            {
                Stack<int> stack = new Stack<int>();
                int w = v;
                while (w != s && w != -1)
                {
                    stack.Push(w);
                    w = g.Pi[w];
                }
                stack.Push(s);
                Console.WriteLine(string.Join("->", stack));
            }
        }

        private static Digraph Make(string mode)
        {
            Digraph g = new Digraph(5);
            if (mode == "Dijkstra")
            {
                g = new Digraph(5);
                g.AddEdge(0, 1, 10);
                g.AddEdge(0, 4, 5);
                g.AddEdge(1, 2, 1);
                g.AddEdge(1, 4, 2);
                g.AddEdge(2, 3, 4);
                g.AddEdge(3, 0, 7);
                g.AddEdge(3, 2, 6);
                g.AddEdge(4, 1, 3);
                g.AddEdge(4, 2, 9);
                g.AddEdge(4, 3, 2);
            }
            else if (mode == "Dijkstra1")
            {
                g = new Digraph(5);
                g.AddEdge(0, 1, 3);
                g.AddEdge(0, 4, 5);
                g.AddEdge(1, 2, 6);
                g.AddEdge(1, 4, 2);
                g.AddEdge(2, 3, 2);
                g.AddEdge(3, 0, 3);
                g.AddEdge(3, 2, 7);
                g.AddEdge(4, 1, 1);
                g.AddEdge(4, 2, 4);
                g.AddEdge(4, 3, 6);
            }
            else if (mode == "DAG")
            {
                g = new Digraph(6);
                g.AddEdge(0, 1, 5);
                g.AddEdge(0, 2, 3);
                g.AddEdge(1, 2, 2);
                g.AddEdge(1, 3, 6);
                g.AddEdge(2, 3, 7);
                g.AddEdge(2, 4, 4);
                g.AddEdge(2, 5, 2);
                g.AddEdge(3, 4, -1);
                g.AddEdge(3, 5, 1);
                g.AddEdge(4, 5, -2);
            }
            return g;
        }
    }
}