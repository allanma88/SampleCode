using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SearchResearch
{
    class Program
    {
        static void Main(string[] args)
        {
            // SampleClient sampleClient = new SampleClient();
            // sampleClient.Run(GetSt(20));

            //OrderSampleClient orderSampleClient = new OrderSampleClient();
            //orderSampleClient.Run(GetOrderSt());

            //PerformanceClient perfClient = new PerformanceClient();
            //perfClient.Run(GetSt(23), "tinyTale.txt", 1);
            //perfClient.Run(GetSt(10677), "tale.txt", 1);
            //perfClient.Run(GetSt(54997), "leipzig1M.txt", 1);

            SearchHelper searchHelper = new SearchHelper();
            string stType = "binarysearch";
            string insertType = "random";
            if (args.Length > 1)
            {
                stType = args[0];
                insertType = args[1];
            }
            else if (args.Length > 0)
            {
                stType = args[0];
            }
            searchHelper.PerfRun(stType, insertType);
        }

        private static STBase<string, int> GetSt(int cap)
        {
            STBase<string, int> st;
            //st = new SequentialSearchST<string, int>();
            //st = new BinarySearchST<string, int>(10000000);
            //st = new BinarySearchTree_Loop<string, int>();
            //st = new BinarySearchTree_Recur<string, int>();
            //st = new RedBlackBST<string, int>();
            st = new SeparateChainingHashST<string, int>(cap);
            //st = new LinearProbingHashST<string, int>();
            return st;
        }

        private static OrderedSTBase<string, int> GetOrderSt()
        {
            OrderedSTBase<string, int> st;
            //st = new BinarySearchST<string, int>(10000000);
            //st = new BinarySearchTreeSt_Loop<string, int>();
            //st = new BinarySearchTree_Recur<string, int>();
            st = new RedBlackBST<string, int>();
            return st;
        }
    }

    public class OrderSampleClient
    {
        public void Run(OrderedSTBase<string, int> st)
        {
            string s = "SEARCHEXAMPLE";
            //string s = "SEBRCHEXBMPLE";
            var chars = s.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                st.Put(chars[i].ToString(), i);
            }
            foreach (string key in st.Keys())
            {
                Console.Write("{0}", key);
            }
            Console.WriteLine();

            //string key = "T";

            //string floorValue = st.Floor(key);
            //Console.WriteLine("{0} floor value is {1}", key, floorValue);

            //string ceilingValue = st.Ceiling(key);
            //Console.WriteLine("{0} ceiling value is {1}", key, ceilingValue);

            //int rank = 3;
            //key = st.Select(rank);
            //Console.WriteLine("Rank {0} key is {1}", rank, key);

            //key = "I";
            //rank = st.Rank(key);
            //Console.WriteLine("Key {0} rank is {1}", key, rank);


            //while (!st.IsEmpty())
            //{
            //    //st.DeleteMin();
            //    st.DeleteMax();
            //    foreach (string key in st.Keys())
            //    {
            //        Console.Write("{0}", key);
            //    }
            //    Console.WriteLine();
            //}


            st.Delete("E");
            foreach (string key in st.Keys())
            {
                Console.Write("{0}", key);
            }
            Console.WriteLine();

            //var indics = GetIndics(s, "random");
            //var indics = GetIndics(s, "order");
            //foreach (var index in indics)
            //{
            //    string k = s[index].ToString();
            //    Console.WriteLine("delete {0}", k);
            //    st.Delete(k);
            //    foreach (string key in st.Keys())
            //    {
            //        Console.Write("{0}", key);
            //    }
            //    Console.WriteLine();
            //}

            //var keys = st.Keys("E", "P");
            //Console.WriteLine(string.Join(",", keys));
        }

        private static List<int> GetIndics(string s, string type)
        {
            List<int> indics = new List<int>();
            Random random = new Random();
            for (int i = 0; i < s.Length; i++)
            {
                if (type == "random")
                {
                    int r;
                    while (true)
                    {
                        r = random.Next(0, s.Length);
                        if (!indics.Contains(r))
                        {
                            indics.Add(r);
                            break;
                        }
                    }
                }
                else
                {
                    indics.Add(i);
                }

            }
            return indics;
        }
    }

    public class SampleClient
    {
        public void Run(STBase<string, int> st)
        {
            string s = "SEARCHEXAMPLE";
            //string s = "SEARCHX";
            var chars = s.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                st.Put(chars[i].ToString(), i);
            }
            foreach (string key in st.Keys())
            {
                Console.Write("{0}:{1} ", key, st.Get(key));
            }
            Console.WriteLine();
            Console.WriteLine(st.Size());

            //st.Delete("C");
            //var v = st.Get("H");
            //Console.WriteLine(v);

            //for (int i = 0; i < chars.Length; i++)
            //{
            //    string k = chars[i].ToString();
            //    Console.WriteLine("Delete {0}", k);
            //    st.Delete(k);
            //    foreach (string key in st.Keys())
            //    {
            //        Console.Write("{0}:{1} ", key, st.Get(key));
            //    }
            //    Console.WriteLine();
            //}
        }
    }

    public class PerformanceClient
    {
        public void Run(STBase<string, int> st, string path, int minlen)
        {
            int wordCount = 0;
            Stopwatch watch = Stopwatch.StartNew();
            using (Stream stream = new FileStream(path, FileMode.Open))
            using (StreamReader reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                string[] lines = content.Split('\n', '\r');
                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] words = line.Split(' ');
                        foreach (string word in words)
                        {
                            if (word.Length < minlen)
                            {
                                continue;
                            }
                            else
                            {
                                wordCount++;
                                if (st.Contains(word))
                                {
                                    st.Put(word, st.Get(word) + 1);
                                }
                                else
                                {
                                    st.Put(word, 1);
                                }
                            }
                        }
                    }
                }
            }

            string max = "";
            st.Put(max, 0);
            int keyCount = 0;
            foreach (string key in st.Keys())
            {
                keyCount++;
                if (st.Get(key) > st.Get(max))
                {
                    max = key;
                }
            }
            Console.WriteLine("Words: {0}, Keys: {1}", wordCount, keyCount);
            Console.WriteLine("{0} {1} ({2}ms)", max, st.Get(max), watch.ElapsedMilliseconds);
        }
    }

    public class SearchHelper
    {
        public void PerfRun(string stType, string insertType)
        {
            System.Console.WriteLine("{0} symbole table, {1} insert order", stType, insertType);
            int count = 1000 * 1000;
            STBase<int, string> st = GetST(count, stType);
            Stopwatch sw = Stopwatch.StartNew();
            Insert(insertType, st, count);
            System.Console.WriteLine(sw.Elapsed);
        }

        public STBase<int, string> GetST(int n, string stType)
        {
            STBase<int, string> st = null;
            switch (stType)
            {
                case "binarysearch":
                    st = new BinarySearchST<int, string>(n);
                    break;

                case "bst":
                case "bstloop":
                    st = new BinarySearchTree_Loop<int, string>();
                    break;

                case "bstrecur":
                    st = new BinarySearchTree_Recur<int, string>();
                    break;

                case "hash":
                case "schash":
                    st = new SeparateChainingHashST<int, string>();
                    break;

                case "lphash":
                    st = new LinearProbingHashST<int, string>();
                    break;

                default:
                    break;
            }
            return st;
        }

        public void Insert(string insertType, STBase<int, string> st, int count)
        {
            switch (insertType)
            {
                case "asc":
                    for (int i = 0; i < count; i++)
                    {
                        st.Put(i, "a");
                    }
                    break;

                case "desc":
                    for (int i = count; i > 0; i--)
                    {
                        st.Put(i, "a");
                    }
                    break;

                case "random":
                    Random random = new Random();
                    for (int i = 0; i < count; i++)
                    {
                        int j = random.Next(0, count);
                        st.Put(j, "a");
                    }
                    break;

                default:
                    break;
            }
        }
    }
}