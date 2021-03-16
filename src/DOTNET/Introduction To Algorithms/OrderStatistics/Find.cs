using System;

namespace OrderStatistics
{
    public class Find
    {
        public void Run()
        {
            int[] A = Make(10, 10, 100);
            var pair = FindMinMax(A);
            var str = string.Join(',', A);
            Console.WriteLine($"{str}: min={pair.Item1} max={pair.Item2}");
            int subMin = FindSubMin1(A);
            Console.WriteLine($"{str}: subMin={subMin}");
        }

        public Tuple<int, int> FindMinMax(int[] A)
        {
            if (A.Length <= 0)
            {
                throw new Exception("too small");
            }
            int min = Math.Min(A[0], A[1]);
            int max = Math.Max(A[0], A[1]);
            if (A.Length % 2 == 0)
            {
                if (A[0] < A[1])
                {
                    min = A[0];
                    max = A[1];
                }
                else
                {
                    min = A[1];
                    max = A[0];
                }
            }
            else
            {
                min = max = A[0];
            }
            int i;
            for (i = 2; i < A.Length; i += 2)
            {
                int small;
                int big;
                if (A[i] < A[i + 1])
                {
                    small = A[i];
                    big = A[i + 1];
                }
                else
                {
                    small = A[i + 1];
                    big = A[i];
                }
                if (small < min)
                {
                    min = small;
                }
                if (big > max)
                {
                    max = big;
                }
            }
            return new Tuple<int, int>(min, max);
        }

        public int FindSubMin(int[] A)
        {
            if (A.Length < 2)
            {
                throw new Exception("too small");
            }
            int min, subMin;
            if (A[0] < A[1])
            {
                min = A[0];
                subMin = A[1];
            }
            else
            {
                min = A[1];
                subMin = A[0];
            }
            int i;
            for (i = 2; i < A.Length; i += 2)
            {
                int small, subSmall;
                if (A[i] < A[i + 1])
                {
                    small = A[i];
                    subSmall = A[i + 1];
                }
                else
                {
                    small = A[i + 1];
                    subSmall = A[i];
                }
                if (small < min)
                {
                    if (subSmall < min)
                    {
                        subMin = subSmall;
                    }
                    else
                    {
                        subMin = min;
                    }
                    min = small;
                }
                else
                {
                    if (small < subMin)
                    {
                        subMin = small;
                    }
                }
            }
            if (i < A.Length)
            {
                if (A[i] < min)
                {
                    subMin = min;
                    min = A[i];
                }
                else if (A[i] < subMin)
                {
                    subMin = A[i];
                }
            }
            return subMin;
        }

        public int FindSubMin1(int[] A)
        {
            if (A.Length == 2)
            {
                return Math.Max(A[0], A[1]);
            }
            int n = (int)Math.Ceiling((double)A.Length / 2);
            int[] B = new int[n];
            for (int i = 0; i < B.Length; i++)
            {
                int j = 2 * i;
                int k = 2 * i + 1;
                if (k < A.Length)
                {
                    if (A[j] < A[k])
                    {
                        B[i] = A[j];
                    }
                    else
                    {
                        B[i] = A[k];
                    }
                }
                else
                {
                    B[i] = A[j];
                }
            }
            return FindSubMin1(B);
        }

        private int[] Make(int n, int min, int max)
        {
            Random random = new Random();
            int[] A = new int[n];
            for (int i = 0; i < n; i++)
            {
                A[i] = random.Next(min, max);
            }
            return A;
        }
    }
}