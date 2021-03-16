using System;
using System.Collections.Generic;

namespace Dp
{
    public class BalancePartitionSample
    {
        public void Test()
        {
            BalancePartition partition = new BalancePartition();
            int[] nums;
            int sum;
            sum = 10;
            nums = new int[] { 3, 7, 4, 6 };
            Test(partition, nums, sum);
            sum = 5;
            nums = new int[] { 1, 1, 1, 3, 2, 2 };
            Test(partition, nums, sum);
            sum = 10;
            nums = new int[] { 3, 2, 1, 1, 6, 7 };
            Test(partition, nums, sum);
            sum = 11;
            nums = new int[] { 3, 2, 1, 1, 6, 8 };
            Test(partition, nums, sum);
            sum = 6;
            nums = new int[] { 3, 2, 5 };
            Test(partition, nums, sum);

            nums = new int[] { 66, 90, 7, 6, 32, 16, 2, 78, 69, 88, 85, 26, 3, 9, 58, 65, 30, 96, 11, 31, 99, 49, 63, 83, 79, 97, 20, 64, 81, 80, 25, 69, 9, 75, 23, 70, 26, 71, 25, 54, 1, 40, 41, 82, 32, 10, 26, 33, 50, 71, 5, 91, 59, 96, 9, 15, 46, 70, 26, 32, 49, 35, 80, 21, 34, 95, 51, 66, 17, 71, 28, 88, 46, 21, 31, 71, 42, 2, 98, 96, 40, 65, 92, 43, 68, 14, 98, 38, 13, 77, 14, 13, 60, 79, 52, 46, 9, 13, 25, 8 };
            Test(partition, nums, 2387);

            // Test(partition, new int[] { 100 }, 50);
        }

        private void Test(BalancePartition partition, int[] nums, int sum)
        {
            bool isSubsetWithRecur = partition.IsSubsetSumWithRecur(nums, sum);
            bool isSubset = partition.IsSubsetSum1(nums, sum);
            Console.WriteLine($"{string.Join(",", nums)} for {sum} can be balance partition: {isSubset} and {isSubsetWithRecur}(Recur)");
        }
    }

    public class BalancePartition
    {
        public bool IsSubsetSumWithRecur1(int[] nums, int sum)
        {
            int total = 0;
            foreach (int num in nums)
            {
                total += num;
            }
            if (total != sum * 2)
            {
                return false;
            }
            return IsSubsetSumWithRecur1(new List<int>(nums), sum, sum);
        }

        private bool IsSubsetSumWithRecur1(List<int> nums, int sum, int originSum)
        {
            if (sum < 0)
            {
                return false;
            }
            if (sum == 0)
            {
                return true;
            }
            for (int i = 0; i < nums.Count; i++)
            {
                int num = nums[i];
                nums.RemoveAt(i);
                bool isSubset = IsSubsetSumWithRecur1(nums, sum - num, originSum);
                if (isSubset) return true;
                nums.Insert(i, num);
            }
            return false;
        }

        public bool IsSubsetSumWithRecur(int[] nums, int sum)
        {
            int total = 0;
            foreach (int num in nums)
            {
                total += num;
            }
            if (total != sum * 2)
            {
                return false;
            }
            return IsSubsetSumWithRecur(nums, sum, 0);
        }

        private bool IsSubsetSumWithRecur(int[] nums, int sum, int start)
        {
            if (sum < 0)
            {
                return false;
            }
            if (sum == 0)
            {
                return true;
            }
            for (int i = start; i < nums.Length; i++)
            {
                bool isSubset = IsSubsetSumWithRecur(nums, sum - nums[i], i + 1);
                if (isSubset)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsSubsetSum(int[] nums, int sum)
        {
            int total = 0;
            foreach (int num in nums)
            {
                total += num;
            }
            if (total != sum * 2)
            {
                return false;
            }
            int[,] matrix = new int[nums.Length, sum + 1];
            int max = total + 1;
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 1; j < sum + 1; j++)
                {
                    matrix[i, j] = max;
                }
            }
            for (int i = 0; i < nums.Length; i++)
            {
                int sum1 = 0;
                for (int j = 0; j <= i; j++)
                {
                    matrix[i, nums[j]] = nums[j];
                    sum1 += nums[j];
                    matrix[i, sum1] = sum1;
                }
            }
            for (int i = 0; i < nums.Length; i++)
            {
                if (matrix[i, sum] == sum)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsSubsetSum1(int[] nums, int sum)
        {
            int total = 0;
            foreach (int num in nums)
            {
                total += num;
            }
            if (total != sum * 2)
            {
                return false;
            }
            int len = nums.Length;
            int n = (int)Math.Pow(2, len + 1);
            for (int i = 1; i < n; i++)
            {
                int setSum = 0;
                for (int j = 0; j < len; j++)
                {
                    if ((i & (1 << j)) > 0)
                    {
                        setSum += nums[j];
                    }
                }
                if (setSum == sum)
                {
                    return true;
                }
            }
            return false;
        }
    }
}