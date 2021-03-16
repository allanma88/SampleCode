namespace SortResearch
{
    public class MergeSort : SortBase
    {
        private int[] aud;

        public override void Sort(int[] a)
        {
            aud = new int[a.Length];
            Sort(a, 0, a.Length - 1);
        }

        private void Sort(int[] a, int low, int high)
        {
            if (low >= high)
            {
                return;
            }
            int middle = (low + high) / 2;
            Sort(a, low, middle);
            Sort(a, middle + 1, high);
            if(Compare(a, middle+1, middle))
            {
                Merge(a, low, middle, high);
            }
        }

        private void Merge(int[] a, int low, int middle, int high)
        {
            int i = low;
            int j = middle + 1;
            for (int k = low; k <= high; k++)
            {
                // aud[k] = a[k];
                Set(aud, k, Access(a,k));
            }
            for (int k = low; k <= high; k++)
            {
                if (i > middle)
                {
                    // a[k] = aud[j++];
                    Set(a, k, Access(aud, j++));
                }
                else if (j > high)
                {
                    // a[k] = aud[i++];
                    Set(a, k, Access(aud, i++));
                }
                // else if (aud[i] < aud[j])
                else if (Compare(aud, i, j))
                {
                    // a[k] = aud[i++];
                    Set(a, k, Access(aud, i++));
                }
                else
                {
                    // a[k] = aud[j++];
                    Set(a, k, Access(aud, j++));
                }
            }
        }
    }
}