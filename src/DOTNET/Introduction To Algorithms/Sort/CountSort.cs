using System;

namespace Sort
{
    public class CountSort
    {
        public void Run()
        {
            int[] A = new int[] { 1, 2, 1, 5, 4, 3, 3, 1 };
            int[] B = new int[A.Length];
            SortFromFront(A, B, 5);
            Console.WriteLine(string.Join(',', B));
            int count = Range(A, 5, 2, 4);
            Console.WriteLine(count);

            A = new int[] { 6, 0, 2, 0, 1, 3, 4, 6, 1, 3, 2 };
            B = new int[A.Length];
            SortFromFront(A, B, 6);
            Console.WriteLine(string.Join(',', B));
            count = Range(A, 6, 3, 5);
            Console.WriteLine(count);
        }

        public void SortFromBottom(int[] A, int[] B, int k)
        {
            int[] C = new int[k + 1];
            for (int i = 0; i < A.Length; i++)
            {
                C[A[i]] = C[A[i]] + 1;
            }
            for (int i = 1; i < k + 1; i++)
            {
                C[i] += C[i - 1];
            }
            for (int i = A.Length - 1; i >= 0; i--)
            {
                B[C[A[i]] - 1] = A[i];
                C[A[i]] = C[A[i]] - 1;
            }
        }

        public void SortFromFront(int[] A, int[] B, int k)
        {
            int[] C = new int[k + 1];
            for (int i = 0; i < A.Length; i++)
            {
                C[A[i]] = C[A[i]] + 1;
            }
            for (int i = 1; i < k + 1; i++)
            {
                C[i] += C[i - 1];
            }
            for (int i = k; i > 0; i--)
            {
                C[i] = C[i - 1];
            }
            C[0] = 0;
            for (int i = 0; i < A.Length; i++)
            {
                B[C[A[i]]] = A[i];
                C[A[i]] = C[A[i]] + 1;
            }
        }

        public int Range(int[] A, int k, int a, int b)
        {
            int[] C = new int[k + 1];
            for (int i = 0; i < A.Length; i++)
            {
                C[A[i]] = C[A[i]] + 1;
            }
            for (int i = 1; i < k + 1; i++)
            {
                C[i] += C[i - 1];
            }
            return C[b] - C[a] + 1;
        }
    }
}