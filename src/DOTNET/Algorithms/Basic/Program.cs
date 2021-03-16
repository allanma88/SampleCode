using System;

namespace Basic
{
    class Program
    {
        static void Main(string[] args)
        {
            BagDemo();

            Console.ReadLine();
        }

        private static void BagDemo()
        {
            Bag<int> bag = new Bag<int>();
            bag.IsEmpty().Dump();
            for (int i = 0; i < 10; i++)
            {
                bag.Add(i+1);
            }
            bag.Size().Dump();
            bag.IsEmpty().Dump();

            foreach (int i in bag)
            {
                i.Dump();
            }

            foreach (int i in bag)
            {
                i.Dump();
            }
        }

        private static void ListDemo()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                list.Add(i + 1);
            }
            for (int i = 0; i < 20; i++)
            {
                list.Get(i).Dump();
            }
            for (int i = 0; i < 20; i++)
            {
                list.Remove(i + 1);
            }
            for (int i = 0; i < 20; i++)
            {
                list.Get(i).Dump();
            }
        }

        private static void EvaluateDemo()
        {
            Evaluate evaluate = new Evaluate();
            evaluate.Eval("1 + 2 + 3").Dump();
            evaluate.Eval("1 + 2 * 3").Dump();
            evaluate.Eval("(1 + 2) * 3").Dump();
            evaluate.Eval("(1 + 2) * 3 / ( 4 - 1 ) + 10 * 9").Dump();
        }

        private static void QueueDemo()
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.Size().Dump();
            queue.Dequeue().Dump();
            queue.Dequeue().Dump();
            queue.Dequeue().Dump();
            queue.Dequeue().Dump();
            queue.Dequeue().Dump();
        }

        private static void StackDemo()
        {
            Stack<int> stack;
            stack = new Stack_Array<int>();
            //stack = new Stack_LinkList<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);
            stack.Push(6);
            stack.Push(7);
            stack.Push(8);
            stack.Push(9);
            stack.Push(10);
            stack.Push(11);

            stack.IsEmpty().Dump();
            stack.Size().Dump();

            stack.Pop().Dump();
            stack.Pop().Dump();
            stack.Pop().Dump();
            stack.Pop().Dump();
            stack.Pop().Dump();
            stack.Pop().Dump();
            stack.Pop().Dump();
            stack.Pop().Dump();
            stack.Pop().Dump();
            stack.Pop().Dump();
            stack.Pop().Dump();
            stack.Pop().Dump();
            stack.Pop().Dump();

            stack.IsEmpty().Dump();
            stack.Size().Dump();
        }
    }

    public static class DumpHelper
    {
        public static void Dump(this string s)
        {
            Console.WriteLine(s);
        }

        public static void Dump(this bool b)
        {
            Console.WriteLine(b);
        }

        public static void Dump(this int b)
        {
            Console.WriteLine(b);
        }

        public static void Dump(this double b)
        {
            Console.WriteLine(b);
        }
    }
}