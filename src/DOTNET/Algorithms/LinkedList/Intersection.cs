using System;

namespace LinkedList
{
    public class IntersectionSample
    {
        public void Run()
        {
            Intersection intersection = new Intersection();
            Insertion insertion = new Insertion();

            Node p = Common.BuildList(new int[] { 11, 12, 13, 14 });
            Node q = Common.BuildList(new int[] { 21, 22, 23 });
            bool result = intersection.CheckIntersect(p, q);
            Node interN = intersection.GetIntersectNode(p, q);
            Console.WriteLine("{0} and {1} is intersect: {2}, intersect node is: {3}", Common.ToString(p), Common.ToString(q), result, interN != null ? interN.V.ToString() : "null");

            Node n = Common.BuildList(new int[] { 4, 5, 6 });
            p = insertion.InsertLast(p, n);
            q = insertion.InsertLast(q, n);
            result = intersection.CheckIntersect(p, q);
            interN = intersection.GetIntersectNode(p, q);
            Console.WriteLine("{0} and {1} is intersect: {2}, intersect node is: {3}", Common.ToString(p), Common.ToString(q), result, interN != null ? interN.V.ToString() : "null");
        }
    }

    public class Intersection
    {
        public bool CheckIntersect(Node p, Node q)
        {
            int len;
            Node pLast = GetLast(p, out len);
            Node qLast = GetLast(q, out len);
            return pLast == qLast;
        }

        public Node GetIntersectNode(Node p, Node q)
        {
            if(p == null || q == null) return null;
            int pLen, qLen;
            Node pLast = GetLast(p, out pLen);
            Node qLast = GetLast(q, out qLen);
            if (pLast != qLast)
            {
                return null;
            }
            if (pLen > qLen)
            {
                int minus = pLen - qLen;
                for (int i = 0; i < minus; i++)
                {
                    p = p.Next;
                }
            }
            else if (pLen < qLen)
            {
                int minus = qLen - pLen;
                for (int i = 0; i < minus; i++)
                {
                    q = q.Next;
                }
            }
            while (p != q)
            {
                p = p.Next;
                q = q.Next;
            }
            return p;
        }

        private Node GetLast(Node p, out int len)
        {
            len = 1;
            while (p.Next != null)
            {
                p = p.Next;
                len++;
            }
            return p;
        }
    }
}