using System;
using System.Collections.Generic;

namespace HeapSort
{
    class Program
    {
        static void Main(string[] args)
        {
            // MaxHeap maxHeap = new MaxHeap();
            // HeapSort(maxHeap);
            // Verify(maxHeap);
            // MaxHeapify(maxHeap);
            // BuildMaxHeap(maxHeap);

            // MaxPriorityQueue priorityQueue = new MaxPriorityQueue();
            // ExtractMax(priorityQueue);
            // IncreaseKey(priorityQueue);
            // Delete(priorityQueue);

            ListMerge merge = new ListMerge();
            merge.Run();

            // DHeap();
            // YoungTableau();

            // TopK();
        }
        
        public static void TopK()
        {
            Top top = new Top();
            int[] nums = new int[] { 3, 6, 1, 10, 7, 16 };
            int[] tops = top.TopKs(nums, 4);
            Console.WriteLine(string.Join(',', tops));
        }


        private static void DHeap()
        {
            int[] nums = new int[] { 18, 14, 16, 15, 17, 13, 12, 11, 10, 9, 8 };
            DMaxHeap dHeap = new DMaxHeap(nums, 3);
            dHeap.MaxHeapify(2);
            Console.WriteLine($"{string.Join(',', dHeap.GetHeap())}");
            int max = dHeap.ExtractMax();
            Console.WriteLine($"{max}, {string.Join(',', dHeap.GetHeap())}");
            dHeap.Insert(18);
            Console.WriteLine($"{string.Join(',', dHeap.GetHeap())}");
            dHeap.IncreaseKey(5, 19);
            Console.WriteLine($"{string.Join(',', dHeap.GetHeap())}");
        }

        private static void YoungTableau()
        {
            int[,] matrix = new int[3, 3]
            {
                {1, 5, 9},
                {2, 6, 10},
                {3, 7, 11}
            };
            YoungTableau tableau = new YoungTableau(matrix, 8);
            int min = tableau.ExtractMin();
            Console.WriteLine($"min: {min}, tableau: \n{tableau.ToString()}");
            tableau.Insert(8);
            Console.WriteLine($"tableau: \n{tableau.ToString()}");
            bool exist = tableau.Search(9);
            Console.WriteLine($"9 exist: {exist}");
            exist = tableau.Search(4);
            Console.WriteLine($"4 exist: {exist}");
        }

        private static List<int> Merge(List<int>[] lists, int k)
        {
            MaxPriorityQueue priorityQueue = new MaxPriorityQueue();
            Heap<int> heap = new Heap<int>(k);
            List<int> merged = new List<int>();

            List<Queue<int>> queues = new List<Queue<int>>();
            foreach (var list in lists)
            {
                Queue<int> queue = new Queue<int>(list);
                queues.Add(queue);
            }

            foreach (var queue in queues)
            {
                int n = queue.Dequeue();
                priorityQueue.Insert(heap, n);
            }

            int i = 0;
            while (queues.Count > 0)
            {
                if (queues[i].Count == 0)
                {
                    queues.RemoveAt(i);
                    continue;
                }
                int max = priorityQueue.ExtractMax(heap);
                merged.Add(max);
                int n = queues[i].Dequeue();
                priorityQueue.Insert(heap, n);
                i = (++i) % queues.Count;
            }

            while (heap.HeapSize > 0)
            {
                int remain = priorityQueue.ExtractMax(heap);
                merged.Add(remain);
            }

            return merged;
        }

        private static void BuildMaxHeap(MaxHeap maxHeap)
        {
            int[] nums = new int[] { 5, 3, 17, 10, 84, 19, 6, 22, 9 };
            Heap<int> heap = new Heap<int>(nums);
            maxHeap.BuildMaxHeap(heap);
            Console.WriteLine($"{string.Join(',', nums)} Max Heap is: {string.Join(',', heap.GetArray())} ");
        }

        private static void MaxHeapify(MaxHeap maxHeap)
        {
            int[] nums = new int[] { 27, 17, 3, 16, 13, 10, 1, 5, 7, 12, 4, 8, 9, 0 };
            Heap<int> heap = new Heap<int>(nums);
            maxHeap.MaxHeapify(heap, 3);
            Console.WriteLine($"{string.Join(',', nums)} MaxHeapify 3: {string.Join(',', heap.GetArray())} ");
        }

        private static void Verify(MaxHeap maxHeap)
        {
            int[] nums = new int[] { 23, 17, 14, 6, 13, 10, 1, 5, 7, 12 };
            Heap<int> heap = new Heap<int>(nums);
            bool isMaxHeap = maxHeap.Verify(heap);
            Console.WriteLine($"{string.Join(',', heap.GetArray())} is max heap: {isMaxHeap}");
        }

        private static void HeapSort(MaxHeap maxHeap)
        {
            int[] nums = new int[] { 5, 13, 2, 25, 7, 17, 20, 8, 4 };
            Heap<int> heap = new Heap<int>(nums);
            maxHeap.HeapSort(heap);
            Console.WriteLine(string.Join(',', heap.GetArray()));
        }

        private static void ExtractMax(MaxPriorityQueue priorityQueue)
        {
            int[] nums = new int[] { 15, 13, 9, 5, 12, 8, 7, 4, 0, 6, 2, 1 };
            Heap<int> heap = new Heap<int>(nums);
            int max = priorityQueue.ExtractMax(heap);
            Console.WriteLine($"{string.Join(',', nums)} max is {max}, heap is {string.Join(',', heap.GetHeap())}");
        }

        private static void IncreaseKey(MaxPriorityQueue priorityQueue)
        {
            int[] nums = new int[] { 15, 13, 9, 5, 12 };
            Heap<int> heap = new Heap<int>(nums);
            priorityQueue.IncreaseKey(heap, 4, 16);
            Console.WriteLine($"{string.Join(',', nums)} increase 4 to 16 is {string.Join(',', heap.GetArray())}");
        }

        private static void Delete(MaxPriorityQueue priorityQueue)
        {
            int[] nums = new int[] { 15, 13, 9, 5, 12 };
            Heap<int> heap = new Heap<int>(nums);
            priorityQueue.Delete(heap, 3);
            Console.WriteLine($"{string.Join(',', nums)} delete 3 is {string.Join(',', heap.GetHeap())}");
        }
    }
}