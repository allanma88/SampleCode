using System;
using System.Collections.Generic;

namespace LinkedList
{
    public class MergeSample
    {
        public void Run()
        {
            Merge merge = new Merge();
            RunConstruct(merge);
            // RunMergeKLists(merge);
        }

        private void RunConstruct(Merge merge)
        {
            int len = 3;
            int[][] nodes = new int[len][];
            Random random = new Random();
            for (int i = 0; i < len; i++)
            {
                // int count = random.Next(5, 10);
                int count = 5;
                nodes[i] = new int[count];
                for (int j = 0; j < count; j++)
                {
                    nodes[i][j] = j + 1;
                }
            }
            Common.PrintMatrix(nodes);
            Node n = merge.Construct(nodes);
            Common.PrintList(n, "");
        }

        private void RunMergeKLists(Merge merge)
        {
            Node node1 = Common.BuildList(new int[] { 1, 3, 7, 12, 42 });
            Node node2 = Common.BuildList(new int[] { 7, 12, 18, 42, 53, 62, 97 });
            Node node3 = Common.BuildList(new int[] { 1, 30 });
            var n = merge.MergeKLists(new List<Node>() { node1, node2, node3 });
            Common.PrintList(n, "");
        }
    }

    public class Merge
    {
        public Node MergeKLists(List<Node> lists)
        {
            Node head = new Node(-1);
            Node n = head;
            while (lists.Count > 0)
            {
                int min = 0;
                for (int i = 1; i < lists.Count; i++)
                {
                    if (lists[i].V < lists[min].V)
                    {
                        min = i;
                    }
                }
                n.Next = lists[min];
                if (lists[min].Next == null)
                {
                    lists.RemoveAt(min);
                }
                else
                {
                    lists[min] = lists[min].Next;
                }
                n = n.Next;
            }
            return head.Next;
        }

        public Node Construct(int[][] nodes)
        {
            var len = nodes.Length;
            int[] counts = new int[len];
            for (int i = 0; i < len; i++)
            {
                counts[i] = nodes[i].Length;
            }
            int[] indics = new int[len];
            Node head = new Node(-1);
            Node n = head;
            while (true)
            {
                int min = 0;
                while (min < len && indics[min] >= counts[min])
                {
                    min++;
                }
                if (min >= len) break;
                for (int i = min + 1; i < len; i++)
                {
                    if (indics[i] < counts[i])
                    {
                        int v = nodes[i][indics[i]];
                        if (v < nodes[min][indics[min]])
                        {
                            min = i;
                        }
                    }
                }
                n.Next = new Node(nodes[min][indics[min]++]);
                n = n.Next;
            }
            return head.Next;
        }
    }
}