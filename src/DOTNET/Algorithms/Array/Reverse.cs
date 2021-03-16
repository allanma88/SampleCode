using System;

namespace Array
{
    public class ReverseSample
    {
        public void Run()
        {
            Reverse reverse = new Reverse();
            Console.WriteLine("Reverse Array: ");

            int[] values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Common.Print(values, "before: ");
            reverse.ReverseArray(values);
            Common.Print(values, " after: ");

            values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Common.Print(values, "before: ");
            reverse.ReverseArray(values);
            Common.Print(values, " after: ");

            values = new int[] { 1, 2 };
            Common.Print(values, "before: ");
            reverse.ReverseArray(values);
            Common.Print(values, " after: ");

            values = new int[] { 1 };
            Common.Print(values, "before: ");
            reverse.ReverseArray(values);
            Common.Print(values, " after: ");

            values = new int[0];
            Common.Print(values, "before: ");
            reverse.ReverseArray(values);
            Common.Print(values, " after: ");
        }
    }

    public class Reverse
    {
        public void ReverseArray(int[] values)
        {
            int i = 0, j = values.Length - 1;
            while (i < j)
            {
                Swap(values, i++, j--);
            }
        }

        private void Swap(int[] values, int i, int j)
        {
            int temp = values[i];
            values[i] = values[j];
            values[j] = temp;
        }
    }
}