using System;
using System.Collections.Generic;

namespace Bit
{
    public class SingleNumberSample
    {
        public void Run()
        {
            SingleNumber singleNumber = new SingleNumber();
            // Run1In2(singleNumber);
            Run1InN(singleNumber);
            // Run2In2(singleNumber);
        }

        private void Run1In2(SingleNumber singleNumber)
        {
            int[] numbers = GetNumbers(1, 2, 6);
            string s = string.Join(",", numbers);

            int single = singleNumber.Find1In2WithXor(numbers);
            Console.WriteLine($"XOR: Single 1 in 2 of {s} is {single}");

            single = singleNumber.Find1In2WithBitCount(numbers);
            Console.WriteLine($"BitCount: Single 1 in 2 of {s} is {single}");
        }

        private void Run1InN(SingleNumber singleNumber)
        {
            int n = 4;
            int[] numbers = GetNumbers(1, n, 6);
            int single = singleNumber.Find1InNWithBitCount(numbers, n);
            Console.WriteLine($"BitCount: Single 1 in {n} of {string.Join(",", numbers)} is {single}");

            n = 5;
            numbers = GetNumbers(1, n, 6);
            single = singleNumber.Find1InNWithBitCount(numbers, n);
            Console.WriteLine($"BitCount: Single 1 in {n} of {string.Join(",", numbers)} is {single}");

            n = 3;
            numbers = GetNumbers(1, n, 2);
            single = singleNumber.Find1In3WithXor(numbers);
            Console.WriteLine($"Xor: Single 1 in {n} of {string.Join(",", numbers)} is {single}");

            n = 3;
            numbers = GetNumbers(1, n, 3);
            single = singleNumber.Find1InNWithXor(numbers, n);
            Console.WriteLine($"XOR: Single 1 in {n} of {string.Join(",", numbers)} is {single}");
            single = singleNumber.Find1InNWithXorOfComplex(numbers, n);
            Console.WriteLine($"XOR(Complex): Single 1 in {n} of {string.Join(",", numbers)} is {single}");

            numbers = new int[] { 7, 7, 7, 1 };
            single = singleNumber.Find1In3WithXor1(numbers);
            Console.WriteLine($"XOR1: Single 1 in 3 of {string.Join(",", numbers)} is {single}");

            numbers = new int[] { 7, 7, 7, 7, 1 };
            single = singleNumber.Find1In4WithXor1(numbers);
            Console.WriteLine($"XOR1: Single 1 in 4 of {string.Join(",", numbers)} is {single}");
        }

        private void Run2In2(SingleNumber singleNumber)
        {
            int[] numbers = GetNumbers(2, 2, 6);
            string numbersString = string.Join(",", numbers);

            int[] singles = singleNumber.Find2In2WithXor(numbers);
            string singlesString = string.Join(",", singles);
            Console.WriteLine($"XOR: Single 2 in 2 of {numbersString} is {singlesString}");
        }

        private int[] GetNumbers(int singleCount, int nonSingleCount, int count)
        {
            Random random = new Random();
            List<int> numbers = new List<int>();
            for (int i = 0; i < count; i++)
            {
                int item = random.Next(1, 100);
                for (int j = 0; j < nonSingleCount; j++)
                {
                    if (numbers.Count > 0)
                    {
                        int index = random.Next(0, numbers.Count - 1);
                        numbers.Insert(index, item);
                    }
                    else
                    {
                        numbers.Add(item);
                    }
                }
            }

            for (int i = 0; i < singleCount; i++)
            {
                int index = random.Next(0, numbers.Count - 1);
                int single = random.Next(1, 100);
                numbers.Insert(index, single);
            }

            return numbers.ToArray();
        }
    }

    public class SingleNumber
    {
        public int Find1In2WithXor(int[] numbers)
        {
            if (numbers.Length == 0)
            {
                throw new InvalidOperationException();
            }
            int single = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                single ^= numbers[i];
            }
            return single;
        }

        public int Find1In2WithBitCount(int[] numbers)
        {
            return Find1InNWithBitCount(numbers, 2);
        }

        public int Find1In3WithXor(int[] numbers)
        {
            int x1 = 0, x2 = 0;
            foreach (int num in numbers)
            {
                x2 ^= x1 & num;
                x1 ^= num;
                int mask = ~(x1 & x2);
                x2 &= mask;
                x1 &= mask;
            }
            return x1;
        }

        public int Find1In3WithXor1(int[] numbers)
        {
            int ones = 0, twos = 0;
            foreach (var num in numbers)
            {
                ones = (ones ^ num) & ~twos;
                twos = (twos ^ num) & ~ones;
            }
            return ones;
        }

        public int Find1In4WithXor1(int[] numbers)
        {
            int x1 = 0, x2 = 0;
            foreach (var num in numbers)
            {
                x1 = x1 ^ num;
                x2 = x2 ^ num ^ x1;
            }
            return x1;
        }

        public int Find1InNWithXor1(int[] numbers)
        {
            throw new NotImplementedException();
        }

        public int Find1In3WithBitCount(int[] numbers)
        {
            return Find1InNWithBitCount(numbers, 3);
        }

        public int Find1InNWithBitCount(int[] numbers, int n)
        {
            int single = 0;
            for (int i = 0; i < 32; i++)
            {
                int count = 0;
                foreach (var num in numbers)
                {
                    if ((num & (1 << i)) > 0)
                    {
                        count++;
                    }
                }
                if (count % n != 0)
                {
                    single |= (1 << i);
                }
            }
            return single;
        }

        public int Find1InNWithXor(int[] numbers, int k)
        {
            int m = (int)Math.Log(k, 2);
            bool needMask = (k & (k - 1)) != 0;
            if (needMask) m++;
            int[] counters = new int[m];
            foreach (int num in numbers)
            {
                for (int i = m - 1; i >= 0; i--)
                {
                    int andVal = num;
                    for (int j = i - 1; j >= 0; j--)
                    {
                        andVal &= counters[j];
                    }
                    counters[i] ^= andVal;
                }
                if (needMask)
                {
                    int mask = ~0;
                    for (int i = 0; i < m; i++)
                    {
                        if (((k >> i) & 1) == 1)
                        {
                            mask &= counters[i];
                        }
                        else
                        {
                            mask &= ~counters[i];
                        }
                    }
                    mask = ~mask;
                    for (int i = 0; i < m; i++)
                    {
                        counters[i] &= mask;
                    }
                }
            }
            int single = 0;
            for (int i = 0; i < m; i++)
            {
                single |= counters[i];
            }
            return single;
        }

        public int Find1InNWithXorOfComplex(int[] numbers, int k)
        {
            int[] counts = new int[32];
            for (int i = 0; i < 32; i++)
            {
                foreach (int num in numbers)
                {
                    if (((num >> i) & 1) > 0)
                    {
                        counts[i]++;
                        counts[i] %= k;
                    }
                }
            }
            int ret = 0;
            for (int i = 0; i < 32; i++)
            {
                if (counts[i] > 0)
                {
                    ret |= (1 << i);
                }
            }
            return ret;
        }

        public int[] Find2In2WithXor(int[] numbers)
        {
            int xor = 0;
            foreach (int num in numbers)
            {
                xor ^= num;
            }
            int firstOneBit = xor & (~(xor - 1));
            int a = 0;
            foreach (int num in numbers)
            {
                if ((num & firstOneBit) > 0)
                {
                    a ^= num;
                }
            }
            int b = xor ^ a;
            return new int[] { a, b };
        }

        public int Find2In3(int[] numbers)
        {
            throw new NotImplementedException();
        }
    }
}