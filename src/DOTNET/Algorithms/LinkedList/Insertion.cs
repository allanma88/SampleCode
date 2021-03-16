using System;

namespace LinkedList
{
    public class InsertionSample
    {
        public void Run()
        {
            Insertion insertion = new Insertion();
            Node n = new Node(100);

            Node start = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Common.PrintList(start, "InsertLast before: ");
            start = insertion.InsertLast(start, n);
            Common.PrintList(start, "InsertLast after: ");

            start = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Common.PrintList(start, "InsertFirst before: ");
            start = insertion.InsertFirst(start, n);
            Common.PrintList(start, "InsertFirst after: ");

            start = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Common.PrintList(start, "InsertAfter before: ");
            start = insertion.InsertAfter(start, start.Next.Next, n);
            Common.PrintList(start, "InsertAfter after: ");

            start = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Common.PrintList(start, "InsertBefore before: ");
            start = insertion.InsertBefore(start, start.Next.Next, n);
            Common.PrintList(start, "InsertBefore after: ");
        }
    }

    public class Insertion
    {
        public Node InsertLast(Node start, Node n)
        {
            if (start == null) return n;
            Node pn = start;
            while (pn.Next != null)
            {
                pn = pn.Next;
            }
            pn.Next = n;
            return start;
        }

        public Node InsertFirst(Node start, Node n)
        {
            n.Next = start;
            return n;
        }

        public Node InsertBefore(Node start, Node target, Node n)
        {
            if (target == null) return start;
            Node head = new Node(-1);
            head.Next = start;
            Node p = head;
            while (p.Next != null && p.Next != target)
            {
                p = p.Next;
            }
            if (p.Next != null)
            {
                p.Next = n;
                n.Next = target;
            }
            return head.Next;
        }

        public Node InsertAfter(Node start, Node target, Node n)
        {
            if (target == null) return start;
            Node p = start;
            while (p != null && p != target)
            {
                p = p.Next;
            }
            if (p != null)
            {
                Node temp = p.Next;
                p.Next = n;
                n.Next = temp;
            }
            return start;
        }
    }
}