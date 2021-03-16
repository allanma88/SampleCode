using System;

namespace String
{
    public class SortSample
    {
        public void Run()
        {
            Sort sort = new Sort();
            Console.WriteLine("LSD: ");
            string[] values = new string[] { "baaa", "aaaa", "abaa", "aaac", "aaba", "aaca", "acaa", "aaab", "caaa" };
            Console.WriteLine(string.Join(",", values));
            sort.Lsd(values, 4);
            Console.WriteLine(string.Join(",", values));

            // Console.WriteLine("MSD: ");
            // values = new string[] { "baaa", "aaaa", "abaa", "aaac", "aaba", "aaca", "acaa", "aaab", "caaa" };
            // Console.WriteLine(string.Join(",", values));
            // sort.Msd(values);
            // Console.WriteLine(string.Join(",", values));

            Console.WriteLine("MSD: ");
            values = new string[] { "ab", "a", "abc", "abbcb", "abbcbbbb", "abbcbcc", "abbcbaaaaaaaa", "b", "c" };
            Console.WriteLine(string.Join(",", values));
            sort.Msd(values);
            Console.WriteLine(string.Join(",", values));
        }
    }

    public class Sort
    {
        public void Lsd(string[] values, int len)
        {
            string[] aux = new string[values.Length];
            for (int i = len - 1; i >= 0; i--)
            {
                int[] counts = new int[4];
                foreach (var v in values)
                {
                    int index = v[i] - 'a' + 1;
                    counts[index]++;
                }
                for (int j = 0; j < counts.Length - 1; j++)
                {
                    counts[j + 1] += counts[j];
                }
                foreach (var v in values)
                {
                    int index = v[i] - 'a';
                    aux[counts[index]++] = v;
                }
                for (int j = 0; j < aux.Length; j++)
                {
                    values[j] = aux[j];
                }
            }
        }

        public void Msd(string[] values)
        {
            string[] aux = new string[values.Length];
            Msd(values, aux, 0, values.Length - 1, 0);
        }

        private void Msd(string[] values, string[] aux, int lo, int hi, int d)
        {
            if (lo >= hi) return;
            int len = 3;
            int[] counts = new int[len + 2];
            for (int i = lo; i <= hi; i++)
            {
                string v = values[i];
                int index = CharAt(v, d);
                counts[index + 2]++;
            }
            for (int i = 0; i < len + 1; i++)
            {
                counts[i + 1] += counts[i];
            }
            for (int i = lo; i <= hi; i++)
            {
                string v = values[i];
                int index = CharAt(v, d);
                aux[counts[index + 1]++] = v;
            }
            for (int i = lo; i <= hi; i++)
            {
                values[i] = aux[i - lo];
            }
            for (int i = 2; i < len + 2; i++)
            {
                Msd(values, aux, counts[i - 2], counts[i - 1] - 1, d + 1);
            }
        }

        private int CharAt(string s, int i)
        {
            if (i < s.Length)
            {
                return s[i] - 'a';
            }
            return -1;
        }
    }
}