using System;
using System.Collections.Generic;

namespace LinkedList
{
    public class RemoveSample
    {
        public void Run()
        {
            Remove remove = new Remove();
            // RunLastK(remove);
            // RunDuplicateNoOrder(remove);
            RunDuplicateInOrder(remove);
            // RunAllDuplicateInOrder(remove);
        }

        private void RunLastK(Remove remove)
        {
            int i = 1;
            Node n = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Common.PrintList(n, string.Format("before remove at last {0}: ", i));
            n = remove.RemoveLastK(n, i);
            Common.PrintList(n, string.Format("after remove at last {0}: ", i));

            i = 6;
            n = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Common.PrintList(n, string.Format("before remove at last {0}: ", i));
            n = remove.RemoveLastK(n, i);
            Common.PrintList(n, string.Format("after remove at last {0}: ", i));

            i = 3;
            n = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Common.PrintList(n, string.Format("before remove at last {0}: ", i));
            n = remove.RemoveLastK(n, i);
            Common.PrintList(n, string.Format("after remove at last {0}: ", i));

            i = 0;
            n = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Common.PrintList(n, string.Format("before remove at last {0}: ", i));
            n = remove.RemoveLastK(n, i);
            Common.PrintList(n, string.Format("after remove at last {0}: ", i));

            i = 7;
            n = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Common.PrintList(n, string.Format("before remove at last {0}: ", i));
            n = remove.RemoveLastK(n, i);
            Common.PrintList(n, string.Format("after remove at last {0}: ", i));
        }

        private void RunDuplicateNoOrder(Remove remove)
        {
            Node n = Common.BuildList(new int[] { 1, 2, 2, 3, 4, 4, 5, 5 });
            Common.PrintList(n, "remove duplicate before: ");
            n = remove.RemoveDuplicateNoOrder(n);
            Common.PrintList(n, "remove duplicate after: ");

            n = Common.BuildList(new int[] { 4, 4, 1, 2, 5, 2, 3, 5 });
            Common.PrintList(n, "remove duplicate before: ");
            n = remove.RemoveDuplicateNoOrder(n);
            Common.PrintList(n, "remove duplicate after: ");

            n = Common.BuildList(new int[] { 1, 1, 1, 1, 1 });
            Common.PrintList(n, "remove duplicate before: ");
            n = remove.RemoveDuplicateNoOrder(n);
            Common.PrintList(n, "remove duplicate after: ");

            n = Common.BuildList(new int[] { 1 });
            Common.PrintList(n, "remove duplicate before: ");
            n = remove.RemoveDuplicateNoOrder(n);
            Common.PrintList(n, "remove duplicate after: ");

            n = Common.BuildList(new int[] { });
            Common.PrintList(n, "remove duplicate before: ");
            n = remove.RemoveDuplicateNoOrder(n);
            Common.PrintList(n, "remove duplicate after: ");

            n = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Common.PrintList(n, "remove duplicate before: ");
            n = remove.RemoveDuplicateNoOrder(n);
            Common.PrintList(n, "remove duplicate after: ");
        }

        private void RunDuplicateInOrder(Remove remove)
        {
            Node n = Common.BuildList(new int[] { 1, 2, 2, 3, 4, 4, 5, 5 });
            Common.PrintList(n, "remove duplicate before: ");
            n = remove.RemoveDuplicateInOrder(n);
            Common.PrintList(n, "remove duplicate after: ");

            n = Common.BuildList(new int[] { 1, 1, 1, 1, 1 });
            Common.PrintList(n, "remove duplicate before: ");
            n = remove.RemoveDuplicateInOrder(n);
            Common.PrintList(n, "remove duplicate after: ");

            n = Common.BuildList(new int[] { 1 });
            Common.PrintList(n, "remove duplicate before: ");
            n = remove.RemoveDuplicateInOrder(n);
            Common.PrintList(n, "remove duplicate after: ");

            n = Common.BuildList(new int[] { });
            Common.PrintList(n, "remove duplicate before: ");
            n = remove.RemoveDuplicateInOrder(n);
            Common.PrintList(n, "remove duplicate after: ");

            n = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Common.PrintList(n, "remove duplicate before: ");
            n = remove.RemoveDuplicateInOrder(n);
            Common.PrintList(n, "remove duplicate after: ");
        }

        private void RunAllDuplicateInOrder(Remove remove)
        {
            Node n = Common.BuildList(new int[] { 1, 2, 2, 3, 4, 4, 5, 5 });
            Common.PrintList(n, "remove all duplicate before: ");
            n = remove.RemoveAllDuplicateInOrder(n);
            Common.PrintList(n, "remove all duplicate after: ");

            n = Common.BuildList(new int[] { 1, 1, 1, 1, 1 });
            Common.PrintList(n, "remove all duplicate before: ");
            n = remove.RemoveAllDuplicateInOrder(n);
            Common.PrintList(n, "remove all duplicate after: ");

            n = Common.BuildList(new int[] { 1 });
            Common.PrintList(n, "remove all duplicate before: ");
            n = remove.RemoveAllDuplicateInOrder(n);
            Common.PrintList(n, "remove all duplicate after: ");

            n = Common.BuildList(new int[] { });
            Common.PrintList(n, "remove all duplicate before: ");
            n = remove.RemoveAllDuplicateInOrder(n);
            Common.PrintList(n, "remove all duplicate after: ");

            n = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Common.PrintList(n, "remove all duplicate before: ");
            n = remove.RemoveAllDuplicateInOrder(n);
            Common.PrintList(n, "remove all duplicate after: ");
        }
    }

    public class Remove
    {
        public Node RemoveLastK(Node start, int k)
        {
            Node head = new Node(-1);
            head.Next = start;
            List<Node> list = new List<Node>();
            Node n = head;
            while (n != null)
            {
                list.Add(n);
                n = n.Next;
            }
            int m = list.Count - k - 1;
            if (m < 0)
            {
                throw new InvalidOperationException(string.Format("invalid k={0} of count={1}", k, list.Count - 1));
            }
            Node node = list[m];
            if (node.Next != null)
            {
                node.Next = node.Next.Next;
            }
            return head.Next;
        }

        public Node RemoveDuplicateNoOrder(Node start)
        {
            if (start == null) return start;
            HashSet<int> set = new HashSet<int>();
            set.Add(start.V);
            Node n = start;
            while (n.Next != null)
            {
                if (set.Contains(n.Next.V))
                {
                    n.Next = n.Next.Next;
                }
                else
                {
                    set.Add(n.Next.V);
                    n = n.Next;
                }
            }
            return start;
        }

        public Node RemoveDuplicateInOrder(Node start)
        {
            if(start == null) return start;
            Node n = start;
            while (n.Next != null)
            {
                if (n.V == n.Next.V)
                {
                    n.Next = n.Next.Next;
                }
                else
                {
                    n = n.Next;
                }
            }
            return start;
        }

        public Node RemoveAllDuplicateInOrder(Node start)
        {
            Node head = new Node(-1);
            head.Next = start;
            Node prev = head;
            while (prev.Next != null)
            {
                Node cur = prev.Next;
                if (cur.Next != null)
                {
                    bool duplicate = false;
                    while (cur.Next != null && cur.V == cur.Next.V)
                    {
                        duplicate = true;
                        cur.Next = cur.Next.Next;
                    }
                    if (duplicate)
                    {
                        prev.Next = cur.Next;
                    }
                    else
                    {
                        prev = prev.Next;
                    }
                }
                else
                {
                    break;
                }
            }
            return head.Next;
        }
    }
}