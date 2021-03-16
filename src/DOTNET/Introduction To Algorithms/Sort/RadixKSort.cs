using System;

namespace Sort
{
    public class RadixKSort
    {
        public void Run()
        {
            int d = 3;
            int k = 5;
            int[] A = Make(10, k, d);
            Sort(A, k, d);
            Console.WriteLine(string.Join(',', A));
        }

        public void Sort(int[] A, int k, int d)
        {
            for (int i = 1; i <= d; i++)
            {
                CountSort(A, k, i);
            }
        }

        private void CountSort(int[] A, int k, int d)
        {
            int[] B = new int[A.Length];
            for (int i = 0; i < A.Length; i++)
            {
                B[i] = A[i];
            }
            int[] C = new int[10];
            for (int i = 0; i < B.Length; i++)
            {
                int digit = GetDigit(B[i], k, d);
                C[digit] = C[digit] + 1;
            }
            for (int i = 1; i < 10; i++)
            {
                C[i] += C[i - 1];
            }
            for (int i = B.Length - 1; i >= 0; i--)
            {
                int digit = GetDigit(B[i], k, d);
                A[C[digit] - 1] = B[i];
                C[digit] = C[digit] - 1;
            }
        }

        private int GetDigit(int n, int k, int d)
        {
            return (int)((n / Math.Pow(k, d - 1)) % k);
        }

        private int[] Make(int n, int k, int d)
        {
            Random random = new Random();
            int max = (int)(Math.Pow(k, d) - 1);
            int[] A = new int[n];
            for (int i = 0; i < n; i++)
            {
                A[i] = random.Next(0, max);
            }
            return A;
        }
    }
}