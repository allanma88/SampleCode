using System;

namespace Sort
{
    class ParityII
    {
        public void Run()
        {
            int[] A = new int[] { 4, 2, 6, 5, 7, 3 };
            int[] B = SortArrayByParityII(A);
            Console.WriteLine(string.Join(",", B));
        }

        public int[] SortArrayByParityII(int[] A)
        {
            int[] B = new int[A.Length];
            int even = 0;
            int odd = 1;
            foreach (int n in A)
            {
                if (n % 2 == 0)
                {
                    B[even] = n;
                    even += 2;
                }
                else
                {
                    B[odd] = n;
                    odd += 2;
                }
            }
            return B;
        }
    }
}
