using System;

namespace GraphResearch
{
    public class MinPQ<T> where T : IComparable<T>
    {
        private T[] pq;

        private int len;

        public MinPQ(int max)
        {
            pq = new T[max + 1];
        }

        public void Insert(T item)
        {
            pq[++len] = item;
            Swim(len);
        }

        public T RemoveMin()
        {
            if (len <= 0)
            {
                throw new InvalidOperationException("pq is empty, can not remove");
            }
            T min = pq[1];
            pq[1] = pq[len];
            pq[len] = min;
            pq[len] = default(T);
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
            while (k > 1 && pq[k].CompareTo(pq[k / 2]) < 0)
            {
                T temp = pq[k];
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
                if (j + 1 < len && pq[j + 1].CompareTo(pq[j]) < 0)
                {
                    j = j + 1;
                }
                if (pq[k].CompareTo(pq[j]) > 0)
                {
                    T temp = pq[k];
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
}