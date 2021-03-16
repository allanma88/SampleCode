using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace BinaryTree
{
    public class SymmetricTreeSample
    {
        public void Run()
        {
            SymmetricTree symmetric = new SymmetricTree();

            Node n = new Node(1);
            Console.WriteLine("IsSymmetricWithCol:{0}", symmetric.IsSymmetricWithCol(n));
            Console.WriteLine("IsSymmetricByRow:{0}", symmetric.IsSymmetricByRow(n));
            Console.WriteLine("IsSymmetricWithRecur:{0}", symmetric.IsSymmetricWithRecur(n));
            Console.WriteLine("IsSymmetricWithLoop:{0}", symmetric.IsSymmetricWithLoop(n));
            Console.WriteLine();

            n = new Node(1);
            n.Left = new Node(2);
            n.Right = new Node(2);
            n.Left.Right = new Node(3);
            n.Right.Right = new Node(3);
            Console.WriteLine("IsSymmetricWithCol:{0}", symmetric.IsSymmetricWithCol(n));
            Console.WriteLine("IsSymmetricByRow:{0}", symmetric.IsSymmetricByRow(n));
            Console.WriteLine("IsSymmetricWithRecur:{0}", symmetric.IsSymmetricWithRecur(n));
            Console.WriteLine("IsSymmetricWithLoop:{0}", symmetric.IsSymmetricWithLoop(n));
            Console.WriteLine();

            n = new Node(1);
            n.Left = new Node(2);
            n.Right = new Node(2);
            n.Left.Left = new Node(3);
            n.Right.Right = new Node(3);
            Console.WriteLine("IsSymmetricWithCol:{0}", symmetric.IsSymmetricWithCol(n));
            Console.WriteLine("IsSymmetricByRow:{0}", symmetric.IsSymmetricByRow(n));
            Console.WriteLine("IsSymmetricWithRecur:{0}", symmetric.IsSymmetricWithRecur(n));
            Console.WriteLine("IsSymmetricWithLoop:{0}", symmetric.IsSymmetricWithLoop(n));
        }
    }

    public class SymmetricTree
    {
        public bool IsSymmetricWithCol(Node root)
        {
            List<Node> row = new List<Node>();
            row.Add(root);
            while (row.Count > 0)
            {
                List<Node> newRow = new List<Node>();
                int total = 0;
                foreach (var node in row)
                {
                    total += node.Col;
                    if (node.Left != null)
                    {
                        node.Left.Col = node.Col - 1;
                        newRow.Add(node.Left);
                    }
                    if (node.Right != null)
                    {
                        node.Right.Col = node.Col + 1;
                        newRow.Add(node.Right);
                    }
                }
                if (total != 0) return false;
                row = newRow;
            }
            return true;
        }

        public bool IsSymmetricByRow(Node root)
        {
            if (root == null) return true;
            if (root.Left == null && root.Right == null) return true;
            List<Node> row = new List<Node>();
            if (root.Left != null && root.Right != null)
            {
                if (root.Left.V != root.Right.V)
                {
                    return false;
                }
                row.Add(root.Left);
                row.Add(root.Right);
            }
            else
            {
                return false;
            }

            while (row.Count > 0)
            {
                int i = 0, j = row.Count - 1;
                while (i < j)
                {
                    if (row[i].Left != null && row[j].Right != null)
                    {
                        if (row[i].Left.V != row[j].Right.V)
                        {
                            return false;
                        }
                    }
                    else if (row[i].Left == null && row[j].Right == null)
                    {
                    }
                    else
                    {
                        return false;
                    }
                    if (row[i].Right != null && row[j].Left != null)
                    {
                        if (row[i].Right.V != row[j].Left.V)
                        {
                            return false;
                        }
                    }
                    else if (row[i].Right == null && row[j].Left == null)
                    {
                    }
                    else
                    {
                        return false;
                    }
                    i++;
                    j--;
                }
                List<Node> newRow = new List<Node>();
                for (int k = 0; k < row.Count; k++)
                {
                    if (row[k].Left != null)
                    {
                        newRow.Add(row[k].Left);
                    }
                    if (row[k].Right != null)
                    {
                        newRow.Add(row[k].Right);
                    }
                }
                row = newRow;
            }
            return true;
        }

        public bool IsSymmetricWithLoop(Node root)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                var q = queue.Dequeue();
                if(p == null && q == null) continue;
                if(p == null || q == null) return false;
                if (p.V != q.V) return false;
                queue.Enqueue(p.Left);
                queue.Enqueue(q.Right);
                queue.Enqueue(p.Right);
                queue.Enqueue(q.Left);
            }
            return true;
        }

        public bool IsSymmetricWithRecur(Node root)
        {
            return IsSymmetricWithRecur(root, root);
        }

        private bool IsSymmetricWithRecur(Node p, Node q)
        {
            if (p == null && q == null) return true;
            if (p == null || q == null) return false;
            if (p.V != q.V) return false;
            return IsSymmetricWithRecur(p.Left, q.Right) && IsSymmetricWithRecur(p.Right, q.Left);
        }
    }
}