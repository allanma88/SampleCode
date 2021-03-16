using System;
using System.Collections.Generic;
using System.IO;

namespace GraphResearch
{
    public class Digraph
    {
        private Bag<int>[] _adjacencies;

        private int _V;

        private int _E;

        public Digraph(int V)
        {
            _V = V;
            BuildGraph(_V);
        }

        public Digraph(string path)
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
                        var line = lines[i + 2];
                        if (line.StartsWith("/"))
                        {
                            continue;
                        }
                        var vertices = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        int v1, v2;
                        if (int.TryParse(vertices[0], out v1) && int.TryParse(vertices[1], out v2))
                        {
                            AddEdge(v1, v2);
                        }
                    }
                }
            }
        }

        public int V()
        {
            return _V;
        }

        public int E()
        {
            return _E;
        }

        public void AddEdge(int v, int w)
        {
            _E++;
            _adjacencies[v].Add(w);
        }

        public IEnumerable<int> Adj(int v)
        {
            return _adjacencies[v];
        }

        public Digraph Reverse()
        {
            Digraph d = new Digraph(_V);
            for (int v = 0; v < _V; v++)
            {
                foreach (var w in Adj(v))
                {
                    d.AddEdge(w, v);
                }
            }
            return d;
        }

        private void BuildGraph(int v)
        {
            _adjacencies = new Bag<int>[v];
            for (int i = 0; i < v; i++)
            {
                _adjacencies[i] = new Bag<int>();
            }
        }
    }
}