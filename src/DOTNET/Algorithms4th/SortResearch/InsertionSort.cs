namespace SortResearch
{
    public class InsertionSort : SortBase
    {
        //最好情况下：compare: n-1, exchange: 0
        public override void Sort(int[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if(Compare(a, j, j-1))
                    {
                        Exchange(a, j, j-1);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        //最好情况下：compare: 1+2+...+ (n-1) = (n-1)*(n-2)/2, exchange: 0
        public void MySort(int[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (a[i] < a[j])
                    {
                        int temp = a[i];
                        a[i] = a[j];
                        a[j] = temp;
                    }
                }
            }
        }
    }
}