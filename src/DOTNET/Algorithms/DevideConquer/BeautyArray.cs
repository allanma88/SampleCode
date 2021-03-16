using System;

namespace DevideConquer
{
    public class BeautyArray
    {
        public void Run()
        {
            int n = 5;
            var A = BeautifulArray(n);
            Console.WriteLine($"{n}: {string.Join(',', A)}");

            n = 10;
            A = BeautifulArray(n);
            Console.WriteLine($"{n}: {string.Join(',', A)}");
        }

        public int[] BeautifulArray(int N)
        {
            int[] A = new int[N];
            for (int i = 0; i < N; i++)
            {
                A[i] = i + 1;
            }
            return BeautifulArray(A);
        }

        private int[] BeautifulArray(int[] A)
        {
            if (A.Length <= 2)
            {
                return A;
            }
            int[] right = new int[A.Length / 2];
            int[] left = new int[A.Length - A.Length / 2];
            for (int i = 0; i < A.Length; i++)
            {
                if (i % 2 == 0)
                {
                    left[i / 2] = A[i];
                }
                else
                {
                    right[i / 2] = A[i];
                }
            }
            left = BeautifulArray(left);
            right = BeautifulArray(right);
            left.CopyTo(A, 0);
            right.CopyTo(A, left.Length);
            return A;
        }
    }
}