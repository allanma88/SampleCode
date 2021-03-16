using System;

namespace Heap
{
    public class KLargest
    {
        public void Run()
        {
            int[] nums = new int[] { 3, 2, 1, 5, 6, 4 };
            int k = 2;
            int n = FindKthLargest(nums, k);
            Console.WriteLine(n);

            nums = new int[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 };
            k = 4;
            n = FindKthLargest(nums, k);
            Console.WriteLine(n);
        }

        public int FindKthLargest(int[] nums, int k)
        {
            for (int i = 1; i < k; i++)
            {
                int p = (i + 1) / 2 - 1;
                if (nums[i] < nums[p])
                {
                    Exchange(nums, i, p);
                }
            }
            for (int i = k; i < nums.Length; i++)
            {
                if (nums[i] > nums[0])
                {
                    Exchange(nums, i, 0);
                    MinHeapify(nums, 0, k);
                }
            }
            return nums[0];
        }

        private void MinHeapify(int[] nums, int i, int k)
        {
            while (i < k)
            {
                int left = (i + 1) * 2 - 1;
                int right = (i + 1) * 2;
                int min = i;
                if (left < k && nums[left] < nums[min])
                {
                    min = left;
                }
                if (right < k && nums[right] < nums[min])
                {
                    min = right;
                }
                if (min != i)
                {
                    Exchange(nums, min, i);
                    i = min;
                }
                else
                {
                    break;
                }
            }
        }

        private void Exchange(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
    }
}