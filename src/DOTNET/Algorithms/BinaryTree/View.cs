using System;
using System.Collections.Generic;

namespace BinaryTree
{
    public class ViewSample
    {
        public void Run()
        {
            View topView = new View();
            Node root = BuildTree();
            Print("Top", topView.Top(root));
            Print("Right", topView.Right(root));
        }

        private void Print(string direction, List<Node> nodes)
        {
            Console.Write(direction + ": ");
            foreach (var node in nodes)
            {
                Console.Write(node.V + " ");
            }
            Console.WriteLine();
        }

        private Node BuildTree()
        {
            // Node root = new Node(1);
            // root.Left = new Node(2);
            // root.Right = new Node(3);
            // root.Left.Right = new Node(4);
            // root.Left.Right.Right = new Node(5);
            // root.Left.Right.Right.Right = new Node(6);

            // Node root = new Node(1);
            // root.Left = new Node(2);
            // root.Right = new Node(3);
            // root.Left.Left = new Node(4);
            // root.Left.Right = new Node(5);
            // root.Right.Left = new Node(6);
            // root.Right.Right = new Node(7);

            Node root = new Node(1);
            root.Left = new Node(2);
            root.Right = new Node(3);
            root.Left.Left = new Node(4);
            root.Left.Left.Left = new Node(5);
            root.Left.Left.Right = new Node(7);
            root.Left.Left.Left.Left = new Node(6);
            root.Left.Left.Left.Right = new Node(8);

            return root;
        }
    }

    public class View
    {
        public List<Node> Top(Node root)
        {
            List<Node> nodes = new List<Node>();
            nodes.Add(root);
            int left = 0, right = 0;
            Queue<Node> row = new Queue<Node>();
            if (root.Left != null)
            {
                root.Left.Col = root.Col - 1;
                row.Enqueue(root.Left);
            }
            if (root.Right != null)
            {
                root.Right.Col = root.Col + 1;
                row.Enqueue(root.Right);
            }
            while (row.Count > 0)
            {
                Queue<Node> nextRow = new Queue<Node>();
                int i = 0;
                int count = row.Count;
                while (row.Count > 0)
                {
                    var node = row.Dequeue();
                    if (node.Col < left || node.Col > right)
                    {
                        nodes.Add(node);
                    }
                    if (i == 0 && node.Col < left)
                    {
                        left = node.Col;
                    }
                    if (i == count - 1 && node.Col > right)
                    {
                        right = node.Col;
                    }
                    if (node.Left != null)
                    {
                        node.Left.Col = node.Col - 1;
                        nextRow.Enqueue(node.Left);
                    }
                    if (node.Right != null)
                    {
                        node.Right.Col = node.Col + 1;
                        nextRow.Enqueue(node.Right);
                    }
                    i++;
                }
                row = nextRow;
            }
            return nodes;
        }

        public List<Node> Right(Node root)
        {
            if (root == null)
            {
                return new List<Node>();
            }
            List<Node> rights = new List<Node>();
            List<Node> row = new List<Node>();
            row.Add(root);
            while (row.Count > 0)
            {
                rights.Add(row[row.Count - 1]);
                List<Node> newRow = new List<Node>();
                foreach (var node in row)
                {
                    if (node.Left != null) newRow.Add(node.Left);
                    if (node.Right != null) newRow.Add(node.Right);
                }
                row = newRow;
            }
            return rights;
        }
    }
}