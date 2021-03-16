using System;
using System.Collections.Generic;

namespace Digraph
{
    public class Digraph
    {
        private List<int>[] _adjacencyList;
        private List<int> _vertexes;

        public Digraph(List<int> vertexes)
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

        public Digraph Reverse()
        {
            Digraph g = new Digraph(_vertexes);
            foreach (int v in _vertexes)
            {
                foreach (int w in Adj(v))
                {
                    g.AddEdge(w, v);
                }
            }
            return g;
        }
    }
}