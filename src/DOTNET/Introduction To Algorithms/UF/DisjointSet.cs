using System;
using System.Collections.Generic;

namespace UF
{
    public class DisjointSet
    {
        public void Run(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            LinkedListNode[] sets = new LinkedListNode[n];
            for (int i = 0; i < n; i++)
            {
                LinkedListNode node = new LinkedListNode() { Val = i };
                MakeSet(node);
                sets[i] = node;
            }
            foreach (var set in sets)
            {
                int i = set.Val;
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        Union1(set, sets[j]);
                    }
                }
            }
            HashSet<LinkedListNode> hashset = new HashSet<LinkedListNode>();
            foreach (var set in sets)
            {
                hashset.Add(Find(set));
            }
            foreach (var set in hashset)
            {
                LinkedListNode node = set.Next;
                Console.Write("head");
                while (node != null)
                {
                    Console.Write($" -> {node.Val}");
                    node = node.Next;
                }
                Console.WriteLine();
            }
        }

        public void Union1(LinkedListNode x, LinkedListNode y)
        {
            LinkedListNode xHead = Find(x);
            LinkedListNode yHead = Find(y);
            if (xHead.Length < yHead.Length)
            {
                Union(y, x);
            }
            else
            {
                Union(x, y);
            }
        }

        public void Union(LinkedListNode x, LinkedListNode y)
        {
            LinkedListNode xHead = Find(x);
            LinkedListNode yHead = Find(y);
            if (xHead != yHead)
            {
                xHead.Tail.Next = yHead.Next;
                LinkedListNode node = yHead.Next;
                while (node != null)
                {
                    node.Head = xHead;
                    node = node.Next;
                }
                xHead.Tail = yHead.Tail;
            }
        }

        public LinkedListNode Find(LinkedListNode x)
        {
            return x.Head;
        }

        public LinkedListNode MakeSet(LinkedListNode x)
        {
            LinkedListNode head = new LinkedListNode();
            head.Head = head;
            head.Next = x;
            head.Tail = x;
            head.Length = 1;
            x.Head = head;
            return head;
        }
    }

    public class LinkedListNode
    {
        public int Val;

        public LinkedListNode Next;

        public LinkedListNode Head;

        public LinkedListNode Tail;

        public int Length;
    }
}