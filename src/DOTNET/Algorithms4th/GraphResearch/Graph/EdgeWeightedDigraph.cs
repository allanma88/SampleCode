using System;
using System.Collections.Generic;
using System.IO;

namespace GraphResearch
{
    public class EdgeWeightedDigraph
    {
        private Bag<DirectedEdge>[] _adjacencies;

        private int _v;

        private int _e;

        public EdgeWeightedDigraph(int V)
        {
            _v = V;
            BuildGraph(_v);
        }

        public EdgeWeightedDigraph(string path)
        {
             using (Stream stream = new FileStream("Resources/" + path, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                var lines = content.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                int v;
                if (int.TryParse(lines[0], out v))
                {
                    _v = v;
                }
                BuildGraph(_v);
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
                        var vertices = line.Split(' ');
                        int v1, v2;
                        double weight;
                        if (int.TryParse(vertices[0], out v1) && int.TryParse(vertices[1], out v2) && double.TryParse(vertices[2], out weight))
                        {
                            DirectedEdge edge = new DirectedEdge(v1, v2, weight);
                            AddEdge(edge);
                        }
                    }
                }
            }
        }

        public int V()
        {
            return _v;
        }

        public int E()
        {
            return _e;
        }

        public  void AddEdge(DirectedEdge e)
        {
            var from = e.From();
            _adjacencies[from].Add(e);
            _e++;
        }

        public IEnumerable<DirectedEdge> Adj(int v)
        {
            return _adjacencies[v];
        }

        public IEnumerable<DirectedEdge> Edges()
        {
            List<DirectedEdge> edges = new List<DirectedEdge>();
            for (int v = 0; v < _v; v++)
            {
                edges.AddRange(Adj(v));
            }
            return edges;
        }

        private void BuildGraph(int v)
        {
            _adjacencies = new Bag<DirectedEdge>[v];
            for (int i = 0; i < v; i++)
            {
                _adjacencies[i] = new Bag<DirectedEdge>();
            }
        }
    }

    public class DirectedEdge
    {
        private int _from;

        private int _to;

        private double _weight;

        public DirectedEdge(int v, int w, double weight)
        {
            _from = v;
            _to = w;
            _weight = weight;
        }

        public double Weight()
        {
            return _weight;
        }

        public int From()
        {
            return _from;
        }

        public int To()
        {
            return _to;
        }

        public override string ToString()
        {
            return String.Format("{0:D}->{1:d} {2:F2}", _from, _to, _weight);
        }
    }
}