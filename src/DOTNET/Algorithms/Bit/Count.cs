using System;
using System.Diagnostics;

namespace Bit
{
    public class CountSample
    {
        public void Run()
        {
            Count count = new Count();
            uint c = count.Count1WithCount(10);
            Console.WriteLine(c);
            c = count.Count1WithCount(18);
            Console.WriteLine(c);
            c = count.Count1WithBitAdd(10);
            Console.WriteLine(c);
            c = count.Count1WithBitAdd(18);
            Console.WriteLine(c);
            c = count.Count1(10);
            Console.WriteLine(c);
            c = count.Count1(18);
            Console.WriteLine(c);

            PerfRun(count);
        }

        private void PerfRun(Count count)
        {
            Stopwatch watch = Stopwatch.StartNew();
            for (int i = 0; i < 1000 * 1000 * 100; i++)
            {
                count.Count1WithCount(18);
            }
            Console.WriteLine("Count: " + watch.Elapsed);

            watch = Stopwatch.StartNew();
            for (int i = 0; i < 1000 * 1000 * 100; i++)
            {
                count.Count1WithBitAdd(18);
            }
            Console.WriteLine("Bit Add: " + watch.Elapsed);

            watch = Stopwatch.StartNew();
            for (int i = 0; i < 1000 * 1000 * 100; i++)
            {
                count.Count1WithBitwiseAnd(18);
            }
            Console.WriteLine("Bitwise &: " + watch.Elapsed);

            uint n = (uint)(Math.Pow(2, 31) - 1);
            watch = Stopwatch.StartNew();
            for (int i = 0; i < 1000 * 1000 * 100; i++)
            {
                count.Count1WithBitwiseAnd(n);
            }
            Console.WriteLine("Bitwise &: " + watch.Elapsed);

            watch = Stopwatch.StartNew();
            for (int i = 0; i < 1000 * 1000 * 100; i++)
            {
                count.Count1((uint)(18));
            }
            Console.WriteLine("Simple: " + watch.Elapsed);
        }
    }

    public class Count
    {
        public uint Count1WithCount(uint n)
        {
            uint j = 0;
            for (int i = 0; i < 32; i++)
            {
                if ((n & 1 << i) > 0)
                {
                    j++;
                }
            }
            return j;
        }

        public uint Count1WithBitAdd(uint n)
        {
            n = ((n & 0xAAAAAAAA) >> 1) + (n & 0x55555555);
            n = ((n & 0xCCCCCCCC) >> 2) + (n & 0x33333333);
            n = ((n & 0xF0F0F0F0) >> 4) + (n & 0x0F0F0F0F);
            n = ((n & 0xFF00FF00) >> 8) + (n & 0x00FF00FF);
            n = ((n & 0xFFFF0000) >> 16) + (n & 0x0000FFFF);
            return n;
        }

        public uint Count1WithBitwiseAnd(uint n)
        {
            uint count = 0;
            while (n > 0)
            {
                n = n & (n - 1);
                count++;
            }
            return count;
        }

        public uint Count1(uint n)
        {
            uint ret = n;
            while (n != 0)
            {
                n = n >> 1;
                ret -= n;
            }
            return ret;
        }

        public uint Count1WithRecur(uint n)
        {
            return Count1WithRecur(n, n >> 1);
        }

        private uint Count1WithRecur(uint n, uint m)
        {
            if (m == 0) return n;
            return Count1WithRecur(n - m, m >> 1);
        }
    }
}