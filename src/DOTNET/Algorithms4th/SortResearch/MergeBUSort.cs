using System;

namespace SortResearch
{
    public class MergeBUSort : SortBase
    {
        private int[] aud;

        public override void Sort(int[] a)
        {
            int N = a.Length;
            aud = new int[N];
            for (int k = 1; k < N; k = k * 2)
            {
                for (int i = 0; i < N - k; i += 2 * k)
                {
                    Merge(a, i, i + k - 1, Math.Min(i + 2 * k - 1, N - 1));
                }
            }
        }

        private void Merge(int[] a, int low, int middle, int high)
        {
            int i = low;
            int j = middle + 1;
            for (int k = low; k <= high; k++)
            {
                aud[k] = a[k];
            }
            for (int k = low; k <= high; k++)
            {
                if (i > middle)
                {
                    a[k] = aud[j++];
                }
                else if (j > high)
                {
                    a[k] = aud[i++];
                }
                else if (aud[i] < aud[j])
                {
                    a[k] = aud[i++];
                }
                else
                {
                    a[k] = aud[j++];
                }
            }
        }
    }
}