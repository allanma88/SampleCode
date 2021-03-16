using System;

namespace OrderStatistics
{
    public class Select
    {
        public void Run()
        {
            int[] A = Make(10, 10, 100);
            string str = string.Join(',', A);
            int k = 5;
            int n = RandomizedSelect_Loop(A, 0, 9, k);
            Console.WriteLine($"{str}: the {k} smallest is {n}");
        }

        public int RandomizedSelect(int[] A, int p, int r, int i)
        {
            if (p == r)
            {
                return A[p];
            }
            int q = RandomizedPartition(A, p, r);
            int k = q - p + 1;
            if (i == k)
            {
                return A[q];
            }
            else if (i < k)
            {
                return RandomizedSelect(A, p, q - 1, i);
            }
            else
            {
                return RandomizedSelect(A, q + 1, r, i - k);
            }
        }

        public int RandomizedSelect_Loop(int[] A, int p, int r, int i)
        {
            while (p < r)
            {
                int q = RandomizedPartition(A, p, r);
                int k = q - p + 1;
                if (i == k)
                {
                    return A[q];
                }
                else if (i < k)
                {
                    r = q - 1;
                }
                else
                {
                    p = q + 1;
                    i -= k;
                }
            }
            return A[p];
        }

        private int RandomizedPartition(int[] nums, int p, int r)
        {
            int i = new Random().Next(p, r);
            int temp = nums[i];
            nums[i] = nums[r];
            nums[r] = temp;
            return Partition(nums, p, r);
        }

        public int Partition(int[] nums, int p, int r)
        {
            int i = p - 1;
            int x = nums[r];
            for (int j = p; j < r; j++)
            {
                if (nums[j] <= x)
                {
                    i++;
                    int temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;
                }
            }

            int temp1 = nums[i + 1];
            nums[i + 1] = nums[r];
            nums[r] = temp1;

            return i + 1;
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