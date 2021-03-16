using System;
using System.Collections.Generic;

namespace Heap
{
    public class TopK
    {
        public void Run()
        {
            int[] nums = new int[] { 1, 1, 1, 2, 2, 3 };
            var tops = TopKFrequent(nums, 2);
            Console.WriteLine(string.Join(',', tops));

            nums = new int[] { 1 };
            tops = TopKFrequent(nums, 1);
            Console.WriteLine(string.Join(',', tops));
        }

        public IList<int> TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> countMap = new Dictionary<int, int>();
            foreach (var num in nums)
            {
                if (countMap.ContainsKey(num))
                {
                    countMap[num]++;
                }
                else
                {
                    countMap[num] = 1;
                }
            }
            int[,] matrix = new int[countMap.Count, 2];
            int i = 0;
            foreach (var pair in countMap)
            {
                matrix[i, 0] = pair.Key;
                matrix[i, 1] = pair.Value;
                i++;
            }
            MinHeapSort(matrix);
            IList<int> tops = new List<int>();
            for (int j = 0; j < k; j++)
            {
                tops.Add(matrix[j, 0]);
            }
            return tops;
        }

        private void MinHeapSort(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                MinHeapify(matrix, i, n);
            }
            for (int i = n - 1; i > 0; i--)
            {
                Exchange(matrix, 0, i);
                MinHeapify(matrix, 0, i);
            }
        }

        private void MinHeapify(int[,] matrix, int i, int heapSize)
        {
            while (i < heapSize)
            {
                int min = i;
                int left = (i + 1) * 2 - 1;
                int right = (i + 1) * 2;
                if (left < heapSize && matrix[left, 1] < matrix[min, 1])
                {
                    min = left;
                }
                if (right < heapSize && matrix[right, 1] < matrix[min, 1])
                {
                    min = right;
                }
                if (min != i)
                {
                    Exchange(matrix, min, i);
                    i = min;
                }
                else
                {
                    break;
                }
            }
        }

        private void Exchange(int[,] matrix, int i, int j)
        {
            int x = matrix[i, 0];
            int y = matrix[i, 1];
            matrix[i, 0] = matrix[j, 0];
            matrix[i, 1] = matrix[j, 1];
            matrix[j, 0] = x;
            matrix[j, 1] = y;
        }
    }
}