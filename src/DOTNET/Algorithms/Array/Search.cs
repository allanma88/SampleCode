using System;

namespace Array
{
    public class SearchDemo
    {
        public void Run()
        {
            Search search = new Search();
            int target = 3;
            int[] input = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            int i = search.BinarySearch(input, target);
            Console.WriteLine(i);

            input = new int[] { };
            i = search.BinarySearch(input, target);
            Console.WriteLine(i);
        }
    }

    public class Search
    {
        public int BinarySearch(int[] input, int target)
        {
            if (input.Length <= 0)
            {
                return -1;
            }
            int start = 0;
            int end = input.Length;
            while (start <= end)
            {
                int mid = start + (end - start) / 2;
                if (input[mid] == target)
                {
                    return mid;
                }
                else if (input[mid] > target)
                {
                    end = mid - 1;
                }
                else
                {
                    start = mid + 1;
                }
            }
            return -1;
        }
    }
}