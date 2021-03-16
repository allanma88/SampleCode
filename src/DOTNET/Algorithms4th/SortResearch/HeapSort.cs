using System;

namespace SortResearch
{
    public class HeapSort : SortBase
    {
        public void Sort1(int[] a)
        {
            //IMinPQ pg = new MinUnorderArrayPQ(a.Length);
            //IMinPQ pg = new MinOrderArrayPQ(a.Length);
            IMinPQ pg = new MinHeapPQ(a.Length);
            for (int i = 0; i < a.Length; i++)
            {
                pg.Insert(a[i]);
            }
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = pg.RemoveMin();
            }
        }

        public override void Sort(int[] a)
        {
            int N = a.Length;
            for (int k = N / 2; k >= 1; k--)
            {
                Sink(a, k, N);
            }
            while (N > 1)
            {
                Exch(a, 1, N--);
                Sink(a, 1, N);
            }
        }

        private void Sink(int[] pq, int k, int len)
        {
            while (2 * k <= len)
            {
                int j = 2 * k;
                if (j + 1 <= len && Compare(pq, j + 1, j))
                {
                    j = j + 1;
                }
                if (Compare(pq, j, k))
                {
                    Exch(pq, j, k);
                }
                else
                {
                    break;
                }
                k = j;
            }
        }

        private bool Compare(int[] pq, int i, int j)
        {
            i--;
            j--;
            return pq[i] > pq[j];
        }

        private void Exch(int[] pq, int i, int j)
        {
            i--;
            j--;
            int temp = pq[i];
            pq[i] = pq[j];
            pq[j] = temp;
        }
    }

    public interface IMinPQ
    {
        void Insert(int item);

        int RemoveMin();

        int Size();

        bool IsEmpty();
    }

    public class MinHeapPQ : IMinPQ
    {
        private int[] pq;

        private int len;

        public MinHeapPQ(int max)
        {
            pq = new int[max + 1];
        }

        public void Insert(int item)
        {
            pq[++len] = item;
            Swim(len);
        }

        public int RemoveMin()
        {
            if (len <= 0)
            {
                throw new InvalidOperationException("pq is empty, can not remove");
            }
            int min = pq[1];
            pq[1] = pq[len];
            pq[len] = min;
            pq[len] = 0;
            len--;
            Sink(1);
            return min;
        }

        public int Size()
        {
            return len - 1;
        }

        public bool IsEmpty()
        {
            return len <= 0;
        }

        private void Swim(int k)
        {
            while (k > 1 && pq[k] < pq[k / 2])
            {
                int temp = pq[k];
                pq[k] = pq[k / 2];
                pq[k / 2] = temp;
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= len)
            {
                int j = 2 * k;
                if (j + 1 < len && pq[j + 1] < pq[j])
                {
                    j = j + 1;
                }
                if (pq[k] > pq[j])
                {
                    int temp = pq[k];
                    pq[k] = pq[j];
                    pq[j] = temp;
                }
                else
                {
                    break;
                }
                k = j;
            }
        }
    }

    public class MinUnorderArrayPQ : IMinPQ
    {
        private int[] a;

        private int len;

        public MinUnorderArrayPQ(int max)
        {
            a = new int[max];
        }

        public void Insert(int i)
        {
            a[len++] = i;
        }

        public int RemoveMin()
        {
            if (len <= 0)
            {
                throw new InvalidOperationException("pq is empty, can not remove");
            }
            int min = 0;
            for (int i = 1; i < len; i++)
            {
                if (a[i] < a[min])
                {
                    min = i;
                }
            }
            int minValue = a[min];
            a[min] = a[len - 1];
            a[len - 1] = minValue;
            len--;
            return minValue;
        }

        public int Size()
        {
            return len;
        }

        public bool IsEmpty()
        {
            return len <= 0;
        }
    }

    public class MinOrderArrayPQ : IMinPQ
    {
        private int[] a;

        private int len;

        public MinOrderArrayPQ(int max)
        {
            a = new int[max];
        }

        public void Insert(int i)
        {
            a[len++] = i;
            for (int j = len - 1; j > 0; j--)
            {
                if (a[j] > a[j - 1])
                {
                    int temp = a[j];
                    a[j] = a[j - 1];
                    a[j - 1] = temp;
                }
                else
                {
                    break;
                }
            }

        }

        public int RemoveMin()
        {
            if (len <= 0)
            {
                throw new InvalidOperationException("pq is empty, can not remove");
            }
            return a[--len];
        }

        public int Size()
        {
            return len;
        }

        public bool IsEmpty()
        {
            return len <= 0;
        }
    }
}