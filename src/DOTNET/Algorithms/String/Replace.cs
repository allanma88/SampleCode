using System;
using System.Text;

namespace String
{
    public class ReplaceImp
    {
        public string Replace(string s, string oldStr, string newStr)
        {
            StringBuilder builder = new StringBuilder();
            int start = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int j;
                for (j = i; j < i + oldStr.Length; j++)
                {
                    if (s[j] != oldStr[j - i]) break;
                }
                if (j == i + oldStr.Length)
                {
                    builder.Append(newStr);
                    start = j;
                }
                else
                {
                    //TODO: 这里的插入有问题
                    for (int k = start; k <= j; k++)
                    {
                        builder.Append(s[k]);
                    }
                    start = j + 1;
                }
            }
            return builder.ToString();
        }
    }
}