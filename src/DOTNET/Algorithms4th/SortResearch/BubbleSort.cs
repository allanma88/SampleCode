namespace SortResearch
{
    public class BubbleSort : SortBase
    {
        public override void Sort(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = a.Length - 1; j > i; j--)
                {
                    if (Compare(a, j, j - 1))
                    {
                        Exchange(a, j, j - 1);
                    }
                }
            }
        }
    }
}