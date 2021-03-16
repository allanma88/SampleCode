using System;

namespace LinkedList
{
    public class RingSample
    {
        public void Run()
        {
            Ring ring = new Ring();

            Node p = Common.BuildList(new int[] { 1, 2, 3, 4, 5 });
            bool result = ring.IsRing(p);
            Node ringNode = ring.GetRingNode(p);
            Console.WriteLine("{0} is ring: {1}, the ring node is: {2}", Common.ToString(p), result, ringNode != null ? ringNode.V.ToString() : "null");

            Node pLast = GetLast(p);
            pLast.Next = p.Next.Next;
            result = ring.IsRing(p);
            ringNode = ring.GetRingNode(p);
            Console.WriteLine("{0} is ring: {1}, the ring node is: {2}", Common.ToString(p), result, ringNode != null ? ringNode.V.ToString() : "null");
        }

        private Node GetLast(Node p)
        {
            while (p.Next != null)
            {
                p = p.Next;
            }
            return p;
        }
    }

    public class Ring
    {
        public bool IsRing(Node p)
        {
            if (p == null) return false;
            Node p1 = p;
            Node p2 = p;
            while (p1 != null && p2 != null)
            {
                if (p1.Next == null)
                {
                    break;
                }
                p1 = p1.Next.Next;
                p2 = p2.Next;
                if (p1 == p2)
                {
                    return true;
                }
            }
            return false;
        }

        public Node GetRingNode(Node p)
        {
            if (p == null) return null;
            Node p1 = p;
            Node p2 = p;
            Node meetNode = null;
            while (p1 != null && p2 != null)
            {
                if (p1.Next == null)
                {
                    break;
                }
                p1 = p1.Next.Next;
                p2 = p2.Next;
                if (p1 == p2)
                {
                    meetNode = p1;
                    break;
                }
            }
            if (meetNode != null)
            {
                Node ringNode = p;
                while (ringNode != meetNode)
                {
                    ringNode = ringNode.Next;
                    meetNode = meetNode.Next;
                }
                return ringNode;
            }
            return null;
        }
    }
}