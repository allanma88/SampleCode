using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryTree
{
    public class BinarySearchTreeSample
    {
        public void Run()
        {
            //Node root = BuildTree();
            BinarySearchTree bst = new BinarySearchTree();

            int[] values = new int[] { 8, 10, 7, 16, 14, 11, 1, 3, 5, 13 };
            Node root = null;
            foreach (int v in values)
            {
                root = bst.Insert(root, v);
            }
            PrintTree(root, "Insert: ");

            PrintNode(bst.Search(root, 8), "Search 8: ");
            PrintNode(bst.Search(root, 9), "Search 9: ");

            // PrintTree(bst.DeleteMin(root), "DeleteMin: ");
            // PrintTree(bst.DeleteMax(root), "DeleteMax: ");
            PrintTree(bst.Delete(root, 8), "Delete 8: ");
            // PrintTree(bst.Delete(root, 12), "Delete 12: ");
        }

        private void PrintNode(Node n, string pre)
        {
            if (n == null)
            {
                Console.WriteLine("{0}null", pre);
            }
            else
            {
                Console.WriteLine("{0}{1}", pre, n.V);
            }
        }

        private void PrintTree(Node root, string pre)
        {
            LoopTraverse traverse = new LoopTraverse();
            var inValues = traverse.In(root).Select(x => x.V).ToArray();
            Console.WriteLine("{0}{1}", pre, string.Join(",", inValues));
        }

        private Node BuildTree()
        {
            Node root = new Node(10);
            root.Left = new Node(6);
            root.Right = new Node(14);
            root.Left.Left = new Node(4);
            root.Left.Right = new Node(8);
            root.Right.Left = new Node(12);
            root.Right.Right = new Node(16);
            root.Left.Left.Left = new Node(3);
            return root;
        }
    }

    public class BinarySearchTree
    {
        public Node Search(Node root, int v)
        {
            Node n = root;
            while (n != null)
            {
                if (v == n.V)
                {
                    return n;
                }
                else if (v > n.V)
                {
                    n = n.Right;
                }
                else
                {
                    n = n.Left;
                }
            }
            return null;
        }

        public Node Insert(Node root, int v)
        {
            if (root == null)
            {
                return new Node(v);
            }
            Node n = root;
            while (true)
            {
                if (v == n.V)
                {
                    n.V = v;
                    break;
                }
                else if (v > n.V)
                {
                    if (n.Right != null)
                    {
                        n = n.Right;
                    }
                    else
                    {
                        n.Right = new Node(v);
                        break;
                    }
                }
                else
                {
                    if (n.Left != null)
                    {
                        n = n.Left;
                    }
                    else
                    {
                        n.Left = new Node(v);
                        break;
                    }
                }
            }
            return root;
        }

        public Node DeleteMin(Node root)
        {
            if (root == null)
            {
                return null;
            }
            if (root.Left == null)
            {
                return root.Right;
            }
            Node n = root;
            while (n.Left != null)
            {
                if (n.Left.Left == null)
                {
                    n.Left = n.Left.Right;
                    break;
                }
                n = n.Left;
            }
            return root;
        }

        public Node DeleteMax(Node root)
        {
            if (root == null)
            {
                return null;
            }
            if (root.Right == null)
            {
                root = root.Left;
            }
            Node n = root;
            while (n.Right != null)
            {
                if (n.Right.Right == null)
                {
                    n.Right = n.Right.Left;
                    break;
                }
                n = n.Right;
            }
            return root;
        }

        public Node Delete(Node root, int v)
        {
            if (root == null) return null;
            if (root.V == v)
            {
                if (root.Right == null) return root.Left;
                if (root.Left == null) return root.Right;
            }
            Node n = root;
            Node prev = root;
            while (n != null)
            {
                if (v == n.V)
                {
                    if (n.Left == null)
                    {
                        if(prev.Left == n) {
                            prev.Left = n.Right;
                        }
                        else {
                            prev.Right = n.Right;
                        }
                    }
                    else if (n.Right == null)
                    {
                        if(prev.Left == n) {
                            prev.Left = n.Left;
                        }
                        else {
                            prev.Right = n.Left;
                        }
                    }
                    else
                    {
                        Node min = Min(n.Right);
                        n.V = min.V;
                        n.Right = DeleteMin(n.Right);
                    }
                    break;
                }
                else if (v > n.V)
                {
                    prev = n;
                    n = n.Right;
                }
                else
                {
                    prev = n;
                    n = n.Left;
                }
            }
            return root;
        }

        private Node Min(Node root)
        {
            if (root == null)
            {
                return null;
            }
            Node n = root;
            while (n.Left != null)
            {
                n = n.Left;
            }
            return n;
        }
    }
}