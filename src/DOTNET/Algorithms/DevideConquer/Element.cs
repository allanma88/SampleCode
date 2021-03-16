using System;

namespace DevideConquer
{
    public class Element
    {
        public void Run()
        {
            int[] nums = new int[] { 3, 2, 3 };
            int majority = MajorityElement(nums);
            Console.WriteLine($"{string.Join(",", nums)}: {majority}");

            nums = new int[] { 2, 2, 1, 1, 1, 2, 2 };
            majority = MajorityElement(nums);
            Console.WriteLine($"{string.Join(",", nums)}: {majority}");

            nums = new int[] { -1, 1, 1, 1, 1, 2 };
            majority = MajorityElement(nums);
            Console.WriteLine($"{string.Join(",", nums)}: {majority}");
        }

        public int MajorityElement(int[] nums)
        {
            Array.Sort(nums);

            int n = nums.Length;
            int mid = n / 2;
            int first = -1, second = -1;
            if (n % 2 == 0)
            {
                first = mid;
                second = mid - 1;
            }
            else
            {
                first = second = mid;
            }

            if (CheckSame(nums, 0, first))
            {
                return nums[0];
            }
            if (CheckSame(nums, second, n - 1))
            {
                return nums[n - 1];
            }
            if (CheckSameOfMid(nums))
            {
                return nums[mid];
            }
            return -1;
        }

        private bool CheckSame(int[] nums, int lo, int hi)
        {
            if (lo == hi)
            {
                return true;
            }
            int mid = (lo + hi) / 2;
            if (CheckSame(nums, lo, mid) && CheckSame(nums, mid + 1, hi))
            {
                return nums[lo] == nums[hi];
            }
            return false;
        }

        private bool CheckSameOfMid(int[] nums)
        {
            int mid = nums.Length / 2;
            int i;
            for (i = mid - 1; i >= 0; i--)
            {
                if (nums[i] != nums[mid])
                {
                    break;
                }
            }
            int j;
            for (j = mid + 1; j < nums.Length; j++)
            {
                if (nums[j] != nums[mid])
                {
                    break;
                }
            }
            return j - i + 1 > nums.Length / 2;
        }
    }
}