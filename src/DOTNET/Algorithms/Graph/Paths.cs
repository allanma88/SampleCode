using System;
using System.Collections.Generic;
using System.Linq;

namespace Graph
{
    public class PathsSample
    {
        public void Run()
        {
            var g = GraphFactory.BuildGraph();
            Paths p = new Paths();

            Console.WriteLine("All paths: ");
            var paths = p.FindAllPaths(g, 0, 3);
            foreach (var path in paths)
            {
                Console.WriteLine(string.Join("->", path.Select(x => x.Index)));
            }

            Console.WriteLine("Shortest paths: ");
            var shortestPaths = p.FindShortestPaths(g, 0, 3);
            foreach (var path in shortestPaths)
            {
                Console.WriteLine(string.Join("->", path.Select(x => x.Index)));
            }

            Console.WriteLine("Shortest path: ");
            var shortestPath = p.FindShortestPath(g, 0, 3);
            Console.WriteLine(string.Join("->", shortestPath.Select(x => x.Index)));
        }
    }

    public class Paths
    {
        public List<List<Vertex>> FindAllPaths(ComplexGraph g, int s, int t)
        {
            List<List<Vertex>> paths = new List<List<Vertex>>();
            var vertex = g.Vertexes.First(x => x.Index == s);
            Dfs(g, vertex, t, new List<Vertex>() { vertex }, paths);
            return paths;
        }

        private void Dfs(ComplexGraph g, Vertex s, int t, List<Vertex> path, List<List<Vertex>> paths)
        {
            if (s.Index == t)
            {
                paths.Add(path);
            }
            foreach (var w in g.Adj(s.Index))
            {
                if (!path.Contains(w))
                {
                    Dfs(g, w, t, new List<Vertex>(path) { w }, paths);
                }
            }
        }

        public List<List<Vertex>> FindShortestPaths(ComplexGraph g, int s, int t)
        {
            List<List<Vertex>> paths = new List<List<Vertex>>();
            Dictionary<Vertex, List<List<Vertex>>> pathDic = new Dictionary<Vertex, List<List<Vertex>>>();
            var sVertex = g.Vertexes.First(x => x.Index == s);
            pathDic[sVertex] = new List<List<Vertex>>() { new List<Vertex>() { sVertex } };
            Queue<Vertex> queue = new Queue<Vertex>();
            queue.Enqueue(sVertex);
            while (queue.Count > 0)
            {
                var v = queue.Dequeue();
                foreach (var w in g.Adj(v.Index))
                {
                    var pathList = pathDic[v];
                    List<List<Vertex>> newPathList = new List<List<Vertex>>();
                    foreach (var path in pathList)
                    {
                        newPathList.Add(new List<Vertex>(path) { w });
                    }
                    if (w.Index == t)
                    {
                        foreach (var newPath in newPathList)
                        {
                            if (paths.Count == 0)
                            {
                                paths.Add(newPath);
                            }
                            else if (paths[0].Count > newPath.Count)
                            {
                                paths = new List<List<Vertex>>() { newPath };
                            }
                            else if (paths[0].Count == newPath.Count)
                            {
                                paths.Add(newPath);
                            }
                        }
                        continue;
                    }
                    if (pathDic.ContainsKey(w))
                    {

                        pathDic[w].AddRange(newPathList);
                    }
                    else
                    {
                        pathDic[w] = newPathList;
                        queue.Enqueue(w);
                    }
                }
            }
            return paths;
        }

        public IEnumerable<Vertex> FindShortestPath(ComplexGraph g, int s, int t)
        {
            bool[] marked = new bool[g.Vertexes.Count];
            var vertex = g.Vertexes.First(x => x.Index == s);
            Queue<Vertex> queue = new Queue<Vertex>();
            queue.Enqueue(vertex);
            marked[s] = true;
            Dictionary<Vertex, Vertex> pathDic = new Dictionary<Vertex, Vertex>();
            bool find = false;
            while (queue.Count > 0)
            {
                var v = queue.Dequeue();
                foreach (var w in g.Adj(v.Index))
                {
                    if (!marked[w.Index])
                    {
                        marked[w.Index] = true;
                        pathDic[w] = v;
                        queue.Enqueue(w);
                        if (w.Index == t)
                        {
                            find = true;
                            break;
                        }
                    }
                }
                if (find)
                {
                    break;
                }
            }
            var tVertex = g.Vertexes.First(x => x.Index == t);
            Stack<Vertex> path = new Stack<Vertex>();
            path.Push(tVertex);
            while (pathDic.ContainsKey(tVertex))
            {
                tVertex = pathDic[tVertex];
                path.Push(tVertex);
            }
            return path;
        }
    }
}