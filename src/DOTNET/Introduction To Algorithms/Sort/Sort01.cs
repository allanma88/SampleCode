using System;

namespace Sort
{
    public class Sort01
    {
        public void Run()
        {
            int[] A = new int[] { 0, 1, 1, 0, 0, 1, 1, 0 };
            Sort(A);
            Console.WriteLine(string.Join(',', A));
        }

        public void Sort(int[] A)
        {
            int i = 0, j = A.Length - 1;
            while (i < j)
            {
                while (A[i] == 0)
                {
                    i++;
                }
                while (A[j] == 1)
                {
                    j--;
                }
                int temp = A[i];
                A[i] = A[j];
                A[j] = temp;
                i++;
                j--;
            }
        }
    }
}