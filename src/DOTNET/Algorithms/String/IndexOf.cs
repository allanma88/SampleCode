using System;

namespace String
{
    public class IndexOfSample
    {
        public void Run()
        {
            IndexOfImp indexOf = new IndexOfImp();
            string s = "";
            string t = "";
            Console.WriteLine($"system: {t} indexof {s} : {s.IndexOf(t)}");
            Console.WriteLine($"my: {t} indexof {s} : {indexOf.IndexOf(s, t)}");

            s = "abcdeffghhi";
            t = "ffg";
            Console.WriteLine($"system: {t} indexof {s} : {s.IndexOf(t)}");
            Console.WriteLine($"my: {t} indexof {s} : {indexOf.IndexOf(s, t)}");

            s = "abcdeffghhi";
            t = "w";
            Console.WriteLine($"system: {t} indexof {s} : {s.IndexOf(t)}");
            Console.WriteLine($"my: {t} indexof {s} : {indexOf.IndexOf(s, t)}");

            s = " ";
            t = " ";
            Console.WriteLine($"system: {t} indexof {s} : {s.IndexOf(t)}");
            Console.WriteLine($"my: {t} indexof {s} : {indexOf.IndexOf(s, t)}");

            s = "a ";
            t = " ";
            Console.WriteLine($"system: {t} indexof {s} : {s.IndexOf(t)}");
            Console.WriteLine($"my: {t} indexof {s} : {indexOf.IndexOf(s, t)}");

             s = " a ";
            t = " ";
            Console.WriteLine($"system: {t} indexof {s} : {s.IndexOf(t)}");
            Console.WriteLine($"my: {t} indexof {s} : {indexOf.IndexOf(s, t)}");
        }
    }

    public class IndexOfImp
    {
        public int IndexOf(string s, string t)
        {
            if (t == string.Empty) return 0;
            if (s.Length < t.Length) return -1;
            int i;
            for (i = 0; i < s.Length - t.Length; i++)
            {
                int j;
                for (j = i; j < i + t.Length; j++)
                {
                    if (s[j] != t[j - i])
                        break;
                }
                if (j == i + t.Length)
                    break;
            }
            if (i == s.Length - t.Length) return -1;
            return i;
        }
    }
}