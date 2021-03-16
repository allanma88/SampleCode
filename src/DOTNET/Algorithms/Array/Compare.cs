using System;

namespace Array
{
    public class CompareSample
    {
        public void Run()
        {
            Compare compare = new Compare();
            int[] values1 = new int[] { 1, 3, 3 };
            int[] values2 = new int[] { 1, 2, 3, 1 };
            bool result = compare.IsGreaterOrEqualThan(values1, values2);
            Console.WriteLine(result);
        }
    }

    public class Compare
    {
        public int CompareArray(int[] values1, int[] values2)
        {
            int len = Math.Min(values1.Length, values2.Length);
            for (int i = 0; i < len; i++)
            {
                if (values1[i] > values2[i]) return 1;
                else if (values1[i] < values2[i]) return -1;
            }
            if (values1.Length > len) return 1;
            if (values2.Length > len) return -1;
            return 0;
        }

        public bool IsGreaterOrEqualThan(int[] values1, int[] values2)
        {
            int len = Math.Min(values1.Length, values2.Length);
            for (int i = 0; i < len; i++)
            {
                if (values1[i] > values2[i]) return true;
                else if (values1[i] < values2[i]) return false;
            }
            if (values1.Length > len) return true;
            if (values2.Length > len) return false;
            return true;
        }
    }
}