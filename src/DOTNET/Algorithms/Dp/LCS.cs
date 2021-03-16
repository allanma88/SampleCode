using System;

namespace Dp
{
    public class LCSSample
    {
        public void Run()
        {
            string s = "cnblogs";
            string t = "belong";
            LCS lcs = new LCS();
            int len = lcs.FindWithRecur(s, t);
            Console.WriteLine($"FindWithRecur: LCS of {s} and {t} is {len}");
            len = lcs.Find(s, t);
            string sub = lcs.FindSubSequnce(s, t);
            Console.WriteLine($"Find: LCS of {s} and {t} is {len}, the sequence is {sub}");
            len = lcs.FindContinuous(s, t);
            // string sub = lcs.FindStr(s, t);
            Console.WriteLine($"FindContinuous: LCS of {s} and {t} is {len}");
        }
    }

    public class LCS
    {
        public int FindWithRecur(string s, string t)
        {
            return FindWithRecur(s, s.Length - 1, t, t.Length - 1);
        }

        private int FindWithRecur(string s, int si, string t, int ti)
        {
            if (si < 0 | ti < 0) return 0;
            if (s[si] == t[ti])
            {
                return FindWithRecur(s, si - 1, t, ti - 1) + 1;
            }
            else
            {
                return Math.Max(FindWithRecur(s, si - 1, t, ti), FindWithRecur(s, si, t, ti - 1));
            }
        }

        public int Find(string s, string t)
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

        public string FindSubSequnce(string s, string t)
        {
            int[,] dp = new int[s.Length + 1, t.Length + 1];
            string[,] dpStr = new string[s.Length + 1, t.Length + 1];
            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    if (s[i - 1] == t[j - 1])
                    {
                        dpStr[i, j] = dpStr[i - 1, j - 1] + s[i - 1];
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                    }
                    else
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
                        if (dp[i - 1, j] > dp[i, j - 1])
                        {
                            dpStr[i, j] = dpStr[i - 1, j];
                        }
                        else
                        {
                            dpStr[i, j] = dpStr[i, j - 1];
                        }
                    }
                }
            }
            return dpStr[s.Length, t.Length];
        }

        public int FindContinuous(string s, string t)
        {
            int[,] dp = new int[s.Length + 1, t.Length + 1];
            int longest = 0;
            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    if (s[i - 1] == t[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + 1;
                        if (dp[i, j] > longest) longest = dp[i, j];
                    }
                }
            }
            return longest;
        }

        public int FindContinuous1(string s, string t)
        {
            int[,] dp = new int[s.Length + 1, t.Length + 1];
            for (int i = 1; i <= s.Length; i++)
            {
                for (int j = 1; j <= t.Length; j++)
                {
                    if (s[i - 1] == t[j - 1])
                    {
                        dp[i, j] = 1;
                    }
                }
            }
            int k = 0, m = 0;
            int longest = 0;
            while (k < s.Length + 1)
            {
                int len = 0;
                int p = k, q = m;
                while (p < s.Length + 1 && q < t.Length + 1)
                {
                    if (dp[p, q] == 1)
                    {
                        len++;
                    }
                    else
                    {
                        if (len > longest) longest = len;
                        len = 0;
                    }
                    p++;
                    q++;
                }
                k++;
            }
            while (m < t.Length + 1)
            {
                int len = 0;
                int p = k, q = m;
                while (p < s.Length + 1 && q < t.Length + 1)
                {
                    if (dp[p, q] == 1)
                    {
                        len++;
                    }
                    else
                    {
                        if (len > longest) longest = len;
                        len = 0;
                    }
                    p++;
                    q++;
                }
                m++;
            }
            return longest;
        }
    }
}