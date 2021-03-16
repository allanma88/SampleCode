using System;

namespace LinkedList
{
    public class Node
    {
        public Node(int v)
        {
            V = v;
        }
        public int V { get; set; }

        public Node Next { get; set; }
    }
}