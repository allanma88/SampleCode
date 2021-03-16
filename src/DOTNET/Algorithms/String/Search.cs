using System;

namespace String
{
    public class SearchSample
    {
        public void Run()
        {
            Search search = new Search();
            int i = search.Kmp("BBC ABCDAB ABCDABCDABDE", "ABCDABD");
            Console.WriteLine(i);

            i = search.Kmp("BFACDEABGHACDMACDEAC", "ACDEAC");
            Console.WriteLine(i);

            i = search.Kmp("BFACDEABGHACDMACDEAB", "ACDEAC");
            Console.WriteLine(i);
        }
    }

    public class Search
    {
        public int Kmp(string txt, string pattern)
        {
            int[] table = BuildPartialMatchTable(pattern);
            for (int i = 0, j = 0; i < txt.Length; i++)
            {
                if (txt[i] == pattern[j])
                {
                    if (++j == pattern.Length)
                    {
                        return i - j + 1;
                    }
                }
                else
                {
                    j = table[j];
                }
            }
            return txt.Length;
        }

        private int[] BuildPartialMatchTable(string pattern)
        {
            int[] table = new int[pattern.Length + 1];
            for (int len = 2; len < pattern.Length + 1; len++)
            {
                table[len] = FindCommonLength(pattern, len);
            }
            return table;
        }

        private int FindCommonLength(string pattern, int len)
        {
            int secondPart = len / 2;
            int firstPart = 0;
            int common = 0;
            while (secondPart < len)
            {
                if (pattern[firstPart] != pattern[secondPart])
                {
                    secondPart++;
                    firstPart = 0;
                    common = 0;
                }
                else
                {
                    firstPart++;
                    secondPart++;
                    common++;
                }
            }
            return common;
        }
    }
}