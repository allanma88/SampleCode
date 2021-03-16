using System.Collections.Generic;

namespace HeapSort
{
    public class Top
    {
        public int[] TopKs(int[] nums, int k)
        {
            int[] heap = new int[k];
            for (int i = 0; i < k; i++)
            {
                heap[i] = nums[i];
            }
	        BuildMinHeap(heap);
            for (int i = k; i < nums.Length; i++)
            {
                if (nums[i] > heap[0])
                {
                    ReplaceMin(heap, nums[i]);
                }
            }
            return heap;
        }

        private void BuildMinHeap(int[] heap)
        {
            for (int i = heap.Length / 2 - 1; i >= 0; i--)
            {
                MinHeapify(heap, i);
            }
        }

        private void ReplaceMin(int[] heap, int num)
        {
            heap[0] = num;
            MinHeapify(heap, 0);
        }

        private void MinHeapify(int[] heap, int i)
        {
            while (i < heap.Length)
            {
                int min = i;
                int left = (i + 1) * 2 - 1;
                int right = (i + 1) * 2;
                if (left < heap.Length && heap[left] < heap[min])
                {
                    min = left;
                }
                if (right < heap.Length && heap[right] < heap[min])
                {
                    min = right;
                }
                if (min != i)
                {
                    int temp = heap[min];
                    heap[min] = heap[i];
                    heap[i] = temp;
                    i = min;
                }
                else
                {
                    break;
                }
            }
        }

        public int TopK(int[] nums, int k)
        {
            var tops = TopKs(nums, k);
            return tops[0];
        }
    }
}