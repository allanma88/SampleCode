using System;
using System.Collections.Generic;

namespace Bit
{
    public class PrimeSample
    {
        public void Run()
        {
            Prime prime = new Prime();
            Console.WriteLine("Normal: ");
            int n = 10;
            var primes = prime.Find(n);
            Console.WriteLine($"{n}: " + string.Join(",", primes));
            n = 30;
            primes = prime.Find(n);
            Console.WriteLine($"{n}: " + string.Join(",", primes));

            Console.WriteLine("Normal Filter: ");
            n = 10;
            primes = prime.FindWithNormalFilter(n);
            Console.WriteLine($"{n}: " + string.Join(",", primes));
            n = 30;
            primes = prime.FindWithNormalFilter(n);
            Console.WriteLine($"{n}: " + string.Join(",", primes));

            Console.WriteLine("Normal Filter 2: ");
            n = 10;
            primes = prime.FindWithNormalFilter2(n);
            Console.WriteLine($"{n}: " + string.Join(",", primes));
            n = 30;
            primes = prime.FindWithNormalFilter2(n);
            Console.WriteLine($"{n}: " + string.Join(",", primes));

             Console.WriteLine("Linear Filter: ");
            n = 10;
            primes = prime.FindWithLinearFilter(n);
            Console.WriteLine($"{n}: " + string.Join(",", primes));
            n = 30;
            primes = prime.FindWithLinearFilter(n);
            Console.WriteLine($"{n}: " + string.Join(",", primes));
        }
    }

    public class Prime
    {
        public List<int> Find(int n)
        {
            List<int> primes = new List<int>();
            for (int i = 2; i <= n; i++)
            {
                for (int j = 2; j <= i; j++)
                {
                    if (i != j && i % j == 0)
                    {
                        break;
                    }
                    else if (i == j)
                    {
                        primes.Add(i);
                    }
                }
            }
            return primes;
        }

        public List<int> FindWithNormalFilter(int n)
        {
            bool[] table = new bool[n + 1];
            for (int i = 2; i <= n; i++)
            {
                for (int j = i + i; j <= n; j += i)
                {
                    table[j] = true;
                }
            }
            List<int> primes = new List<int>();
            for (int i = 2; i < table.Length; i++)
            {
                if (!table[i])
                {
                    primes.Add(i);
                }
            }
            return primes;
        }

        public List<int> FindWithNormalFilter2(int n)
        {
            bool[] table = new bool[n + 1];
            for (int j = 2; j <= n / 2; j++)
            {
                for (int i = 2; i <= n / j; i++)
                {
                    table[i * j] = true;
                }
            }
            List<int> primes = new List<int>();
            for (int i = 2; i < table.Length; i++)
            {
                if (!table[i])
                {
                    primes.Add(i);
                }
            }
            return primes;
        }

        public List<int> FindWithLinearFilter(int n)
        {
            List<int> primes = new List<int>();
            bool[] table = new bool[n + 1];
            for (int i = 2; i <= n; i++)
            {
                if (!table[i])
                {
                    primes.Add(i);
                }
                for (int j = 0; j < primes.Count && i * primes[j] <= n; j++)
                {
                    table[i * primes[j]] = true;
                    if (i % primes[j] == 0) break;
                }
            }
            return primes;
        }
    }
}