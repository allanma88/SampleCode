using System;

namespace OrderStatistics
{
    public class Top
    {
        public void Run()
        {
            int[] A = Make(10, 1, 100);
            string str = string.Join(',', A);
            int k = 5;
            int[] tops = TopK(A, k);
            Console.WriteLine($"{str}: the {k}s smallest is {string.Join(',', tops)}");
        }

        public int[] TopK(int[] A, int k)
        {
            int[] tops = new int[k];
            Select select = new Select();
            int kth = select.RandomizedSelect(A, 0, A.Length - 1, k);
            int ki = 0;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == kth)
                {
                    ki = i;
                }
            }

            int temp = A[ki];
            A[ki] = A[A.Length - 1];
            A[A.Length - 1] = temp;

            select.Partition(A, 0, A.Length - 1);
            for (int i = 0; i < k; i++)
            {
                tops[i] = A[i];
            }
            Array.Sort(tops);
            return tops;
        }


        private int[] Make(int n, int min, int max)
        {
            Random random = new Random(max);
            int[] A = new int[n];
            for (int i = 0; i < n; i++)
            {
                A[i] = random.Next(min, max);
            }
            return A;
        }
    }
}