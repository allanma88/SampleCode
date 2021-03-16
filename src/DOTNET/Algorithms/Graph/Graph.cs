using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class Graph
    {
        private List<int>[] _adjacencyList;
        private List<int> _vertexes;

        public Graph(List<int> vertexes)
        {
            _vertexes = vertexes;
            _adjacencyList = new List<int>[vertexes.Count];
            for (int i = 0; i < vertexes.Count; i++)
            {
                _adjacencyList[i] = new List<int>();
            }
        }

        public void AddEdge(int s, int t)
        {
            _adjacencyList[s].Add(t);
            _adjacencyList[t].Add(s);
        }

        public List<int> Adj(int vertexIndex)
        {
            return _adjacencyList[vertexIndex];
        }

        public List<int> Vertexes
        {
            get
            {
                return _vertexes;
            }
        }
    }
}