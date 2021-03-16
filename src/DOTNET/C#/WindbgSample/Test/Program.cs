using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> l1 = new List<string>();
            l1.Add("");
            List<string> l2 = new List<string>();
            List<string> l3 = new List<string>();

            var result = from a in l1
                         from b in l2
                         select new List<string> { a, b };

            Console.WriteLine("Welcome to Advanced .NET Debugging!");
            Console.Out.WriteLine("","");
            Console.ReadKey();
        }
    }
}