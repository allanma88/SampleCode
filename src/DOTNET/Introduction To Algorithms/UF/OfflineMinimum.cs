using System;
using System.Collections.Generic;

namespace UF
{
    public class OfflineMinimum
    {
        public void Run()
        {
            int n = 9;
            int m = 6;
            int[] operations = new int[] { 4, 8, -1, 3, -1, 9, 2, 6, -1, -1, -1, 1, 7, -1, 5 };
            int[] extracted = Extract(operations, n, m);
            Console.WriteLine(string.Join(",", extracted));

            extracted = Extract1(operations, n, m);
            Console.WriteLine(string.Join(",", extracted));
        }

        public int[] Extract1(int[] operations, int n, int m)
        {
            int[] extracted = new int[m];

            int[] disjointSet = new int[operations.Length];
            for (int i = 0; i < operations.Length; i++)
            {
                if (operations[i] == -1)
                {
                    disjointSet[i] = -1;
                }
            }

            HashSet<int> hashset = new HashSet<int>();

            int start = 0, end = 0, q = 0;
            while (start < operations.Length)
            {
                while (end < operations.Length && operations[end] != -1)
                {
                    end++;
                }
                for (int i = start; i < end; i++)
                {
                    disjointSet[i] = q;
                }
                hashset.Add(q++);
                start = ++end;
            }

            Dictionary<int, int> map = new Dictionary<int, int>();
            for (int i = 0; i < operations.Length; i++)
            {
                if (operations[i] != -1)
                {
                    map[operations[i]] = i;
                }
            }

            for (int i = 1; i <= n; i++)
            {
                int k = map[i];
                int j = disjointSet[k];
                if (j < m)
                {
                    extracted[j] = i;
                }

                int l = -1;
                foreach (var num in hashset)
                {
                    if (num > j)
                    {
                        l = num;
                        break;
                    }
                }
                if (l != -1 && l < m + 1)
                {
                    for (int p = 0; p < disjointSet.Length; p++)
                    {
                        if (disjointSet[p] == j)
                        {
                            disjointSet[p] = l;
                        }
                    }
                    hashset.Remove(j);
                }
            }

            return extracted;
        }

        public int[] Extract(int[] operations, int n, int m)
        {
            int[] extracted = new int[m];

            Dictionary<int, List<int>> k = GetK(operations);

            for (int i = 1; i <= n; i++)
            {
                //determine j such that i belong Kj
                int j = -1;
                foreach (int key in k.Keys)
                {
                    if (k[key].Contains(i))
                    {
                        j = key;
                        break;
                    }
                }
                if (j < m)
                {
                    extracted[j] = i;
                }

                //let l be the smallest value greater than j for which set Kl exists
                int l = -1;
                foreach (int key in k.Keys)
                {
                    if (key > j)
                    {
                        l = key;
                        break;
                    }
                }

                if (l != -1 && l < m + 1)
                {
                    //Kl = Kj & Kl, destroying K
                    List<int> jList = k[j];
                    foreach (int num in k[l])
                    {
                        jList.Add(num);
                    }
                    k[l] = jList;
                    k.Remove(j);
                }
            }

            return extracted;
        }

        private Dictionary<int, List<int>> GetK(int[] operations)
        {
            Dictionary<int, List<int>> k = new Dictionary<int, List<int>>();
            int start = 0, end = 0, q = 0;
            while (start < operations.Length)
            {
                while (end < operations.Length && operations[end] != -1)
                {
                    end++;
                }
                List<int> nums = new List<int>();
                for (int i = start; i < end; i++)
                {
                    nums.Add(operations[i]);
                }
                k.Add(q++, nums);
                start = ++end;
            }
            return k;
        }
    }
}