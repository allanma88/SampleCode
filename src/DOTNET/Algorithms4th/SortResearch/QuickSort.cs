namespace SortResearch
{
    public class QuickSort : SortBase
    {
        private int[] _temp;

        public override void Sort(int[] a)
        {
            _temp = new int[a.Length];
            Sort(a, 0, a.Length - 1);
        }

        private void Sort(int[] a, int low, int high)
        {
            if (low >= high)
            {
                return;
            }
            int j = Partition(a, low, high);
            Sort(a, low, j - 1);
            Sort(a, j + 1, high);
        }

        private int Partition(int[] a, int low, int high)
        {
            int i = low;
            int j = high + 1;
            // int result = a[low];
            int result = Access(a, low);
            while (i < j)
            {
                // while (a[++i] < result)
                while (Compare(Access(a, ++i), result))
                {
                    if (i == high)
                    {
                        break;
                    }
                }
                // while (a[--j] > result)
                while (Compare(result, Access(a, --j)))
                {
                    //if (j == low)
                    //{
                    //    break;
                    //}
                }
                Exchange(a, i, j);
            }
            Exchange(a, j, low);
            return j;
        }

        private int Partition_Bad(int[] a, int low, int high)
        {
            for (int k = low; k < high + 1; k++)
            {
                _temp[k] = a[k];
            }
            int result = a[low];
            int i = low;
            int j = high;
            for (int k = low + 1; k < high + 1; k++)
            {
                if (_temp[k] <= result)
                {
                    a[i++] = _temp[k];
                }
                else if (_temp[k] > result)
                {
                    a[j--] = _temp[k];
                }
            }
            a[j] = result;
            return j;
        }
    }
}