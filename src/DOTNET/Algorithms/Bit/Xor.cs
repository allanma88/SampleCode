using System;
using System.Collections.Generic;

namespace Bit
{
    public class XorSample
    {
        public void Run()
        {
            Xor xor = new Xor();
            RunTest(xor, new int[] { 3, 4, 5, 2, 4 }, 7, 2);
            RunTest(xor, new int[] { 1, 1, 1, 1 }, 0, 2);
            RunTest(xor, new int[] { 1, 2, 4, 3, 4, 0 }, 7, 3);
            RunTest(xor, new int[] { 1, 1, 1, 1 }, 1, 3);
            int[] nums;
            int k;
            nums = GetNums(5);
            k = 1;
            RunTest(xor, nums, k, 2);
            k = nums[0] ^ nums[1] ^ nums[2];
            RunTest(xor, nums, k, 3);
        }

        private void RunTest(Xor xor, int[] nums, int k, int m)
        {
            string res = "";
            int[] results = null;
            int count = 0;
            if (m == 2)
            {
                results = xor.EqualOf2(nums, k);
                count = xor.CountEqualOf2(nums, k);
                // count = xor.CountEqual2_Worst(nums, k);
            }
            else if (m == 3)
            {
                results = xor.EqualOf3(nums, k);
                count = xor.CountEqualOf3(nums, k);
            }
            if (results != null)
            {
                res = string.Join(",", results);
            }
            else
            {
                res = "none";
            }
            Console.WriteLine($"The {m} number of {string.Join(",", nums)}, whose xor equal to {k} is {res} and the count is {count}");
        }

        private int[] GetNums(int n)
        {
            int[] nums = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                nums[i] = random.Next(1, 20);
            }
            return nums;
        }
    }

    public class Xor
    {
        public int[] EqualOf2(int[] nums, int k)
        {
            HashSet<int> set = new HashSet<int>();
            foreach (int num in nums)
            {
                if (set.Contains(num ^ k))
                {
                    return new int[] { num, num ^ k };
                }
                set.Add(num);
            }
            return null;
        }

        public int CountEqualOf2_Worst(int[] nums, int k)
        {
            int count = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    if ((nums[i] ^ nums[j]) == k)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int CountEqualOf2(int[] nums, int k)
        {
            int count = 0;
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (int num in nums)
            {
                if (dic.ContainsKey(num ^ k))
                {
                    count += dic[num ^ k];
                }
                if (dic.ContainsKey(num))
                {
                    dic[num]++;
                }
                else
                {
                    dic[num] = 1;
                }
            }
            return count;
        }

        public int[] EqualOf3(int[] nums, int k)
        {
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    int xor = nums[i] ^ nums[j];
                    if (set.Contains(xor ^ k))
                    {
                        return new int[] { nums[i], nums[j], nums[i] ^ nums[j] ^ k };
                    }
                    set.Add(nums[j]);
                }
                set.Clear();
            }
            return null;
        }

        public int CountEqualOf3(int[] nums, int k)
        {
            int count = 0;
            HashSet<int> set = new HashSet<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = i + 1; j < nums.Length; j++)
                {
                    int xor = nums[i] ^ nums[j];
                    if (set.Contains(xor ^ k))
                    {
                        count++;
                    }
                    set.Add(nums[j]);
                }
                set.Clear();
            }
            return count;
        }

        public int[] EqualofM(int[] nums, int k, int m)
        {
            throw new NotImplementedException();
        }
    }
}