using System;
using System.Text;

namespace String
{
    public class TrimSample
    {
        public void Run()
        {
            TrimImp trim = new TrimImp();
            string s = "abcdefa";
            Console.WriteLine(trim.Trim(s, "aa"));
        }
    }

    public class TrimImp
    {
        public string Trim(string s, string trim)
        {
            StringBuilder builder = new StringBuilder();
            int start = 0;
            int end = s.Length - 1;
            int i;
            for (i = 0; i < trim.Length; i++)
            {
                if (s[i] != trim[i]) break;
            }
            if (i == trim.Length) start = trim.Length;
            int j;
            for (j = s.Length - trim.Length; j < s.Length; j++)
            {
                if (s[j] != trim[j - s.Length + trim.Length]) break;
            }
            if (j == s.Length) end = s.Length - trim.Length - 1;
            for (int k = start; k <= end; k++)
            {
                builder.Append(s[k]);
            }
            return builder.ToString();
        }
    }
}