using System;
using System.Collections.Generic;

namespace BinaryTree
{
    public class Node
    {
        public Node()
        {
        }

        public Node(int v)
        {
            V = v;
        }

        public int V { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public int Col { get; set; }

        public int State { get; set; }
    }
}