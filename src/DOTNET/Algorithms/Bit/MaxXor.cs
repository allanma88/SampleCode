using System;
using System.Collections.Generic;

namespace Bit
{
    public class MaxXorSample
    {
        public void Run()
        {
            Console.WriteLine("Xor =====================");
            MaxXor maxXor = new MaxXor();
            // RunTwo(maxXor);
            RunAnyOfSubset(maxXor);
        }

        private void RunAnyOfSubset(MaxXor maxXor)
        {
            Console.WriteLine("Elements of Subset =====================");
            int[] values;
            int max, maxWithGau;
            values = new int[] { 1, 2, 3, 4 };
            max = maxXor.FindAnyOfSubset(values);
            maxWithGau = maxXor.FindAnyOfSubsetWithGaussian(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max} and {maxWithGau}(Gaussian)");

            values = new int[] { 8, 1, 2, 12, 7, 6 };
            max = maxXor.FindAnyOfSubset(values);
            maxWithGau = maxXor.FindAnyOfSubsetWithGaussian(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max} and {maxWithGau}(Gaussian)");

            values = new int[] { 4, 6 };
            max = maxXor.FindAnyOfSubset(values);
            maxWithGau = maxXor.FindAnyOfSubsetWithGaussian(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max} and {maxWithGau}(Gaussian)");

            values = new int[] { 9, 7, 4, 3 };
            max = maxXor.FindAnyOfSubset(values);
            maxWithGau = maxXor.FindAnyOfSubsetWithGaussian(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max} and {maxWithGau}(Gaussian)");

            values = new int[] { 2, 11, 6, 14, 10 };
            max = maxXor.FindAnyOfSubset(values);
            maxWithGau = maxXor.FindAnyOfSubsetWithGaussian(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max} and {maxWithGau}(Gaussian)");

            values = TrieCommon.GetNums(5);
            max = maxXor.FindAnyOfSubset(values);
            maxWithGau = maxXor.FindAnyOfSubsetWithGaussian(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max} and {maxWithGau}(Gaussian)");
        }

        private void RunTwo(MaxXor maxXor)
        {
            Console.WriteLine("Elements of Two =====================");
            int[] values;
            int max;
            values = new int[] { 3, 10, 5, 25, 2, 8 };
            max = maxXor.FindTwo(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max}");

            values = new int[] { 3, 10, 5, 25 };
            max = maxXor.FindTwo(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max}");

            values = new int[] { 3, 10, 5 + 16, 25 };
            max = maxXor.FindTwo(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max}");

            values = new int[] { 16 + 3, 16 + 10, 16 + 5, 25 };
            max = maxXor.FindTwo(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max}");

            values = new int[] { 10, 2, 8 };
            max = maxXor.FindTwo(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max}");
        }
    }

    public class MaxXor
    {
        public int FindTwo(int[] nums)
        {
            int maxXor = 0, mask = 0;
            for (int i = 31; i >= 0; i--)
            {
                mask = mask | (1 << i);
                HashSet<int> set = new HashSet<int>();
                foreach (var num in nums)
                {
                    set.Add(num & mask);
                }
                int nextMaxXor = maxXor | (1 << i);
                foreach (var prefix in set)
                {
                    if (set.Contains(prefix ^ nextMaxXor))
                    {
                        maxXor = nextMaxXor;
                    }
                }
            }
            return maxXor;
        }

        public int FindAnyOfSubset(int[] nums)
        {
            int max = 0, mask = 0;
            int count = 31;
            for (int i = count; i >= 0; i--)
            {
                mask |= 1 << i;
                HashSet<int> set = new HashSet<int>();
                foreach (var num in nums)
                {
                    set.Add(num & mask);
                }
                int temp = max | (1 << i);
                if (set.Contains(max ^ temp))
                {
                    max = temp;
                    continue;
                }
                foreach (var prefix in set)
                {
                    if (set.Contains(prefix ^ temp) || (prefix ^ temp) == 0)
                    {
                        max = temp;
                        break;
                    }
                }
            }
            return max;
        }

        public int FindAnyOfSubsetWithGaussian(int[] nums)
        {
            int[] newNums = new int[nums.Length];
            Array.Copy(nums, newNums, nums.Length);
            Array.Sort(newNums, new Comparison<int>((i1, i2) => i2.CompareTo(i1)));
            int count = 31;
            int firstBitSet = 0;
            for (int i = count; i >= 0; i--)
            {
                if ((newNums[0] & (1 << i)) > 0)
                {
                    firstBitSet = i;
                    break;
                }
            }
            for (int i = firstBitSet, firstIndex = 0; i >= 0 && firstIndex < newNums.Length; i--, firstIndex++)
            {
                if ((newNums[firstIndex] & (1 << i)) == 0)
                {
                    bool exists = false;
                    for (int k = firstIndex + 1; k < newNums.Length; k++)
                    {
                        if ((newNums[k] & (1 << i)) > 0)
                        {
                            int temp = newNums[firstIndex];
                            newNums[firstIndex] = newNums[k];
                            newNums[k] = temp;
                            exists = true;
                            break;
                        }
                    }
                    if (!exists) continue;
                }
                for (int j = 0; j < newNums.Length; j++)
                {
                    if (j != firstIndex && (newNums[j] & (1 << i)) > 0)
                    {
                        newNums[j] ^= newNums[firstIndex];
                    }
                }
            }
            int maxXor = 0;
            foreach (int num in newNums)
            {
                maxXor = Math.Max(maxXor, maxXor ^ num);
            }
            return maxXor;
        }

        public int FindThree(int[] nums)
        {
            throw new NotImplementedException();
        }
    }

    public class MinXorSample
    {
        public void Run()
        {
            Console.WriteLine("Xor =====================");
            MinXor minXor = new MinXor();
            RunTwo(minXor);
        }

        private void RunTwo(MinXor minXor)
        {
            Console.WriteLine("Elements of Two =====================");
            int[] values;
            int min;
            values = new int[] { 3, 10, 5, 25, 2, 8 };
            min = minXor.FindTwo(values);
            Console.WriteLine($"The min xor of {string.Join(",", values)} is {min}");

            values = new int[] { 3, 10, 5, 25 };
            min = minXor.FindTwo(values);
            Console.WriteLine($"The min xor of {string.Join(",", values)} is {min}");

            values = new int[] { 3, 10, 5 + 16, 25 };
            min = minXor.FindTwo(values);
            Console.WriteLine($"The min xor of {string.Join(",", values)} is {min}");

            values = new int[] { 16 + 3, 16 + 10, 16 + 5, 25 };
            min = minXor.FindTwo(values);
            Console.WriteLine($"The min xor of {string.Join(",", values)} is {min}");

            values = new int[] { 10, 2, 8 };
            min = minXor.FindTwo(values);
            Console.WriteLine($"The min xor of {string.Join(",", values)} is {min}");
        }
    }

    public class MinXor
    {
        public int FindTwo(int[] nums)
        {
            int minXor = 0, mask = 0;
            int count = 31;
            for (int i = count; i >= 0; i--)
            {
                mask = mask | (1 << i);
                int[] prefixes = new int[nums.Length];
                for (int j = 0; j < nums.Length; j++)
                {
                    prefixes[j] = mask & nums[j];
                }
                bool exists = false;
                HashSet<int> set = new HashSet<int>();
                foreach (var prefix in prefixes)
                {
                    if (set.Contains(prefix ^ minXor))
                    {
                        exists = true;
                        break;
                    }
                    set.Add(prefix);
                }
                if (!exists)
                {
                    minXor = minXor | (1 << i);
                }
            }
            return minXor;
        }
    }

    public class MaxXorUseTrieSample
    {
        public void Run()
        {
            Console.WriteLine("Trie =====================");
            MaxXorUseTrie maxXor = new MaxXorUseTrie();
            // RunTwo(maxXor);
            // RunAnyOfArray(maxXor);
            RunLessThanCountOfArray(maxXor);
        }

        private void RunAnyOfArray(MaxXorUseTrie maxXor)
        {
            Console.WriteLine("Elements of Array =====================");
            int[] values;
            int maxWithTrie;
            values = new int[] { 1, 2, 3, 4 };
            maxWithTrie = maxXor.FindAnyOfArray(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {maxWithTrie}");

            values = new int[] { 8, 1, 2, 12, 7, 6 };
            maxWithTrie = maxXor.FindAnyOfArray(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {maxWithTrie}");

            values = new int[] { 4, 6 };
            maxWithTrie = maxXor.FindAnyOfArray(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {maxWithTrie}");

            values = new int[] { 9, 7, 4, 3 };
            maxWithTrie = maxXor.FindAnyOfArray(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {maxWithTrie}");
        }

        private void RunLessThanCountOfArray(MaxXorUseTrie maxXor)
        {
            Console.WriteLine("LessThanCount Elements of Array =====================");
            int[] values;
            int count, k;
            values = new int[] { 1, 2, 3, 4 };
            k = 7;
            count = maxXor.FindLessThanCountOfArray(values, k);
            Console.WriteLine($"The xor's count less than {k} of {string.Join(",", values)} is {count}");

            values = new int[] { 8, 1, 2, 12, 7, 6 };
            k = 10;
            count = maxXor.FindLessThanCountOfArray(values, k);
            Console.WriteLine($"The xor's count less than {k} of {string.Join(",", values)} is {count}");

            values = new int[] { 9, 7, 4, 3 };
            k = 6;
            count = maxXor.FindLessThanCountOfArray(values, k);
            Console.WriteLine($"The xor's count less than {k} of {string.Join(",", values)} is {count}");
        }

        private void RunTwo(MaxXorUseTrie maxXor)
        {
            Console.WriteLine("Elements of Two =====================");
            int[] values;
            int max;
            values = new int[] { 3, 10, 5, 25, 2, 8 };
            max = maxXor.FindTwo(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max}");

            values = new int[] { 3, 10, 5, 25 };
            max = maxXor.FindTwo(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max}");

            values = new int[] { 3, 10, 5 + 16, 25 };
            max = maxXor.FindTwo(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max}");

            values = new int[] { 16 + 3, 16 + 10, 16 + 5, 25 };
            max = maxXor.FindTwo(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max}");

            values = new int[] { 10, 2, 8 };
            max = maxXor.FindTwo(values);
            Console.WriteLine($"The max xor of {string.Join(",", values)} is {max}");
        }
    }

    public class MaxXorUseTrie
    {
        public int FindTwo(int[] nums)
        {
            TrieNode root = new TrieNode();
            int count = 31;
            foreach (int num in nums)
            {
                TrieNode n = root;
                for (int i = count; i >= 0; i--)
                {
                    int bit = (num >> i) & 1;
                    if (n.Next[bit] == null)
                    {
                        n.Next[bit] = new TrieNode();
                    }
                    n = n.Next[bit];
                }
                n.Val = num;
            }
            int max = -1;
            foreach (int num in nums)
            {
                TrieNode n = root;
                for (int i = count; i >= 0; i--)
                {
                    int bit = (num >> i) & 1;
                    if (n.Next[bit ^ 1] != null)
                    {
                        n = n.Next[bit ^ 1];
                    }
                    else
                    {
                        n = n.Next[bit];
                    }
                }
                int xor = num ^ n.Val;
                if (xor > max) max = xor;
            }
            return max;
        }

        public int FindLessThanCountOfArray(int[] nums, int k)
        {
            //TODO
            int count = 0;
            TrieNode root = new TrieNode();
            Insert(root, 0);
            int xor = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                xor ^= nums[i];
                Insert(root, xor);
                Queue<TrieNode> queue = new Queue<TrieNode>();
                queue.Enqueue(root);
                while (queue.Count > 0)
                {
                    TrieNode n = queue.Dequeue();
                    if (n.Val != -1)
                    {
                        if ((xor ^ n.Val) < k)
                        {
                            count++;
                        }
                    }
                    else
                    {
                        foreach (var next in n.Next)
                        {
                            if (next != null)
                            {
                                queue.Enqueue(next);
                            }
                        }
                    }
                }
            }
            return count;
        }

        public int FindAnyOfArray(int[] nums)
        {
            TrieNode root = new TrieNode();
            int maxXor = 0, preXor = 0;
            Insert(root, maxXor);
            foreach (var num in nums)
            {
                preXor = preXor ^ num;
                Insert(root, preXor);
                int max = QueryMax(root, preXor);
                maxXor = Math.Max(maxXor, max);
            }
            return maxXor;
        }

        private int QueryMax(TrieNode node, int xor)
        {
            TrieNode n = node;
            int count = 31;
            for (int i = count; i >= 0; i--)
            {
                int bit = (xor >> i) & 1;
                if (n.Next[bit ^ 1] != null)
                {
                    n = n.Next[bit ^ 1];
                }
                else
                {
                    n = n.Next[bit];
                }
            }
            return xor ^ n.Val;
        }

        private void Insert(TrieNode node, int num)
        {
            int count = 31;
            TrieNode n = node;
            for (int i = count; i >= 0; i--)
            {
                int bit = (num >> i) & 1;
                if (n.Next[bit] == null)
                {
                    n.Next[bit] = new TrieNode();
                }
                n = n.Next[bit];
            }
            n.Val = num;
        }
    }

    public class MinXorUseTrieSample
    {
        public void Run()
        {
            Console.WriteLine("Trie =====================");
            MinXorUseTrie minXor = new MinXorUseTrie();
            RunTwo(minXor);
            // RunAnyOfArray(minXor);
        }

        private void RunAnyOfArray(MinXorUseTrie minXor)
        {
            // Console.WriteLine("Elements of Array =====================");
            // int[] values;
            // int maxWithTrie;
            // values = new int[] { 1, 2, 3, 4 };
            // maxWithTrie = minXor.FindAnyOfArray(values);
            // Console.WriteLine($"The max xor of {string.Join(",", values)} is {maxWithTrie}");

            // values = new int[] { 8, 1, 2, 12, 7, 6 };
            // maxWithTrie = minXor.FindAnyOfArray(values);
            // Console.WriteLine($"The max xor of {string.Join(",", values)} is {maxWithTrie}");

            // values = new int[] { 4, 6 };
            // maxWithTrie = minXor.FindAnyOfArray(values);
            // Console.WriteLine($"The max xor of {string.Join(",", values)} is {maxWithTrie}");

            // values = new int[] { 9, 7, 4, 3 };
            // maxWithTrie = minXor.FindAnyOfArray(values);
            // Console.WriteLine($"The max xor of {string.Join(",", values)} is {maxWithTrie}");
        }

        private void RunTwo(MinXorUseTrie minXor)
        {
            Console.WriteLine("Elements of Two =====================");
            int[] values;
            int min;
            values = new int[] { 3, 10, 5, 25, 2, 8 };
            min = minXor.FindTwo(values);
            Console.WriteLine($"The min xor of {string.Join(",", values)} is {min}");

            values = new int[] { 3, 10, 5, 25 };
            min = minXor.FindTwo(values);
            Console.WriteLine($"The min xor of {string.Join(",", values)} is {min}");

            values = new int[] { 3, 10, 5 + 16, 25 };
            min = minXor.FindTwo(values);
            Console.WriteLine($"The min xor of {string.Join(",", values)} is {min}");

            values = new int[] { 16 + 3, 16 + 10, 16 + 5, 25 };
            min = minXor.FindTwo(values);
            Console.WriteLine($"The min xor of {string.Join(",", values)} is {min}");

            values = new int[] { 10, 2, 8 };
            min = minXor.FindTwo(values);
            Console.WriteLine($"The min xor of {string.Join(",", values)} is {min}");
        }
    }

    public class MinXorUseTrie
    {
        public int FindTwo(int[] nums)
        {
            TrieNode root = new TrieNode();
            int count = 4;
            foreach (int num in nums)
            {
                TrieNode n = root;
                for (int i = count; i >= 0; i--)
                {
                    int bit = (num >> i) & 1;
                    if (n.Next[bit] == null)
                    {
                        n.Next[bit] = new TrieNode();
                    }
                    n = n.Next[bit];
                }
                n.Val = num;
            }
            int minXor = int.MaxValue;
            foreach (int num in nums)
            {
                TrieNode n = root;
                TrieNode other = null;
                int otherBit = -1;
                for (int i = count; i >= 0; i--)
                {
                    int bit = (num >> i) & 1;
                    if (n.Next[1 ^ bit] != null)
                    {
                        other = n.Next[1 ^ bit];
                        otherBit = i;
                    }
                    n = n.Next[bit];
                }
                for (int i = otherBit - 1; i >= 0; i--)
                {
                    int bit = (num >> i) & 1;
                    if (other.Next[bit] != null)
                    {
                        other = other.Next[bit];
                    }
                    else
                    {
                        other = other.Next[1 ^ bit];
                    }
                }
                minXor = Math.Min(minXor, num ^ other.Val);
            }
            return minXor;
        }
    }

    public class TrieNode
    {
        public int Val = -1;

        public TrieNode[] Next = new TrieNode[2];
    }

    public class TrieCommon
    {
        public static int[] GetNums(int len)
        {
            Random random = new Random();
            int[] nums = new int[len];
            for (int i = 0; i < len; i++)
            {
                nums[i] = random.Next(1, 20);
            }
            return nums;
        }
    }
}