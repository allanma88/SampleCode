namespace SortResearch
{
    public class ShellSort : SortBase
    {
        public override void Sort(int[] a)
        {
            // Sort a[] into increasing order.
            int N = a.Length;
            int h = 1;
            // 1, 4, 13, 40, 121, 364, 1093, ...
            while (h < N / 3)
            {
                h = 3 * h + 1;
            }
            while (h >= 1)
            {
                // h-sort the array.
                for (int i = h; i < N; i++)
                {
                    // Insert a[i] among a[i-h], a[i-2*h], a[i-3*h]... .
                    for (int j = i; j >= h; j -= h)
                    {
                        if(Compare(a, j-h, j))
                        {
                            break;
                        }
                        else
                        {
                            Exchange(a,j,j-h);
                        }
                    }
                }
                h = h / 3;
            }
        }

        public void MySort(int[] a)
        {
            int length = a.Length;
            while (length > 1)
            {
                length = length / 2;
                for (int i = 0; i < length; i++)
                {
                    for (int j = i + length; j < a.Length; j += length)
                    {
                        for (int k = i; k < j; k += length)
                        {
                            if (a[k] > a[j])
                            {
                                int temp = a[k];
                                a[k] = a[j];
                                a[j] = temp;
                            }
                        }
                    }
                }
            }
        }
    }
}