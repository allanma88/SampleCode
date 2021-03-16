using System;
using System.Collections.Generic;

namespace Dp
{
    public class LISSample
    {
        public void Run()
        {
            // int[] nums = new int[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 7, 11, 15 };
            // int[] nums = new int[] { 0, 8, 4, 12, 2, 10, 6, 11, 1 };
            int[] nums = new int[] { 0, 8, 4, 12, 2, 10 };

            //0, 8, 4, 12, 2, 10, 6, 11, 1
            //0, 1, 2, 4,  6, 8,  10,11, 12
            LIS lis = new LIS();
            int len = lis.Find(nums);
            Console.WriteLine($"Find: {string.Join(",", nums)} lis is {len}");
            len = lis.FindWithGreedy(nums);
            Console.WriteLine($"FindWithGreedy: {string.Join(",", nums)} lis is {len}");
            len = lis.FindWithLcs(nums);
            Console.WriteLine($"FindWithLcs: {string.Join(",", nums)} lis is {len}");
            len = lis.FindContinuous(nums);
            Console.WriteLine($"FindContinuous: {string.Join(",", nums)} lis is {len}");
        }
    }

    public class LIS
    {
        public int FindWithRecur(int[] nums)
        {
            throw new NotImplementedException();
        }

        public int Find(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;
            int[] dp = new int[nums.Length];
            dp[0] = 1;
            int longest = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                int max = 1;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (nums[i] >= nums[j])
                    {
                        max = Math.Max(max, dp[j] + 1);
                    }
                }
                dp[i] = max;
                longest = Math.Max(longest, max);
            }
            return longest;
        }

        public int FindWithGreedy(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;
            int[] dp = new int[nums.Length];
            int len = 1;
            dp[0] = nums[0];
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] >= dp[len - 1])
                {
                    dp[len++] = nums[i];
                }
                else
                {
                    int j = BinarySearch(dp, 0, len - 1, nums[i]);
                    dp[j] = nums[i];
                }
            }
            return len;
        }

        private int BinarySearch(int[] values, int lo, int hi, int target)
        {
            while (lo <= hi)
            {
                int mid = (lo + hi) / 2;
                if (target < values[mid])
                {
                    hi = mid - 1;
                }
                else if (target > values[mid])
                {
                    lo = mid + 1;
                }
                else
                {
                    return mid;
                }
            }
            return lo;
        }

        public int FindWithLcs(int[] nums)
        {
            int[] newNums = new int[nums.Length];
            Array.Copy(nums, newNums, nums.Length);
            Array.Sort(newNums);
            return Lcs(nums, newNums);
        }

        private int Lcs(int[] s, int[] t)
        {
            int[,] dp = new int[s.Length + 1, t.Length + 1];
            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    if (s[i - 1] == t[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                    }
                }
            }
            return dp[s.Length, t.Length];
        }

        public int FindContinuous(int[] nums)
        {
            if (nums == null || nums.Length == 0) return 0;
            int[] dp = new int[nums.Length];
            dp[0] = 1;
            int longest = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] >= nums[i - 1])
                {
                    dp[i] = dp[i - 1] + 1;
                }
                else
                {
                    dp[i] = 1;
                }
                longest = Math.Max(longest, dp[i]);
            }
            return longest;
        }
    }
}