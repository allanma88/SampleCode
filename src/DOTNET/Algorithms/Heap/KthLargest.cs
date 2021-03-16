using System;

namespace Heap
{
    public class KthLargest
    {
        private int[] heap;
        private int heapSize;

        public KthLargest(int k, int[] nums)
        {
            heap = new int[k];
            for (int i = 0; i < nums.Length; i++)
            {
                Insert(heap, nums[i]);
            }
        }

        public int Add(int val)
        {
            Insert(heap, val);
            return heap[0];
        }

        private void Insert(int[] heap, int val)
        {
            if (heapSize < heap.Length)
            {
                heapSize++;
                heap[heapSize - 1] = val;
                MinHeapifyUp(heap, heapSize - 1);
            }
            else if (val > heap[0])
            {
                heap[0] = val;
                MinHeapify(heap, 0);
            }
        }

        private void MinHeapifyUp(int[] heap, int i)
        {
            while (i > 0)
            {
                int parent = (i + 1) / 2 - 1;
                if (heap[i] < heap[parent])
                {
                    int temp = heap[i];
                    heap[i] = heap[parent];
                    heap[parent] = temp;
                    i = parent;
                }
                else
                {
                    break;
                }
            }
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
    }
}