namespace Multiway
{
    public class IndexHeapMinPQ
    {
        private int[] pq;
        private int[] qp;
        private int[] keys;
        private int len;

        public IndexHeapMinPQ(int max)
        {
            pq = new int[max + 1];
            keys = new int[max + 1];
            qp = new int[max + 1];
            for (int i = 0; i < max + 1; i++)
            {
                qp[i] = -1;
            }
        }

        public void Insert(int k, int item)
        {
            len++;
            qp[k] = len;
            pq[len] = k;
            keys[k] = item;
            Swim(len);
        }

        public int Min()
        {
            return keys[pq[1]];
        }

        public int RemoveMin()
        {
            int minIndex = pq[1];
            Exch(1, len--);
            Sink(1);
            keys[minIndex] = 0;
            qp[minIndex] = -1;
            return minIndex;
        }

        public void Change(int k, int item)
        {
            keys[k] = item;
            Swim(qp[k]);
            Sink(qp[k]);
        }

        public bool Contains(int k)
        {
            return qp[k] != -1;
        }

        public void Delete(int k)
        {
            //may by Exch(k, len--);
            Exch(qp[k], len--);
            Swim(qp[k]);
            Sink(qp[k]);
            keys[pq[len + 1]] = 0;
            qp[pq[len + 1]] = -1;
        }

        public int MinIndex()
        {
            return pq[1];
        }

        public int Get(int k)
        {
            return keys[k];
        }

        public int Size()
        {
            return len;
        }

        public bool IsEmpty()
        {
            return len <= 0;
        }

        private void Swim(int k)
        {
            while (k > 1 && Less(k, k / 2))
            {
                Exch(k, k / 2);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= len)
            {
                int j = 2 * k;
                if (j + 1 < len && Less(j + 1, j))
                {
                    j = j + 1;
                }
                if (Less(j, k))
                {
                    Exch(j, k);
                }
                else
                {
                    break;
                }
                k = j;
            }
        }

        private bool Less(int i, int j)
        {
            return keys[pq[i]] < keys[pq[j]];
        }

        private void Exch(int i, int j)
        {
            int temp = pq[i];
            pq[i] = pq[j];
            pq[j] = temp;

            qp[pq[i]] = i;
            qp[pq[j]] = j;
        }
    }
}