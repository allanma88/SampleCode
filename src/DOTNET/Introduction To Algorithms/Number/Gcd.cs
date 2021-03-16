using System;
using System.Collections.Generic;

namespace Number
{
    public class Gcd
    {
        public void Run()
        {
            int a = 12, b = 9;
            int gcd = Elucid1(a, b);
            Console.WriteLine($"{a} and {b} gcd is {gcd}");

            var tuple = ExtendElucid(a, b);
            Console.WriteLine($"{a} and {b} gcd is {tuple.Item1} with x = {tuple.Item2} y = {tuple.Item3}");
        }

        public int Elucid(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }
            return Elucid(b, a % b);
        }

        public int Elucid1(int a, int b)
        {
            while (b != 0)
            {
                int temp = a;
                a = b;
                b = temp % b;
            }
            return a;
        }

        public Tuple<int, int, int> ExtendElucid(int a, int b)
        {
            if (b == 0)
            {
                return new Tuple<int, int, int>(a, 1, 0);
            }
            var tuple = ExtendElucid(b, a % b);
            int y = tuple.Item2 - (int)Math.Floor((double)(a / b)) * tuple.Item3;
            return new Tuple<int, int, int>(tuple.Item1, tuple.Item3, y);
        }

        public Tuple<int, int, int> ExtendElucid1(int a, int b)
        {
            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();
            while (b != 0)
            {
                stack.Push(new Tuple<int, int>(a, b));
                int tempA = a;
                a = b;
                b = tempA % b;
            }
            int d = a, x = 1, y = 0;
            while (stack.Count > 0)
            {
                var tuple = stack.Pop();
                int tempY = y;
                y = x - (int)Math.Floor((double)(tuple.Item1 / tuple.Item2)) * y;
                x = tempY;
            }
            return new Tuple<int, int, int>(d, x, y);
        }
    }
}