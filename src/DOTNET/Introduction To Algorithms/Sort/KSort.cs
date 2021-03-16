using System;

namespace Sort
{
    public class KSort
    {
        public void Run()
        {
            int[] A = Make(10, 0, 20);
            AvgSort avgSort = new AvgSort();
            avgSort.Sort(A, 3);
            // A = new int[] { 1, 8, 3, 9, 14, 3, 11, 15, 11, 15 };
            Console.WriteLine(string.Join(',', A));
            Sort(A, 3);
            Console.WriteLine(string.Join(',', A));
        }

        public void Sort(int[] A, int k)
        {
            int[] B = new int[A.Length];
            MinPriorityQueue priorityQueue = new MinPriorityQueue(A, k);
            int j = 0;
            while (j < A.Length)
            {
                int min = priorityQueue.ExtractMin();
                B[j++] = A[min];
                if (min + k < A.Length)
                {
                    priorityQueue.Insert(min + k);
                }
            }
            for (int i = 0; i < A.Length; i++)
            {
                A[i] = B[i];
            }
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

    public class MinPriorityQueue
    {
        private int[] _A;
        public int[] _heap;
        public int _heapSize;

        public MinPriorityQueue(int[] A, int k)
        {
            _heap = new int[k + 1];
            _A = A;
            for (int i = 0; i < k; i++)
            {
                Insert(i);
            }
        }

        public void Insert(int i)
        {
            _heapSize++;
            _heap[_heapSize] = i;
            MinHeapifyUp(_heapSize, i);
        }

        public int ExtractMin()
        {
            if (_heapSize < 1)
            {
                throw new Exception("Heap is empty");
            }

            int min = _heap[1];
            _heap[1] = _heap[_heapSize];
            _heapSize--;

            MinHeapify(1);
            return min;
        }

        public void MinHeapify(int i)
        {
            int smallest = i;

            int left = Left(i);
            if (left <= _heapSize && _A[_heap[smallest]] > _A[_heap[left]])
            {
                smallest = left;
            }

            int right = Right(i);
            if (right <= _heapSize && _A[_heap[smallest]] > _A[_heap[right]])
            {
                smallest = right;
            }

            if (smallest != i)
            {
                int temp = _heap[i];
                _heap[i] = _heap[smallest];
                _heap[smallest] = temp;
                MinHeapify(smallest);
            }
        }

        private void MinHeapifyUp(int i, int key)
        {
            while (i > 1 && _A[key] < _A[_heap[Parent(i)]])
            {
                _heap[i] = _heap[Parent(i)];
                i = Parent(i);
            }
            _heap[i] = key;
        }

        private int Parent(int i)
        {
            return i / 2;
        }

        private int Left(int i)
        {
            return i * 2;
        }

        private int Right(int i)
        {
            return i * 2 + 1;
        }
    }
}