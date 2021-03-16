using System;
using System.Diagnostics;
using System.Text;

namespace LinkedList
{
    public class ReverseSample
    {
        public void Run()
        {
            ReverseImp reverseImp = new ReverseImp();
            Node start = Common.BuildList(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            Common.PrintList(start, "Origin: ");
            Node newStart = reverseImp.Reverse(start);
            Common.PrintList(newStart, "Reverse: ");

            start = Common.BuildList(new int[] { 1 });
            Common.PrintList(start, "Origin: ");
            newStart = reverseImp.Reverse(start);
            Common.PrintList(newStart, "Reverse: ");

            start = Common.BuildList(new int[] { });
            Common.PrintList(start, "Origin: ");
            newStart = reverseImp.Reverse(start);
            Common.PrintList(newStart, "Reverse: ");

            start = Common.BuildList(new int[] { 1, 1 });
            Common.PrintList(start, "Origin: ");
            newStart = reverseImp.Reverse(start);
            Common.PrintList(newStart, "Reverse: ");
        }
    }

    public class ReverseImp
    {
        public Node Reverse(Node start)
        {
            if (start == null)
            {
                return null;
            }
            Node newStart = start;
            while (newStart.Next != null)
            {
                newStart = newStart.Next;
            }
            Node n = start;
            while (n != newStart)
            {
                Node next = n.Next;

                Node temp = newStart.Next;
                newStart.Next = n;
                n.Next = temp;

                n = next;
            }
            return newStart;
        }

        public Node ReverseWithRecur(Node start)
        {
            throw new NotImplementedException();
        }
    }
}