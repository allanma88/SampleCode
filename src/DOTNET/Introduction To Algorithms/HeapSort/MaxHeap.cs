using System.Collections.Generic;

namespace HeapSort
{
    public class MaxHeap
    {
        public void MaxHeapifyLoop(Heap<int> A, int i)
        {
            int largest = i;
            while (!A.IsLeaf(largest))
            {
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
                }
                else
                {
                    break;
                }
            }
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

        public void BuildMaxHeap(Heap<int> A)
        {
            for (int i = A.HeapSize / 2; i >= 1; i--)
            {
                MaxHeapify(A, i);
            }
        }

        public void HeapSort(Heap<int> A)
        {
            BuildMaxHeap(A);

            for (int i = A.Length; i > 1; i--)
            {
                int temp = A[1];
                A[1] = A[i];
                A[i] = temp;
                A.HeapSize--;

                MaxHeapify(A, 1);
            }
        }

        public bool Verify(Heap<int> A)
        {
            for (int i = A.HeapSize; i > 1; i--)
            {
                if (A[i] > A[A.Parent(i)])
                {
                    return false;
                }
            }
            return true;
        }
    }

    public class Heap<T> : List<T>
    {
        public Heap(int len)
        {
            AddRange(new T[len+1]);
        }

        public Heap(T[] A)
        {
            Add(default(T));
            AddRange(A);
            HeapSize = A.Length;
        }

        public int Length
        {
            get
            {
                return Count - 1;
            }
        }

        public int HeapSize { get; set; }

        public int Parent(int i)
        {
            return i / 2;
        }

        public int Left(int i)
        {
            return i * 2;
        }

        public int Right(int i)
        {
            return i * 2 + 1;
        }

        public bool IsLeaf(int i)
        {
            return Left(i) > HeapSize && Right(i) > HeapSize;
        }

        public T[] GetArray()
        {
            T[] a = new T[Length];
            for (int i = 0; i < Length; i++)
            {
                a[i] = this[i + 1];
            }
            return a;
        }

        public T[] GetHeap()
        {
            T[] a = new T[HeapSize];
            for (int i = 1; i <= HeapSize; i++)
            {
                a[i - 1] = this[i];
            }
            return a;
        }
    }
}