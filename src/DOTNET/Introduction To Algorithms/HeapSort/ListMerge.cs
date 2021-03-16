using System;
using System.Collections.Generic;
using System.Linq;

namespace HeapSort
{
    public class ListMerge
    {
        public void Run()
        {
            int k = 3;
            List<int>[] lists = new List<int>[k];
            lists[0] = new List<int> { 1, 2, 3 };
            lists[1] = new List<int> { 4, 5, 6 };
            lists[2] = new List<int> { 7, 8, 9 };
            var merged = Merge(lists, k);
            Console.WriteLine($"{string.Join(',', merged)}");
        }

        public List<int> Merge(List<int>[] lists, int k)
        {
            MinPriorityQueue priorityQueue = new MinPriorityQueue(k);
            List<int> merged = new List<int>();

            for (int i = 0; i < k; i++)
            {
                Index index = new Index(i, 0, lists[i][0]);
                priorityQueue.Insert(index);
            }

            int j = 0;
            int len = lists.Sum(x => x.Count);
            while (j < len)
            {
                Index minIndex = priorityQueue.ExtractMin();
                merged.Add(minIndex.V);
                var list = lists[minIndex.I];
                if (minIndex.J + 1 < list.Count)
                {
                    priorityQueue.Insert(new Index(minIndex.I, minIndex.J + 1, list[minIndex.J+1]));
                }
                j++;
            }
            return merged;
        }
    }

    public class MinPriorityQueue
    {
        public Index[] _heap;
        public int _heapSize;

        public MinPriorityQueue(int k)
        {
            _heap = new Index[k + 1];
        }

        public void Insert(Index index)
        {
            _heapSize++;
            _heap[_heapSize] = index;
            MinHeapifyUp(_heapSize, index);
        }

        public Index ExtractMin()
        {
            if (_heapSize < 1)
            {
                throw new Exception("Heap is empty");
            }

            Index min = _heap[1];
            _heap[1] = _heap[_heapSize];
            _heapSize--;

            MinHeapify(1);
            return min;
        }

        public void MinHeapify(int i)
        {
            int smallest = i;

            int left = Left(i);
            if (left <= _heapSize && _heap[smallest].V > _heap[left].V)
            {
                smallest = left;
            }

            int right = Right(i);
            if (right <= _heapSize && _heap[smallest].V > _heap[right].V)
            {
                smallest = right;
            }

            if (smallest != i)
            {
                Index temp = _heap[i];
                _heap[i] = _heap[smallest];
                _heap[smallest] = temp;
                MinHeapify(smallest);
            }
        }

        private void MinHeapifyUp(int i, Index index)
        {
            while (i > 1 && index.V < _heap[Parent(i)].V)
            {
                _heap[i] = _heap[Parent(i)];
                i = Parent(i);
            }
            _heap[i] = index;
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

    public class Index
    {
        public Index(int i, int j, int v)
        {
            I = i;
            J = j;
            V = v;
        }

        public int I { get; set; }

        public int J { get; set; }

        public int V { get; set; }
    }
}