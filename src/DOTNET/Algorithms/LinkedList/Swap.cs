using System;

namespace LinkedList
{
    public class SwapSample
    {
        public void Run()
        {
            Swap swap = new Swap();
            Node start = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            Node p = start.Next.Next;
            Node q = p.Next.Next;
            Common.PrintList(start, string.Format("swap {0},{1} before: ", p.V, q.V));
            start = swap.SwapNode(start, p, q);
            Common.PrintList(start, string.Format("swap {0},{1} after: ", p.V, q.V));

            start = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            p = start.Next.Next;
            q = p.Next;
            Common.PrintList(start, string.Format("swap {0},{1} before: ", p.V, q.V));
            start = swap.SwapNode(start, p, q);
            Common.PrintList(start, string.Format("swap {0},{1} after: ", p.V, q.V));

            start = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            p = start.Next.Next;
            q = p;
            Common.PrintList(start, string.Format("swap {0},{1} before: ", p.V, q.V));
            start = swap.SwapNode(start, p, q);
            Common.PrintList(start, string.Format("swap {0},{1} after: ", p.V, q.V));

            start = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6 });
            p = start.Next.Next;
            q = new Node(7);
            Common.PrintList(start, string.Format("swap {0},{1} before: ", p.V, q.V));
            start = swap.SwapNode(start, p, q);
            Common.PrintList(start, string.Format("swap {0},{1} after: ", p.V, q.V));
        }
    }

    public class Swap
    {
        public Node SwapNode(Node start, Node p, Node q)
        {
            Node head = new Node(-1);
            head.Next = start;
            Node n = head;
            Node pPrev = null, qPrev = null;
            while (n.Next != null)
            {
                if (p == q)
                {
                    break;
                }
                if (n.Next == p)
                {
                    pPrev = n;
                }
                if (n.Next == q)
                {
                    qPrev = n;
                }
                if (pPrev != null && qPrev != null)
                {
                    SwapNode(pPrev, p, qPrev, q);
                    break;
                }
                n = n.Next;
            }
            return head.Next;
        }

        public void SwapNode(Node pPrev, Node p, Node qPrev, Node q)
        {
            if(pPrev == null) throw new ArgumentNullException("pPrev");
            if(qPrev == null) throw new ArgumentNullException("qPrev");
            if (p == q)
            {
            }
            else if (p.Next == q)
            {
                SwapAdjacentNode(pPrev, p, q);
            }
            else if (q.Next == p)
            {
                SwapAdjacentNode(qPrev, q, p);
            }
            else
            {
                Node temp = q.Next;
                pPrev.Next = q;
                q.Next = p.Next;
                qPrev.Next = p;
                p.Next = temp;
            }
        }

        public void SwapAdjacentNode(Node prev, Node cur, Node next)
        {
            if (prev == null) throw new ArgumentNullException("prev");
            Node temp = next.Next;
            prev.Next = next;
            next.Next = cur;
            cur.Next = temp;
        }
    }
}