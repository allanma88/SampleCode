using System;
using System.Collections.Generic;
using System.IO;

namespace GraphResearch
{
    public class SymbolGraph
    {
        private Graph _G;

        private Dictionary<string,int> _index = new Dictionary<string, int>();

        private string[] _keys;

        public SymbolGraph(string filename, char delim)
        {
            using (Stream stream = new FileStream(filename, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
            {
                int i = 0;
                string content = reader.ReadToEnd();
                var lines = content.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (line.StartsWith("/"))
                    {
                        continue;
                    }
                    var vertices = line.Split(delim);
                    foreach (string vertex in vertices)
                    {
                        if (!_index.ContainsKey(vertex))
                        {
                            _index.Add(vertex, i);
                            i++;
                        }
                    }
                }

                int v = _index.Values.Count;
                _G = new AdjacencyListGraph(v);
                _keys = new string[v];

                foreach (string line in lines)
                {
                    if (line.StartsWith("/"))
                    {
                        continue;
                    }
                    var vertices = line.Split(delim);
                    int v1 = _index[vertices[0]];
                    int v2 = _index[vertices[1]];
                    _keys[v1] = vertices[0];
                    _keys[v2] = vertices[1];
                    _G.AddEdge(v1, v2);
                }
            }
        }

        public bool Contains(string key)
        {
            return _index.ContainsKey(key);
        }

        public int Index(string key)
        {
            return _index[key];
        }

        public string Name(int v)
        {
            //return _revertIndex[v];
            return _keys[v];
        }

        public Graph G()
        {
            return _G;
        }
    }
}