using System;
using System.Collections.Generic;
using System.IO;

namespace GraphResearch
{
    public class EdgeWeightedGraph
    {
        private int _v;

        private int _e;

        private Bag<Edge>[] _adjacencies;

        public EdgeWeightedGraph(int V)
        {
            _v = V;
            BuildGraph(_v);
        }

        public EdgeWeightedGraph(string path)
        {
            using (Stream stream = new FileStream("Resources/" + path, FileMode.Open))
            {
                Init(stream);
            }
        }

        public EdgeWeightedGraph(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
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
                            Edge edge = new Edge(v1, v2, weight);
                            AddEdge(edge);
                        }
                    }
                }
            }
        }

        private void BuildGraph(int v)
        {
            _adjacencies = new Bag<Edge>[v];
            for (int i = 0; i < v; i++)
            {
                _adjacencies[i] = new Bag<Edge>();
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

        public void AddEdge(Edge e)
        {
            int v = e.Either();
            int w = e.Other(v);
            _adjacencies[v].Add(e);
            _adjacencies[w].Add(e);
            _e++;
        }

        public IEnumerable<Edge> Adj(int v)
        {
            return _adjacencies[v];
        }

        public IEnumerable<Edge> Edges()
        {
            Bag<Edge> edges = new Bag<Edge>();
            for (int v = 0; v < _v; v++)
            {
                foreach (var e in Adj(v))
                {
                    if (e.Other(v) > v)
                    {
                        edges.Add(e);
                    }
                }
            }
            return edges;
        }
    }

    public class Edge : IComparable<Edge>
    {
        private int _v;

        private int _w;

        private double _weight;

        public Edge(int v, int w, double weight)
        {
            _v = v;
            _w = w;
            _weight = weight;
        }

        public double Weight()
        {
            return _weight;
        }

        public int Either()
        {
            return _v;
        }

        public int Other(int v)
        {
            if (v == _v)
            {
                return _w;
            }
            else if (v == _w)
            {
                return _v;
            }
            throw new ArgumentOutOfRangeException();
        }

        public int CompareTo(Edge other)
        {
            return _weight.CompareTo(other._weight);
        }

        public override string ToString()
        {
            return string.Format("{0:D}-{1:D} {2:F2}", _v, _w, _weight);
        }
    }
}