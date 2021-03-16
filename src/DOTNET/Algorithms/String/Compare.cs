using System;

namespace String
{
    public class CompareSample
    {
        public void Run()
        {
            CompareImp compare = new CompareImp();

            string s = "ab";
            string t = "abd";
            Console.WriteLine($"{s} {GetOp(compare.Compare(s, t))} {t}");
        }

        private string GetOp(int cmp)
        {
            if(cmp < 0) return "<";
            else if(cmp > 0) return ">";
            else return "=";
        }
    }

    public class CompareImp
    {
        public int Compare(string s, string t)
        {
            int len = Math.Min(s.Length, t.Length);
            for (int i = 0; i < len; i++)
            {
                int cmp = s[i].CompareTo(t[i]);
                if (cmp != 0) return cmp;
            }
            return s.Length.CompareTo(t.Length);
        }
    }
}