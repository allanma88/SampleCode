using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTree
{
    public class TraverseSample
    {
        public void Run()
        {
            var root = BuildTree();

            Console.WriteLine("Recursive: ");
            RecurTraverse recurTraverse = new RecurTraverse();

            IEnumerable<Node> nodes = recurTraverse.Pre(root);
            PrintNodes(nodes, "Pre");
            nodes = recurTraverse.In(root);
            PrintNodes(nodes, "In");
            nodes = recurTraverse.Post(root);
            PrintNodes(nodes, "Post");

            Console.WriteLine("Loop: ");
            LoopTraverse loopTraverse = new LoopTraverse();
            nodes = loopTraverse.PreWithSimple(root);
            PrintNodes(nodes, "PreWithSimple");
            nodes = loopTraverse.PreWithState(root);
            PrintNodes(nodes, "PreWithState");
            nodes = loopTraverse.Pre(root);
            PrintNodes(nodes, "Pre");
            nodes = loopTraverse.PreWithMorris(root);
            PrintNodes(nodes, "PreWithMorris");
            nodes = loopTraverse.InWithState(root);
            PrintNodes(nodes, "InWithState");
            nodes = loopTraverse.In(root);
            PrintNodes(nodes, "In");
            nodes = loopTraverse.InWithMorris(root);
            PrintNodes(nodes, "InWithMorris");
            nodes = loopTraverse.PostWithState(root);
            PrintNodes(nodes, "PostWithState");
            nodes = loopTraverse.Post(root);
            PrintNodes(nodes, "Post");
            nodes = loopTraverse.PostWithMorris(root);
            PrintNodes(nodes, "PostWithMorris");
            nodes = loopTraverse.Row(root);
            PrintNodes(nodes, "Row");

            var nodesList = loopTraverse.RowList(root);
            PrintNodes(nodesList, "RowList");
            nodesList = loopTraverse.ColumnList(root);
            PrintNodes(nodesList, "ColumnList");
        }

        private void PrintNodes(IEnumerable<Node> nodes, string order)
        {
            Console.Write("{0}: ", order);
            foreach (var node in nodes)
            {
                Console.Write(node.V + " ");
            }
            Console.WriteLine();
        }

        private void PrintNodes(IEnumerable<IEnumerable<Node>> nodesList, string order)
        {
            Console.Write("{0}: ", order);
            foreach (var nodes in nodesList)
            {
                foreach (var node in nodes)
                {
                    Console.Write(node.V + ",");
                }
                Console.Write(" ");
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
            root.Right.Left = new Node(9);
            root.Right.Right = new Node(10);
            root.Left.Left = new Node(4);
            root.Left.Left.Left = new Node(5);
            root.Left.Left.Right = new Node(7);
            // root.Left.Left.Left.Left = new Node(6);
            // root.Left.Left.Left.Right = new Node(8);

            return root;
        }
    }

    public class RecurTraverse
    {
        public Queue<Node> Pre(Node root)
        {
            Queue<Node> nodes = new Queue<Node>();
            Pre(root, nodes);
            return nodes;
        }

        private void Pre(Node node, Queue<Node> nodes)
        {
            if (node == null)
            {
                return;
            }
            nodes.Enqueue(node);
            Pre(node.Left, nodes);
            Pre(node.Right, nodes);
        }

        public Queue<Node> In(Node root)
        {
            Queue<Node> nodes = new Queue<Node>();
            In(root, nodes);
            return nodes;
        }

        private void In(Node node, Queue<Node> nodes)
        {
            if (node == null)
            {
                return;
            }
            In(node.Left, nodes);
            nodes.Enqueue(node);
            In(node.Right, nodes);
        }

        public Queue<Node> Post(Node root)
        {
            Queue<Node> nodes = new Queue<Node>();
            Post(root, nodes);
            return nodes;
        }

        private void Post(Node node, Queue<Node> nodes)
        {
            if (node == null)
            {
                return;
            }
            Post(node.Left, nodes);
            Post(node.Right, nodes);
            nodes.Enqueue(node);
        }
    }

    public class LoopTraverse
    {
        public IEnumerable<Node> Pre(Node root)
        {
            Stack<Node> stack = new Stack<Node>();
            Queue<Node> nodes = new Queue<Node>();
            Node current = root;
            while (stack.Count > 0 || current != null)
            {
                while (current != null)
                {
                    nodes.Enqueue(current);
                    if (current.Right != null)
                    {
                        stack.Push(current.Right);
                    }
                    current = current.Left;
                }
                if (stack.Count > 0)
                {
                    current = stack.Pop();
                }
            }
            return nodes;
        }

        public IEnumerable<Node> PreWithSimple(Node root)
        {
            Queue<Node> nodes = new Queue<Node>();
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                nodes.Enqueue(node);
                if (node.Right != null)
                {
                    stack.Push(node.Right);
                }
                if (node.Left != null)
                {
                    stack.Push(node.Left);
                }
            }
            return nodes;
        }

        public IEnumerable<Node> PreWithState(Node root)
        {
            Queue<Node> nodes = new Queue<Node>();
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var node = stack.Peek();
                if (node.State == 0)
                {
                    nodes.Enqueue(node);
                    node.State = 1;
                }
                else if (node.State == 1)
                {
                    if (node.Left != null)
                    {
                        stack.Push(node.Left);
                    }
                    node.State = 2;
                }
                else if (node.State == 2)
                {
                    if (node.Right != null)
                    {
                        stack.Push(node.Right);
                    }
                    node.State = 3;
                }
                else if (node.State == 3)
                {
                    stack.Pop();
                    node.State = 0;
                }
            }
            return nodes;
        }

        public IEnumerable<Node> PreWithMorris(Node root)
        {
            List<Node> nodes = new List<Node>();
            Node current = root;
            while (current != null)
            {
                nodes.Add(current);
                if (current.Left != null)
                {
                    if (current.Right != null)
                    {
                        Node prev = FindPrevOfPrev(current.Left);
                        prev.Right = current.Right;
                    }
                    current = current.Left;
                }
                else
                {
                    var next = current.Right;
                    current.Right = null;
                    current = next;
                }
            }
            return nodes;
        }

        private Node FindPrevOfPrev(Node n)
        {
            while (true)
            {
                if (n.Right != null)
                {
                    n = n.Right;
                }
                else if (n.Left != null)
                {
                    n = n.Left;
                }
                else
                {
                    break;
                }
            }
            return n;
        }

        public IEnumerable<Node> In(Node root)
        {
            Queue<Node> nodes = new Queue<Node>();
            Stack<Node> stack = new Stack<Node>();
            Node current = root;
            while (stack.Count > 0 || current != null)
            {
                while (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }
                current = stack.Pop();
                nodes.Enqueue(current);
                current = current.Right;
            }
            return nodes;
        }

        public IEnumerable<Node> InWithState(Node root)
        {
            Queue<Node> nodes = new Queue<Node>();
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var node = stack.Peek();
                if (node.State == 0)
                {
                    if (node.Left != null)
                    {
                        stack.Push(node.Left);
                    }
                    node.State = 1;
                }
                else if (node.State == 1)
                {
                    nodes.Enqueue(node);
                    node.State = 2;
                }
                else if (node.State == 2)
                {
                    if (node.Right != null)
                    {
                        stack.Push(node.Right);
                    }
                    node.State = 3;
                }
                else if (node.State == 3)
                {
                    stack.Pop();
                    node.State = 0;
                }
            }
            return nodes;
        }

        public IEnumerable<Node> InWithMorris(Node root)
        {
            List<Node> nodes = new List<Node>();
            Node current = root;
            while (current != null)
            {
                if (current.Left != null)
                {
                    var prev = FindPrevOfIn(current);
                    if (prev.Right == null)
                    {
                        prev.Right = current;
                        current = current.Left;
                    }
                    else
                    {
                        prev.Right = null;
                        nodes.Add(current);
                        current = current.Right;
                    }
                }
                else
                {
                    nodes.Add(current);
                    current = current.Right;
                }
            }
            return nodes;
        }

        private Node FindPrevOfIn(Node current)
        {
            Node n = current.Left;
            while (n.Right != null && n.Right != current)
            {
                n = n.Right;
            }
            return n;
        }

        public IEnumerable<Node> Post(Node root)
        {
            Queue<Node> nodes = new Queue<Node>();
            Stack<Node> stack = new Stack<Node>();
            Node current = root;
            Node prev = root;
            while (stack.Count > 0 || current != null)
            {
                while (current != null)
                {
                    stack.Push(current);
                    prev = current;
                    current = current.Left;
                }
                current = stack.Peek();
                if (current.Right == null || current.Right == prev)
                {
                    stack.Pop();
                    nodes.Enqueue(current);
                    prev = current;
                    current = null;
                }
                else
                {
                    prev = current;
                    current = current.Right;
                }
            }
            return nodes;
        }

        public IEnumerable<Node> PostWithState(Node root)
        {
            Queue<Node> nodes = new Queue<Node>();
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var node = stack.Peek();
                if (node.State == 0)
                {
                    if (node.Left != null)
                    {
                        stack.Push(node.Left);
                    }
                    node.State = 1;
                }
                else if (node.State == 1)
                {
                    if (node.Right != null)
                    {
                        stack.Push(node.Right);
                    }
                    node.State = 2;
                }
                else if (node.State == 2)
                {
                    nodes.Enqueue(node);
                    node.State = 3;
                }
                else if (node.State == 3)
                {
                    stack.Pop();
                    node.State = 0;
                }
            }
            return nodes;
        }

        public IEnumerable<Node> PostWithMorris(Node root)
        {
            List<Node> nodes = new List<Node>();
            Node temp = new Node(-1);
            temp.Left = root;
            Node current = temp;
            while (current != null)
            {
                if (current.Left != null)
                {
                    var prev = FindPrevOfIn(current);
                    if (prev.Right == null)
                    {
                        prev.Right = current;
                        current = current.Left;
                    }
                    else
                    {
                        prev.Right = null;
                        Node n = current.Left;
                        Stack<Node> stack = new Stack<Node>();
                        while (n != prev)
                        {
                            stack.Push(n);
                            n = n.Right;
                        }
                        nodes.Add(prev);
                        while (stack.Count > 0)
                        {
                            nodes.Add(stack.Pop());
                        }
                        current = current.Right;
                    }
                }
                else
                {
                    current = current.Right;
                }
            }
            return nodes;
        }

        public IEnumerable<Node> Row(Node root)
        {
            List<Node> nodes = new List<Node>();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                Node n = queue.Dequeue();
                nodes.Add(n);
                if (n.Left != null)
                {
                    queue.Enqueue(n.Left);
                }
                if (n.Right != null)
                {
                    queue.Enqueue(n.Right);
                }
            }
            return nodes;
        }

        public IEnumerable<IEnumerable<Node>> RowList(Node root)
        {
            Queue<Queue<Node>> rows = new Queue<Queue<Node>>();
            Queue<Node> row = new Queue<Node>();
            row.Enqueue(root);
            rows.Enqueue(row);
            while (row.Count > 0)
            {
                Queue<Node> nextRow = new Queue<Node>();
                foreach (var node in row)
                {
                    if (node.Left != null)
                    {
                        nextRow.Enqueue(node.Left);
                    }
                    if (node.Right != null)
                    {
                        nextRow.Enqueue(node.Right);
                    }
                }
                rows.Enqueue(nextRow);
                row = nextRow;
            }
            return rows;
        }

        public IEnumerable<IEnumerable<Node>> ColumnList(Node root)
        {
            Dictionary<int, List<Node>> columnDic = new Dictionary<int, List<Node>>();
            List<Node> row = new List<Node>();
            int min = 0, max = 0;
            row.Add(root);
            while (row != null && row.Count > 0)
            {
                List<Node> nextRow = new List<Node>();
                foreach (var n in row)
                {
                    if (!columnDic.ContainsKey(n.Col))
                    {
                        List<Node> column = new List<Node>();
                        columnDic.Add(n.Col, column);
                    }
                    columnDic[n.Col].Add(n);
                    if (n.Left != null)
                    {
                        n.Left.Col = n.Col - 1;
                        min = Math.Min(min, n.Left.Col);
                        nextRow.Add(n.Left);
                    }
                    if (n.Right != null)
                    {
                        n.Right.Col = n.Col + 1;
                        max = Math.Max(max, n.Right.Col);
                        nextRow.Add(n.Right);
                    }
                }
                row = nextRow;
            }
            List<List<Node>> columnList = new List<List<Node>>();
            for (int i = min; i <= max; i++)
            {
                if (columnDic.ContainsKey(i))
                {
                    columnList.Add(columnDic[i]);
                }
            }
            return columnList;
        }
    }
}