using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTree
{
    public class PathsSample
    {
        public void Run()
        {
            Paths p = new Paths();
            Node root = BuildTree();
            Console.WriteLine("All paths: ");
            var paths = p.FindPaths(root);
            foreach (var path in paths)
            {
                Console.WriteLine(string.Join("->", path.Select(x => x.V)));
            }
            Console.WriteLine("Shortest paths: ");
            var shortestPaths = p.FindShortestPaths(root);
            foreach (var path in shortestPaths)
            {
                Console.WriteLine(string.Join("->", path.Select(x => x.V)));
            }
            Console.WriteLine("Shortest path: ");
            var shortestPath = p.FindShortestPath(root);
            Console.WriteLine(string.Join("->", shortestPath.Select(x => x.V)));
        }

        private Node BuildTree()
        {
            Node root = new Node(1);
            root.Left = new Node(2);
            root.Right = new Node(3);
            root.Left.Left = new Node(4);
            root.Left.Left.Left = new Node(5);
            root.Left.Left.Right = new Node(7);
            root.Left.Left.Left.Left = new Node(6);
            root.Left.Left.Left.Right = new Node(8);
            root.Right.Left = new Node(9);
            root.Right.Left.Left = new Node(10);
            return root;
        }
    }

    public class Paths
    {
        public List<List<Node>> FindPaths(Node root)
        {
            List<List<Node>> paths = new List<List<Node>>();
            List<Node> row = new List<Node>();
            row.Add(root);
            Dictionary<Node, List<Node>> dic = new Dictionary<Node, List<Node>>();
            dic.Add(root, new List<Node>() { root });
            while (row.Count > 0)
            {
                List<Node> newRow = new List<Node>();
                foreach (var n in row)
                {
                    if (n.Left == null && n.Right == null)
                    {
                        paths.Add(dic[n]);
                    }
                    if (n.Left != null)
                    {
                        dic.Add(n.Left, new List<Node>(dic[n]) { n.Left });
                        newRow.Add(n.Left);
                    }
                    if (n.Right != null)
                    {
                        dic.Add(n.Right, new List<Node>(dic[n]) { n.Right });
                        newRow.Add(n.Right);
                    }
                }
                row = newRow;
            }
            return paths;
        }

        public List<Node> FindShortestPath(Node root)
        {
            if (root == null) return new List<Node>();
            List<Node> row = new List<Node>();
            row.Add(root);
            Dictionary<Node, List<Node>> dic = new Dictionary<Node, List<Node>>();
            dic.Add(root, new List<Node>() { root });
            while (row.Count > 0)
            {
                List<Node> newRow = new List<Node>();
                foreach (var n in row)
                {
                    if (n.Left == null && n.Right == null)
                    {
                        return dic[n];
                    }
                    if (n.Left != null)
                    {
                        dic.Add(n.Left, new List<Node>(dic[n]) { n.Left });
                        newRow.Add(n.Left);
                    }
                    if (n.Right != null)
                    {
                        dic.Add(n.Right, new List<Node>(dic[n]) { n.Right });
                        newRow.Add(n.Right);
                    }
                }
                row = newRow;
            }
            throw new InvalidOperationException();
        }

        public List<List<Node>> FindShortestPaths(Node root)
        {
            if (root == null) return new List<List<Node>>();
            List<List<Node>> shortestPaths = new List<List<Node>>();
            List<Node> row = new List<Node>();
            row.Add(root);
            Dictionary<Node, List<Node>> dic = new Dictionary<Node, List<Node>>();
            dic.Add(root, new List<Node>() { root });
            while (row.Count > 0)
            {
                List<Node> newRow = new List<Node>();
                foreach (var n in row)
                {
                    if (n.Left == null && n.Right == null)
                    {
                        shortestPaths.Add(dic[n]);
                    }
                    if (n.Left != null)
                    {
                        dic.Add(n.Left, new List<Node>(dic[n]) { n.Left });
                        newRow.Add(n.Left);
                    }
                    if (n.Right != null)
                    {
                        dic.Add(n.Right, new List<Node>(dic[n]) { n.Right });
                        newRow.Add(n.Right);
                    }
                }
                if (shortestPaths.Count > 0)
                {
                    return shortestPaths;
                }
                row = newRow;
            }
            throw new InvalidOperationException();
        }
    }
}