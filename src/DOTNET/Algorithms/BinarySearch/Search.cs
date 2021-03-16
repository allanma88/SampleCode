using System;

namespace BinarySearch
{
    public class Search
    {
        public int SearchEqual(int[] numbers, int target)
        {
            if(numbers == null) return -1;
            int lo = 0, hi = numbers.Length - 1;
            while (lo <= hi)
            {
                int mid = (hi - lo) / 2 + lo;
                if (numbers[mid] == target) return mid;
                else if (numbers[mid] < target) lo = mid + 1;
                else hi = mid - 1;
            }
            return -1;
        }

        // min(>=)
        public int SearchNotLessThan(int[] numbers, int target)
        {
            if(numbers == null || numbers.Length == 0 || numbers[numbers.Length-1] < target) return -1;
            int lo = 0, hi = numbers.Length - 1;
            while (lo < hi)
            {
                int mid = (hi - lo) / 2 + lo;
                if (numbers[mid] >= target)
                {
                    hi = mid;
                }
                else
                {
                    lo = mid + 1;
                }
            }
            return lo;
        }

        // max(<)
        public int SearchLessThan(int[] numbers, int target)
        {
            if(numbers == null || numbers.Length == 0 || numbers[0] >= target) return -1;
            int lo = 0, hi = numbers.Length - 1;
            while (lo < hi)
            {
                int mid = (hi - lo + 1) / 2 + lo;
                if (numbers[mid] < target)
                {
                    lo = mid;
                }
                else
                {
                    hi = mid - 1;
                }
            }
            return lo;
        }

        // max(<=)
        public int SearchNotGreaterThan(int[] numbers, int target)
        {
            if(numbers == null || numbers.Length == 0 || numbers[0] > target) return -1;
            int lo = 0, hi = numbers.Length - 1;
            while (lo < hi)
            {
                int mid = (hi - lo + 1) / 2 + lo;
                if (numbers[mid] <= target)
                {
                    lo = mid;
                }
                else
                {
                    hi = mid - 1;
                }
            }
            return lo;
        }

        // min(>)
        public int SearchGreaterThan(int[] numbers, int target)
        {
            if(numbers == null || numbers.Length == 0 || numbers[numbers.Length-1] <= target) return -1;
            int lo = 0, hi = numbers.Length - 1;
            while (lo < hi)
            {
                int mid = (hi - lo) / 2 + lo;
                if (numbers[mid] > target)
                {
                    hi = mid;

                }
                else
                {
                    lo = mid + 1;
                }
            }
            return lo;
        }
    }
}