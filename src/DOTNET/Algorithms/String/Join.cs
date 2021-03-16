using System;
using System.Text;

namespace String
{
    public class JoinSample
    {
        public void Run()
        {
            JoinImp join = new JoinImp();
            Console.WriteLine(join.Join(",", new string[] { "a", "b", "c" }));
            Console.WriteLine(join.Join("", new string[] { "a", "b", "c" }));
            Console.WriteLine(join.Join("", new string[] { "a", "b", "c" }));
        }
    }

    public class JoinImp
    {
        public string Join(string sep, string[] values)
        {
            int len = values.Length;
            if (len == 0) return string.Empty;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < len - 1; i++)
            {
                builder.Append(values[i]).Append(sep);
            }
            builder.Append(values[len - 1]);
            return builder.ToString();
        }
    }
}