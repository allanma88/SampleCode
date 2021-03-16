using System;
using System.Collections.Generic;
using System.IO;

namespace GraphResearch
{
    public abstract class Graph
    {
        protected int _V;

        protected int _E;

        public Graph(int V)
        {
            _V = V;
            BuildGraph(_V);
        }

        public Graph(string path)
        {
            using (Stream stream = new FileStream("Resources/" + path, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                var lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                int v;
                if (int.TryParse(lines[0], out v))
                {
                    _V = v;
                }
                BuildGraph(_V);
                int e;
                if (int.TryParse(lines[1], out e))
                {
                    for (int i = 0; i < e; i++)
                    {
                        var line = lines[i+2];
                        if (line.StartsWith("/"))
                        {
                            continue;
                        }
                        var vertices = line.Split(' ');
                        int v1, v2;
                        if (int.TryParse(vertices[0], out v1) && int.TryParse(vertices[1], out v2))
                        {
                            AddEdge(v1, v2);
                        }
                    }
                }
            }
        }

        public virtual int V()
        {
            return _V;
        }

        public virtual int E()
        {
            return _E;
        }

        public virtual void AddEdge(int v, int w)
        {
            _E++;
        }

        public abstract IEnumerable<int> Adj(int v);

        protected abstract void BuildGraph(int v);

        public static int Degree(Graph G, int v)
        {
            int degree = 0;
            foreach (int w in G.Adj(v))
            {
                degree++;
            }
            return degree;
        }

        public static int MaxDegree(Graph G)
        {
            int max = 0;
            for (int v = 0; v < G.V(); v++)
            {
                if (Degree(G, v) > max)
                {
                    max = Degree(G, v);
                }
            }

            return max;
        }

        public static int NumberOfSelfLoops(Graph G)
        {
            int count = 0;
            for (int v = 0; v < G.V(); v++)
            {
                foreach (int w in G.Adj(v))
                {
                    if (v == w) count++;
                }
            }

            return count / 2;
        }

        public override string ToString()
        {
            string s = V() + " vertices, " + E() + " edges\n";
            for (int v = 0; v < V(); v++)
            {
                s += v + ": ";
                foreach (int w in this.Adj(v))
                    s += w + " ";
                s += "\n";
            }
            return s;
        }
    }
}