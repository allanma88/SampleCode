namespace SortResearch
{
    public class SelectionSort : SortBase
    {
        public override void Sort(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                int min = i;
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (Compare(a, j, min))
                    {
                        min = j;
                    }
                }
                Exchange(a, i, min);
            }
        }
    }
}