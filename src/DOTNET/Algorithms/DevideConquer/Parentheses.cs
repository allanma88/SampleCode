using System;
using System.Collections.Generic;
using System.Linq;

namespace DevideConquer
{
    public class Parentheses
    {
        public void Run()
        {
            string input = "2*3-4*5";
            IList<int> ways = DiffWaysToCompute(input);
            Console.WriteLine(string.Join(',', ways));

            input = "2*3";
            ways = DiffWaysToCompute(input);
            Console.WriteLine(string.Join(',', ways));

            input = "2";
            ways = DiffWaysToCompute(input);
            Console.WriteLine(string.Join(',', ways));
        }

        public IList<int> DiffWaysToCompute(string input)
        {
            List<string> numAndOpStrs = new List<string>();
            List<int> numAndOps = new List<int>();
            string s = "";
            foreach (var c in input)
            {
                if (char.IsDigit(c))
                {
                    s += c;
                }
                else if (c == '+' || c == '-' || c == '*' || c == '/')
                {
                    numAndOpStrs.Add(s);
                    numAndOpStrs.Add(c.ToString());
                    numAndOps.Add(int.Parse(s));
                    numAndOps.Add(-1);
                    s = "";
                }
            }
            numAndOpStrs.Add(s);
            numAndOps.Add(int.Parse(s));

            if (numAndOps.Count == 1)
            {
                return numAndOps;
            }

            var map = Compute(numAndOps, numAndOpStrs);
            return map.Values.ToList();
        }

        private Dictionary<string, int> Compute(List<int> numAndOps, List<string> numAndOpStrs)
        {
            if (numAndOps.Count == 3)
            {
                int n = Compute(numAndOps[0], numAndOpStrs[1], numAndOps[2]);
                string s = $"({numAndOpStrs[0]}{numAndOpStrs[1]}{numAndOpStrs[2]})";
                return new Dictionary<string, int> { { s, n } };
            }

            Dictionary<string, int> map = new Dictionary<string, int>();

            for (int i = 0; i < numAndOps.Count - 2; i += 2)
            {
                int n = Compute(numAndOps[i], numAndOpStrs[i + 1], numAndOps[i + 2]);
                string s = $"({numAndOpStrs[i]}{numAndOpStrs[i + 1]}{numAndOpStrs[i + 2]})";

                List<int> newNumAndOps = new List<int>();
                for (int j = 0; j < i; j++)
                {
                    newNumAndOps.Add(numAndOps[j]);
                }
                newNumAndOps.Add(n);
                for (int j = i + 3; j < numAndOps.Count; j++)
                {
                    newNumAndOps.Add(numAndOps[j]);
                }

                List<string> newNumAndOpStrs = new List<string>();
                for (int j = 0; j < i; j++)
                {
                    newNumAndOpStrs.Add(numAndOpStrs[j]);
                }
                newNumAndOpStrs.Add(s);
                for (int j = i + 3; j < numAndOpStrs.Count; j++)
                {
                    newNumAndOpStrs.Add(numAndOpStrs[j]);
                }

                var subMap = Compute(newNumAndOps, newNumAndOpStrs);
                foreach (var pair in subMap)
                {
                    if (!map.ContainsKey(pair.Key))
                    {
                        map.Add(pair.Key, pair.Value);
                    }
                }
            }

            return map;
        }

        private int Compute(int n1, string op, int n2)
        {
            if (op == "+")
            {
                return n1 + n2;
            }
            else if (op == "-")
            {
                return n1 - n2;
            }
            else if (op == "*")
            {
                return n1 * n2;
            }
            else if (op == "/")
            {
                return n1 / n2;
            }
            else
            {
                throw new Exception("error op: " + op);
            }
        }
    }
}