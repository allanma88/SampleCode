namespace SortResearch
{
    public abstract class SortBase
    {
        private Statistic statistic = new Statistic();
        public abstract void Sort(int[] a);

        public Statistic Statistic
        {
            get
            {
                return statistic;
            }
        }

        public void Exchange(int[] a, int i, int j)
        {
            // int temp = a[i];
            // a[i] = a[j];
            // a[j] = temp;
            int temp = Access(a, i);
            Set(a, i, Access(a, j));
            Set(a, j, temp);
            statistic.ExchangeContent++;
        }

        public bool Compare(int[] a, int i, int j)
        {
            // return a[i] < a[j];
            // return Access(a, i) < Access(a, j);
            return Compare(Access(a, i), Access(a, j));
        }

        public bool Compare(int a, int b)
        {
            statistic.CompareContent++;
            return a < b;
        }

        public int Access(int[] a, int i)
        {
            statistic.AccessContent++;
            return a[i];
        }

        public void Set(int[] a, int i, int value)
        {
            statistic.AccessContent++;
            a[i] = value;
        }


    }

    public class Statistic
    {
        public int CompareContent { get; set; }

        public int ExchangeContent { get; set; }

        public int AccessContent { get; set; }
    }
}