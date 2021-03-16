using System;

namespace LinkedList
{
    public class SortSample
    {
        Random _random = new Random();

        public void Run()
        {
            SortBase sort = new MergeSort();

            Node start = Common.BuildList(BuildArray("random", 0));
            Common.PrintList(start, "");
            start = sort.Sort(start);
            Common.PrintList(start, "");
            Console.WriteLine();

            start = Common.BuildList(BuildArray("random", 1));
            Common.PrintList(start, "");
            start = sort.Sort(start);
            Common.PrintList(start, "");
            Console.WriteLine();

            start = Common.BuildList(BuildArray("random", 2));
            Common.PrintList(start, "");
            start = sort.Sort(start);
            Common.PrintList(start, "");
            Console.WriteLine();

            start = Common.BuildList(BuildArray("random", 3));
            Common.PrintList(start, "");
            start = sort.Sort(start);
            Common.PrintList(start, "");
            Console.WriteLine();

            start = Common.BuildList(BuildArray("asc", 15));
            Common.PrintList(start, "");
            start = sort.Sort(start);
            Common.PrintList(start, "");
            Console.WriteLine();

            start = Common.BuildList(BuildArray("desc", 15));
            Common.PrintList(start, "");
            start = sort.Sort(start);
            Common.PrintList(start, "");
            Console.WriteLine();

            start = Common.BuildList(BuildArray("same", 15));
            Common.PrintList(start, "");
            start = sort.Sort(start);
            Common.PrintList(start, "");
            Console.WriteLine();

            for (int i = 0; i < 5; i++)
            {
                int len = _random.Next(10, 20);
                start = Common.BuildList(BuildArray("random", len));
                Common.PrintList(start, "");
                start = sort.Sort(start);
                Common.PrintList(start, "");
                Console.WriteLine();
            }
        }

        private int[] BuildArray(string type, int len)
        {
            int[] values = new int[len];

            for (int i = 0; i < len; i++)
            {
                if (type == "asc")
                {
                    values[i] = i + 1;
                }
                else if (type == "desc")
                {
                    values[i] = len - i;
                }
                else if (type == "random")
                {
                    values[i] = _random.Next(100);
                }
                else if (type == "same")
                {
                    values[i] = 1;
                }
            }
            return values;
        }
    }

    public abstract class SortBase
    {
        public abstract Node Sort(Node start);

        protected void Exchange(Node prev, Node node, Node next)
        {
            prev.Next = next;
            node.Next = next.Next;
            next.Next = node;
        }

        protected void Exchange(Node pPrev, Node p, Node qPrev, Node q)
        {
            if (p.Next == q)
            {
                Exchange(pPrev, p, q);
            }
            else if (q.Next == p)
            {
                Exchange(qPrev, q, p);
            }
            else
            {
                pPrev.Next = q;
                qPrev.Next = p;
                Node temp = p.Next;
                p.Next = q.Next;
                q.Next = temp;
            }
        }
    }

    public class MergeSort : SortBase
    {
        public override Node Sort(Node start)
        {
            Node head = new Node(-1);
            head.Next = start;
            int step = 1;
            int len = Len(start);
            while (step < len)
            {
                Node prev = head;
                while (prev.Next != null)
                {
                    Node p = prev.Next;
                    Node q = p;
                    for (int i = 0; i < step && q != null; i++)
                    {
                        q = q.Next;
                    }
                    if (q == null) break;
                    prev = Merge(prev, p, q, step);
                }
                step *= 2;
            }
            return head.Next;
        }

        private int Len(Node root)
        {
            int len = 0;
            Node n = root;
            while (n != null)
            {
                len++;
                n = n.Next;
            }
            return len;
        }

        private Node Merge(Node prev, Node p, Node q, int step)
        {
            Node next = q;
            for (int i = 0; i < step && next != null; i++)
            {
                next = next.Next;
            }
            int pCount = step;
            int qCount = step;
            Node n = prev;
            while ((pCount > 0 && p != null) || (qCount > 0 && q != null))
            {
                if (p == null || pCount <= 0)
                {
                    n.Next = q;
                    q = q.Next;
                    qCount--;
                }
                else if (q == null || qCount <= 0)
                {
                    n.Next = p;
                    p = p.Next;
                    pCount--;
                }
                else if (p.V < q.V)
                {
                    n.Next = p;
                    p = p.Next;
                    pCount--;
                }
                else
                {
                    n.Next = q;
                    q = q.Next;
                    qCount--;
                }
                n = n.Next;
            }
            n.Next = next;
            return n;
        }
    }

    public class InsertionSort : SortBase
    {
        public override Node Sort(Node start)
        {
            Node head = new Node(-1);
            head.Next = start;
            Node end = start;
            while (end != null && end.Next != null)
            {
                end = Insert(head, end, end.Next);
            }
            return head.Next;
        }

        private Node Insert(Node head, Node end, Node n)
        {
            Node p = head;
            while (p != end && n.V > p.Next.V)
            {
                p = p.Next;
            }
            if (p != end)
            {
                end.Next = n.Next;
                Node temp = p.Next;
                p.Next = n;
                n.Next = temp;
                return end;
            }
            else
            {
                return n;
            }
        }
    }

    public class SelectionSort : SortBase
    {
        public override Node Sort(Node start)
        {
            Node head = new Node(-1);
            head.Next = start;
            for (Node prev = head; prev.Next != null; prev = prev.Next)
            {
                Node minPrev = prev;
                Node min = minPrev.Next;
                for (Node pPrev = min; pPrev.Next != null; pPrev = pPrev.Next)
                {
                    Node p = pPrev.Next;
                    if (p.V < min.V)
                    {
                        min = p;
                        minPrev = pPrev;
                    }
                }
                Exchange(prev, prev.Next, minPrev, min);
            }
            return head.Next;
        }
    }

    public class BubbleSort : SortBase
    {
        public override Node Sort(Node start)
        {
            Node head = new Node(-1);
            head.Next = start;
            Node end = null;
            while (head.Next != end)
            {
                Node prev = head;
                Node cur = head.Next;
                while (cur.Next != end)
                {
                    if (cur.V > cur.Next.V)
                    {
                        Exchange(prev, cur, cur.Next);
                        prev = prev.Next;
                    }
                    else
                    {
                        prev = cur;
                        cur = cur.Next;
                    }
                }
                end = cur;
            }
            return head.Next;
        }
    }
}