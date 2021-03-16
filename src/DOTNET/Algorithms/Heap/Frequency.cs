using System;
using System.Collections.Generic;
using System.Text;

namespace Heap
{
    public class Frequency
    {
        public void Run()
        {
            string s = FrequencySort("tree");
            Console.WriteLine(s);

            s = FrequencySort("cccaaa");
            Console.WriteLine(s);

            s = FrequencySort("Aabb");
            Console.WriteLine(s);

             s = FrequencySort("abaccadeeefaafcc");
            Console.WriteLine(s);
        }

        public string FrequencySort(string s)
        {
            Dictionary<char, int> counts = new Dictionary<char, int>();
            foreach (var c in s)
            {
                if (counts.ContainsKey(c))
                {
                    counts[c]++;
                }
                else
                {
                    counts[c] = 1;
                }
            }
            int[,] matrix = new int[counts.Count, 2];
            int i = 0;
            foreach (var pair in counts)
            {
                matrix[i, 0] = pair.Key;
                matrix[i, 1] = pair.Value;
                i++;
            }
            MinHeapSort(matrix);
            StringBuilder builder = new StringBuilder();
            for (int j = 0; j < matrix.GetLength(0); j++)
            {
                char c = (char)matrix[j, 0];
                for (int k = 0; k < matrix[j, 1]; k++)
                {
                    builder.Append(c);
                }
            }
            return builder.ToString();
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