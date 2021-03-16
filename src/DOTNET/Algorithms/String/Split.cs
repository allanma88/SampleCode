using System;
using System.Collections.Generic;

namespace String
{
    public class SplitSample
    {
        public void Run()
        {
            SplitImp split = new SplitImp();
            string s = "aabcddbceeebcfffffbcgg";
            var values = split.Split(s, "bc");
            Console.WriteLine(string.Join(",", values));
        }
    }

    public class SplitImp
    {
        public string[] Split(string s, string sep)
        {
            List<string> values = new List<string>();
            int start = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int j;
                for (j = i; j < i + sep.Length; j++)
                {
                    if (s[j] != sep[j - i]) break;
                }
                if (j == i + sep.Length)
                {
                    values.Add(s.Substring(start, i - start));
                    start = j;
                }
            }
            if (start <= s.Length)
            {
                values.Add(s.Substring(start, s.Length - start));
            }
            return values.ToArray();
        }
    }
}