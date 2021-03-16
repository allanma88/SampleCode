using System;

namespace Heap
{
    public class StoneWeight
    {
        public void Run()
        {
            int[] stones = new int[] { 2, 7, 4, 1, 8, 1 };
            int last = LastStoneWeight(stones);
            Console.WriteLine(last);
        }
        private int heapSize = 0;

        public int LastStoneWeight(int[] stones)
        {
            int[] nums = new int[stones.Length + 1];
            for (int i = 0; i < stones.Length; i++)
            {
                Insert(nums, stones[i]);
            }
            while (heapSize > 1)
            {
                int x = ExtractMax(nums);
                int y = ExtractMax(nums);
                if (x - y > 0)
                {
                    Insert(nums, x - y);
                }
            }
            if (heapSize == 1)
            {
                return nums[1];
            }
            else
            {
                return 0;
            }
        }

        private void MaxHeapify(int[] nums, int i)
        {
            while (i <= heapSize)
            {
                int max = i;
                int left = max * 2;
                int right = max * 2 + 1;

                if (left <= heapSize && nums[left] > nums[max])
                {
                    max = left;
                }
                if (right <= heapSize && nums[right] > nums[max])
                {
                    max = right;
                }

                if (i != max)
                {
                    int temp = nums[i];
                    nums[i] = nums[max];
                    nums[max] = temp;
                    i = max;
                }
                else
                {
                    break;
                }
            }
        }

        private int ExtractMax(int[] nums)
        {
            int max = nums[1];
            nums[1] = nums[heapSize--];
            MaxHeapify(nums, 1);
            return max;
        }

        private void Insert(int[] nums, int v)
        {
            nums[++heapSize] = v;
            int i = heapSize;
            while (i > 1)
            {
                int parent = i / 2;
                if (nums[i] > nums[parent])
                {
                    int temp = nums[i];
                    nums[i] = nums[parent];
                    nums[parent] = temp;
                    i = parent;
                }
                else
                {
                    break;
                }
            }
        }
    }
}