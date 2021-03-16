using System;

namespace HeapSort
{
    public class MaxPriorityQueue
    {
        public int Maximum(Heap<int> A)
        {
            return A[1];
        }

        public int ExtractMax(Heap<int> A)
        {
            if (A.HeapSize < 1)
            {
                throw new Exception("Heap is empty");
            }

            int max = A[1];
            A[1] = A[A.HeapSize];
            A.HeapSize--;

            MaxHeapify(A, 1);
            return max;
        }

        public void IncreaseKey(Heap<int> A, int i, int key)
        {
            if (key < A[i])
            {
                throw new Exception("key is too small");
            }

            A[i] = key;
            while (i > 1 && A[i] > A[A.Parent(i)])
            {
                int temp = A[i];
                A[i] = A[A.Parent(i)];
                A[A.Parent(i)] = temp;

                i = A.Parent(i);
            }
        }

        public void OptIncreaseKey(Heap<int> A, int i, int key)
        {
            if (key < A[i])
            {
                throw new Exception("key is too small");
            }

            MaxHeapifyUp(A, i, key);
        }

        public void Insert(Heap<int> A, int key)
        {
            A.HeapSize++;
            MaxHeapifyUp(A, A.HeapSize, key);
        }

        public void Delete(Heap<int> A, int i)
        {
            A[i] = A[A.HeapSize];
            A.HeapSize--;
            MaxHeapify(A, i);
            MaxHeapifyUp(A, i, A[i]);
        }

        public void MaxHeapifyUp(Heap<int> A, int i, int key)
        {
            while (i > 1 && key > A[A.Parent(i)])
            {
                A[i] = A[A.Parent(i)];
                i = A.Parent(i);
            }
            A[i] = key;
        }

        public void MaxHeapify(Heap<int> A, int i)
        {
            int largest = i;

            int left = A.Left(i);
            if (left <= A.HeapSize && A[largest] < A[left])
            {
                largest = left;
            }

            int right = A.Right(i);
            if (right <= A.HeapSize && A[largest] < A[right])
            {
                largest = right;
            }

            if (largest != i)
            {
                int temp = A[i];
                A[i] = A[largest];
                A[largest] = temp;
                MaxHeapify(A, largest);
            }
        }
    }
}