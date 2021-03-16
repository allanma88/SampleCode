using System;
using System.Text;

namespace Heap
{
    public class Closest
    {
        public void Run()
        {
            int[][] points = MakePoints(5);
            int n = 3;
            int[][] closest = KClosest(points, n);
            Console.WriteLine($"{ToString(points)} closest {n} is {ToString(closest)}");
        }

        public int[][] KClosest(int[][] points, int k)
        {
            int[][] heap = new int[k][];
            for (int i = 0; i < k; i++)
            {
                heap[i] = new int[2];
            }
            for (int i = 0; i < points.Length; i++)
            {
                Insert(heap, points[i][0], points[i][1]);
            }
            return heap;
        }

        private int heapSize;

        private void Insert(int[][] heap, int x, int y)
        {
            if (heapSize < heap.Length)
            {
                heapSize++;
                heap[heapSize - 1][0] = x;
                heap[heapSize - 1][1] = y;
                MaxHeapifyUp(heap, heapSize - 1);
            }
            else if (x * x + y * y < Distance(heap, 0))
            {
                heap[0][0] = x;
                heap[0][1] = y;
                MaxHeapify(heap, 0);
            }
        }

        private void MaxHeapifyUp(int[][] heap, int i)
        {
            while (i > 0)
            {
                int parent = (i + 1) / 2 - 1;
                if (Distance(heap, i) > Distance(heap, parent))
                {
                    Exchange(heap, i, parent);
                    i = parent;
                }
                else
                {
                    break;
                }
            }
        }

        private void MaxHeapify(int[][] heap, int i)
        {
            while (i < heap.Length)
            {
                int max = i;
                int left = (i + 1) * 2 - 1;
                int right = (i + 1) * 2;
                if (left < heap.Length && Distance(heap, left) > Distance(heap, max))
                {
                    max = left;
                }
                if (right < heap.Length && Distance(heap, right) > Distance(heap, max))
                {
                    max = right;
                }
                if (max != i)
                {
                    Exchange(heap, i, max);
                    i = max;
                }
                else
                {
                    break;
                }
            }
        }

        private int Distance(int[][] heap, int i)
        {
            return heap[i][0] * heap[i][0] + heap[i][1] * heap[i][1];
        }

        private void Exchange(int[][] heap, int i, int j)
        {
            int tempX = heap[i][0];
            int tempY = heap[i][1];
            heap[i][0] = heap[j][0];
            heap[i][1] = heap[j][1];
            heap[j][0] = tempX;
            heap[j][1] = tempY;
        }

        private int[][] MakePoints(int n)
        {
            int[][] points = new int[n][];

            for (int i = 0; i < n; i++)
            {
                points[i] = new int[2];
            }

            points[0][0] = -1;
            points[0][1] = -2;

            points[1][0] = 2;
            points[1][1] = 3;

            points[2][0] = -4;
            points[2][1] = 7;

            points[3][0] = 11;
            points[3][1] = 5;

            points[4][0] = 6;
            points[4][1] = -9;

            return points;
        }

        private string ToString(int[][] points)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("[ ");
            for (int i = 0; i < points.Length; i++)
            {
                builder.Append("[");
                builder.Append(points[i][0]);
                builder.Append(",");
                builder.Append(points[i][1]);
                builder.Append("] ");
            }
            builder.Append("]");
            return builder.ToString();
        }
    }
}