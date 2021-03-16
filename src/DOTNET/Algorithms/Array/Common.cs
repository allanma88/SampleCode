using System;

namespace Array
{
     public class Common
    {
        public static void Print(int[] values, string pre="")
        {
            string s = string.Join(",", values);
            Console.WriteLine($"{pre} {s}");
        }
    }
}