using System;

namespace Sort
{
    public class RadixVarSort
    {
        public void Run()
        {
            int d = 3;
            int[] A = Make(10, d);
            Sort(A, d);
            Console.WriteLine(string.Join(',', A));
        }

        public void Sort(int[] A, int d)
        {
            for (int i = 1; i <= d; i++)
            {
                CountSort(A, i);
            }
        }

        private void CountSort(int[] A, int d)
        {
            int[] B = new int[A.Length];
            for (int i = 0; i < A.Length; i++)
            {
                B[i] = A[i];
            }
            int[] C = new int[10];
            for (int i = 0; i < B.Length; i++)
            {
                int digit = GetDigit(B[i], d);
                C[digit] = C[digit] + 1;
            }
            for (int i = 1; i < 10; i++)
            {
                C[i] += C[i - 1];
            }
            for (int i = B.Length - 1; i >= 0; i--)
            {
                int digit = GetDigit(B[i], d);
                A[C[digit] - 1] = B[i];
                C[digit] = C[digit] - 1;
            }
        }

        private int GetDigit(int n, int d)
        {
            return (int)((n / Math.Pow(10, d - 1)) % 10);
        }

        private int[] Make(int n, int d)
        {
            int max = (int)Math.Pow(10, d);
            Random random = new Random();
            int[] A = new int[n];
            for (int i = 0; i < n; i++)
            {
                A[i] = random.Next(0, max);
            }
            return A;
        }
    }
}