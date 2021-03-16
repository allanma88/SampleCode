using System;

namespace DataStructure
{
    public class LinkedList
    {
        public Node Reverse(Node n)
        {
            Node tail = n;
            while (tail.Next != null)
            {
                tail = tail.Next;
            }

            while (n != tail)
            {
                Node next = n.Next;

                Node tailNext = tail.Next;
                tail.Next = n;
                n.Next = tailNext;

                n = next;
            }

            return tail;
        }

        public Node Build(int[] nums)
        {
            Node start = new Node { Val = nums[0] };
            Node n = start;
            for (int i = 1; i < nums.Length; i++)
            {
                n.Next = new Node { Val = nums[i] };
                n = n.Next;
            }
            return start;
        }

        public void Print(Node n)
        {
            Console.Write($"{n.Val}");
            while (n.Next != null)
            {
                Console.Write($" -> {n.Next.Val}");
                n = n.Next;
            }
            Console.WriteLine();
        }
    }

    public class Node
    {
        public int Val;

        public Node Next;
    }
}