using System;

namespace HeapSort
{
    public class DMaxHeap
    {
        private int[] _nums;
        private int _k;
        private int _heapSize;

        public DMaxHeap(int[] nums, int k)
        {
            _nums = new int[nums.Length + 1];
            for (int i = 0; i < nums.Length; i++)
            {
                _nums[i + 1] = nums[i];
            }
            _heapSize = nums.Length;
            _k = k;
        }

        private int Parent(int i)
        {
            return (i - 2) / _k + 1;
        }

        private int[] Children(int i)
        {
            int[] children = new int[_k];
            int right = _k * i + 1;
            for (int j = _k - 1; j >= 0; j--)
            {
                children[j] = right--;
            }
            return children;
        }

        public void MaxHeapify(int i)
        {
            int[] children = Children(i);
            int max = i;
            for (int j = 0; j < children.Length; j++)
            {
                if (children[j] > _heapSize)
                {
                    break;
                }
                if (_nums[max] < _nums[children[j]])
                {
                    max = children[j];
                }
            }
            if (max != i)
            {
                int temp = _nums[i];
                _nums[i] = _nums[max];
                _nums[max] = temp;

                MaxHeapify(max);
            }
        }

        public int ExtractMax()
        {
            int max = _nums[1];
            _nums[1] = _nums[_heapSize];
            _heapSize--;
            MaxHeapify(1);
            return max;
        }

        public void Insert(int key)
        {
            _heapSize++;
            MaxHeapifyUp(_heapSize, key);
        }

        public void IncreaseKey(int i, int key)
        {
            if (_nums[i] > key)
            {
                throw new Exception("key is too small");
            }

            MaxHeapifyUp(i, key);
        }

        private void MaxHeapifyUp(int i, int key)
        {
            while (i > 1 && key > _nums[Parent(i)])
            {
                _nums[i] = _nums[Parent(i)];
                i = Parent(i);
            }
            _nums[i] = key;
        }

        public int[] GetArray()
        {
            int[] a = new int[_nums.Length-1];
            for (int i = 0; i < _nums.Length-1; i++)
            {
                a[i] = _nums[i + 1];
            }
            return a;
        }

        public int[] GetHeap()
        {
            int[] a = new int[_heapSize];
            for (int i = 1; i <= _heapSize; i++)
            {
                a[i - 1] = _nums[i];
            }
            return a;
        }
    }
}