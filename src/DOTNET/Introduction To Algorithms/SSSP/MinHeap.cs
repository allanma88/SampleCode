using System;

namespace SSSP
{
    public class MinHeap
    {
        private int _heapsize;
        private int[] _V;
        private int[] _D;
        private int[] _I;

        public MinHeap(int[] V, int[] D)
        {
            _heapsize = V.Length;

            _V = new int[_heapsize];
            Array.Copy(V, _V, _heapsize);

            _D = new int[_heapsize];
            Array.Copy(D, _D, _heapsize);

            _I = new int[_heapsize];
            for (int i = 0; i < _heapsize; i++)
            {
                _I[_V[i]] = i;
            }

            for (int i = _heapsize / 2 - 1; i >= 0; i--)
            {
                MinHeapify(i);
            }
        }
        
        public int ExtractMin()
        {
            if (_heapsize < 0)
            {
                throw new Exception("Heap is empty");
            }

            int min = _V[0];
            _V[0] = _V[--_heapsize];

            MinHeapify(0);

            return min;
        }

        public void Decrease(int v, int d)
        {
            int i = _I[v];
            if (d < _D[i])
            {
                return;
            }

            _D[i] = d;
            while (i > 0)
            {
                int parent = (i + 1) / 2 - 1;
                if (_D[_V[i]] < _D[_V[parent]])
                {
                    Exchange(i, parent);
                    i = parent;
                }
                else
                {
                    break;
                }
            }
        }

        public bool IsEmpty
        {
            get
            {
                return _heapsize == 0;
            }
        }

        private void MinHeapify(int i)
        {
            int smallest = i;
            while (smallest < _heapsize)
            {
                int left = (smallest + 1) * 2 - 1;
                if (left < _heapsize && _D[_V[left]] < _D[_V[smallest]])
                {
                    smallest = left;
                }

                int right = (smallest + 1) * 2;
                if (right < _heapsize && _D[_V[right]] < _D[_V[smallest]])
                {
                    smallest = right;
                }

                if (smallest != i)
                {
                    Exchange(i, smallest);
                    smallest = i;
                }
                else
                {
                    break;
                }
            }
        }

        private void Exchange(int i, int j)
        {
            int temp = _V[i];
            _V[i] = _V[j];
            _V[j] = temp;

            _I[_V[i]] = i;
            _I[_V[j]] = j;
        }
    }
}