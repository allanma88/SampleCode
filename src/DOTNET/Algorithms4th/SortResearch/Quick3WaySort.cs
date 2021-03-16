using System;

namespace SortResearch
{
    public class Quick3WaySort : SortBase
    {
        public override void Sort(int[] a)
        {
            Sort(a, 0, a.Length - 1);
        }

        private void Sort(int[] a, int lo, int hi)
        {
            if (lo >= hi)
            {
                return;
            }
            int lt = lo;
            int gt = hi;
            int i = lo + 1;
            int v = a[lo];
            while (i <= gt)
            {
                int cmp = a[i].CompareTo(v);
                if (cmp < 0)
                {
                    Exchange(a, lt++, i++);
                }
                else if (cmp > 0)
                {
                    Exchange(a, i, gt--);
                }
                else
                {
                    i++;
                }
            }
            Sort(a, lo, lt-1);
            Sort(a, gt+1, hi);
        }

        private void Exchange(int[] a, int i, int j)
        {
            int temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }
    }
}