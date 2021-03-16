using System;
using System.Collections.Generic;

namespace UF
{
    //dot net use tail
    public class OptDisjointSet
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
                LinkedListNode node = set;
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
            LinkedListNode xSet = Find(x);
            LinkedListNode ySet = Find(y);
            if (xSet.Length < ySet.Length)
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
            LinkedListNode xSet = Find(x);
            LinkedListNode ySet = Find(y);
            if (xSet != ySet)
            {
                LinkedListNode n = ySet;
                while (n != null)
                {
                    LinkedListNode next = n.Next;

                    n.Head = xSet;
                    n.Next = xSet.Next;
                    xSet.Next = n;

                    n = next;
                }
                xSet.Length += ySet.Length;
            }
        }

        public LinkedListNode Find(LinkedListNode x)
        {
            return x.Head;
        }

        public LinkedListNode MakeSet(LinkedListNode x)
        {
            x.Head = x;
            x.Length = 1;
            return x;
        }
    }
}