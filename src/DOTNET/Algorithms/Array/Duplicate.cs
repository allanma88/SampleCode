using System;
using System.Collections.Generic;

namespace Array
{
    public class DuplicateSample
    {
        public void Run()
        {
            Duplicate duplicate = new Duplicate();
            // RunWithNoOrder(duplicate);
            RunWithOrder(duplicate);
        }

        public void RunWithNoOrder(Duplicate duplicate)
        {
            int[] values = new int[] {1, 1, 1, 1, 1, 1, 2 };
            int d = duplicate.FindMostDuplicateWithNoOrder(values);
            Console.WriteLine(d);
        }

        public void RunWithOrder(Duplicate duplicate)
        {
            int[] values = new int[] { 1, 1, 1, 2, 2, 3, 3, 3, 3 };
            int d = duplicate.FindMostDuplicateWithNoOrder(values);
            Console.WriteLine(d);
        }
    }

    public class Duplicate
    {
        public int FindMostDuplicateWithNoOrder(int[] values)
        {
            if (values.Length == 0)
            {
                throw new InvalidOperationException();
            }
            Dictionary<int, int> dic = new Dictionary<int, int>();
            int most = values[0];
            dic[most] = 1;
            for (int i = 1; i < values.Length; i++)
            {
                int v = values[i];
                if (dic.ContainsKey(v))
                {
                    dic[v]++;
                    if (dic[v] > dic[most])
                    {
                        most = v;
                    }
                }
                else
                {
                    dic[v] = 1;
                }
            }
            return most;
        }

        public int FindMostDuplicateWithOrder(int[] values)
        {
            if (values.Length == 0)
            {
                throw new InvalidOperationException();
            }
            int most = values[0];
            int maxLen = 1;
            int len = 1;
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] == values[i - 1])
                {
                    len++;
                }
                else
                {
                    if (len > maxLen)
                    {
                        maxLen = len;
                        most = values[i - 1];
                    }
                    len = 1;
                }
            }
            return most;
        }
    }
}