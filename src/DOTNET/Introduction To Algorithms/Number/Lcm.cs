using System;

namespace Number
{
    public class Lcm
    {
        public void Run()
        {
            int a = 3;
            int b = 1;
            int lcm = Find(a, b);
            Console.WriteLine($"{a} and {b} lcm is: {lcm}");

            a = 3;
            b = 2;
            lcm = Find(a, b);
            Console.WriteLine($"{a} and {b} lcm is: {lcm}");

            a = 18;
            b = 12;
            lcm = Find(a, b);
            Console.WriteLine($"{a} and {b} lcm is: {lcm}");

            a = 3;
            b = 3;
            lcm = Find(a, b);
            Console.WriteLine($"{a} and {b} lcm is: {lcm}");

            a = 3;
            b = 0;
            lcm = Find(a, b);
            Console.WriteLine($"{a} and {b} lcm is: {lcm}");

            int c = 3;
            a = 3;
            b = 1;
            lcm = Find(a, b, c);
            Console.WriteLine($"{a}, {b}, {c} lcm is: {lcm}");

            c = 1;
            a = 3;
            b = 2;
            lcm = Find(a, b, c);
            Console.WriteLine($"{a}, {b}, {c} lcm is: {lcm}");

            c = 2;
            a = 4;
            b = 3;
            lcm = Find(a, b, c);
            Console.WriteLine($"{a}, {b}, {c} lcm is: {lcm}");
        }

        public int Find(int a, int b)
        {
            if (a < b) throw new InvalidOperationException("a must greater than b");

            Gcd gcd = new Gcd();
            int n = gcd.Elucid(a, b);
            return a / n * b / n * n;
        }

        public int Find(int a, int b, int c)
        {
            return Find(Find(a, b), c);
        }
    }
}