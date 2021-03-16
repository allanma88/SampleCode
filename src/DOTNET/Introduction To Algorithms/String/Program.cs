using System;
using System.Collections.Generic;

namespace String
{
    class Program
    {
        static void Main(string[] args)
        {
            string target = "abcaabcaaabcababc";
            string pattern = "abc";
            List<int> matches = Match(target, pattern);
            string str = string.Join(",", matches);
            Console.WriteLine($"{target} match with {pattern} in {str}");
        }

        public static List<int> Match(string target, string pattern)
        {
            int i = 0;
            List<int> matches = new List<int>();
            while (i <= target.Length - pattern.Length)
            {
                int j = 0;
                while (j < pattern.Length)
                {
                    if (target[i + j] == pattern[j])
                    {
                        j++;
                    }
                    else
                    {
                        break;
                    }
                }
                if (j == pattern.Length)
                {
                    matches.Add(i);
                }
                i += j;
            }
            return matches;
        }
    }
}
