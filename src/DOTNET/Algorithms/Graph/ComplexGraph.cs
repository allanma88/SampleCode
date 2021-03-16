using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public abstract class ComplexGraph
    {
        public ComplexGraph(List<Vertex> vertexes)
        {
            Vertexes = vertexes;
        }

        public List<Vertex> Vertexes { get; set; }

        public abstract void AddEdge(int sourceIndex, int targetIndex);

        public void AddEdge(string sourceName, string targetName)
        {
            int sourceIndex = Vertexes.First(x => x.Name == sourceName).Index;
            int targetIndex = Vertexes.First(x => x.Name == targetName).Index;
            AddEdge(sourceIndex, targetIndex);
        }

        public abstract List<Vertex> Adj(int vertexIndex);

        public List<Vertex> Adj(string vertexName)
        {
            int vertexIndex = Vertexes.First(x => x.Name == vertexName).Index;
            return Adj(vertexIndex);
        }
    }

    public class MatrixGraph : ComplexGraph
    {
        public bool[,] _matrix;

        public MatrixGraph(List<Vertex> vertexes) : base(vertexes)
        {
            _matrix = new bool[vertexes.Count, vertexes.Count];
        }

        public override void AddEdge(int sourceIndex, int targetIndex)
        {
            _matrix[sourceIndex, targetIndex] = true;
            _matrix[targetIndex, sourceIndex] = true;
        }

        public override List<Vertex> Adj(int vertexIndex)
        {
            List<Vertex> vertexes = new List<Vertex>();
            for (int i = 0; i < Vertexes.Count; i++)
            {
                if (_matrix[vertexIndex, i])
                {
                    Vertex v = Vertexes.First(x => x.Index == i);
                    vertexes.Add(v);
                }
            }
            return vertexes;
        }
    }

    public class AdjacencyListGraph : ComplexGraph
    {
        public List<int>[] _adjacencyList;
        public AdjacencyListGraph(List<Vertex> vertexes) : base(vertexes)
        {
            _adjacencyList = new List<int>[vertexes.Count];
            for (int i = 0; i < vertexes.Count; i++)
            {
                _adjacencyList[i] = new List<int>();
            }
        }

        public override void AddEdge(int s, int t)
        {
            _adjacencyList[s].Add(t);
            _adjacencyList[t].Add(s);
        }

        public override List<Vertex> Adj(int vertexIndex)
        {
            List<Vertex> vertexes = new List<Vertex>();
            var adjIndics = _adjacencyList[vertexIndex];
            foreach (var i in adjIndics)
            {
                var vertex = Vertexes.First(x => x.Index == i);
                vertexes.Add(vertex);
            }
            return vertexes;
        }
    }

    public class Vertex
    {
        public int Index { get; set; }

        public string Name { get; set; }
    }

    public static class GraphFactory
    {
        public static ComplexGraph BuildGraph()
        {
            List<Vertex> vertexes = new List<Vertex>();
            for (int i = 0; i <= 5; i++)
            {
                vertexes.Add(new Vertex() { Index = i, Name = $"Vertex {i + 1}" });
            }
            ComplexGraph g = new MatrixGraph(vertexes);
            g.AddEdge(0, 1);
            g.AddEdge(0, 5);
            g.AddEdge(1, 2);
            g.AddEdge(1, 4);
            g.AddEdge(2, 3);
            g.AddEdge(2, 5);
            g.AddEdge(3, 4);
            return g;
        }
    }
}