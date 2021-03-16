using System;

namespace DevideConquer
{
    public class KLargest
    {
        public void Run()
        {
            int[] nums = new int[] { 3, 2, 1, 5, 4, 6 };
            int kLargest = FindKthLargest(nums, 2);
            Console.WriteLine(kLargest);

            nums = new int[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 };
            kLargest = FindKthLargest(nums, 4);
            Console.WriteLine(kLargest);

            nums = new int[] { 2, 1 };
            kLargest = FindKthLargest(nums, 2);
            Console.WriteLine(kLargest);

            nums = new int[] { 5, 2, 4, 1, 3, 6, 0 };
            kLargest = FindKthLargest(nums, 4);
            Console.WriteLine(kLargest);
        }

        //6, 5, 4, 1, 3, 2, 0
        public int FindKthLargest(int[] nums, int k)
        {
            int p = 0;
            int r = nums.Length - 1;
            while (p < r)
            {
                int q = Partition(nums, p, r);
                int m = q - p + 1;
                if (k == m)
                {
                    return nums[q];
                }
                else if (k < m)
                {
                    r = q - 1;
                }
                else
                {
                    k = k - m;
                    p = q + 1;
                }
            }
            return nums[p];
        }

        private int Partition(int[] nums, int p, int r)
        {
            int x = nums[r];
            int i = p - 1;
            for (int j = p; j < r; j++)
            {
                if (nums[j] >= x)
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
    }
}