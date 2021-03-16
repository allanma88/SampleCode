using System;
using System.Diagnostics;

namespace SortResearch
{
    class Program
    {
        public static void Main(string[] args)
        {
            string sortType = "bubble";
            string sortOrder = "random";
            if (args.Length >= 2)
            {
                sortType = args[0];
                sortOrder = args[1];
            }
            else if (args.Length >= 1)
            {
                sortType = args[0];
            }
            System.Console.WriteLine("{0} sort {1} order", sortType, sortOrder);
            SortBase sort = GetSort(sortType);
            Run(sort, sortOrder);
            //Time(sort, "asc");
            //Time(sort, "desc");
            //Time(sort, "random", 1, 1000000);
        }

        public static void Run(SortBase sort, string sortType)
        {
            int length = 10;
            int[] a = GetArray(sortType, length);
            string.Join(",", a).Dump();
            sort.Sort(a);
            Assert(a).Dump();
            string.Join(",", a).Dump();

            System.Console.WriteLine("compare content : {0}", sort.Statistic.CompareContent);
            System.Console.WriteLine("exchange content : {0}", sort.Statistic.ExchangeContent);
            System.Console.WriteLine("access content : {0}", sort.Statistic.AccessContent);
        }

        public static void Time(SortBase sort, string sortType, int count = 100, int length = 10000)
        {
            long total = 0;
            for (int j = 0; j < count; j++)
            {
                int[] a = GetArray(sortType, length);
                Stopwatch watch = Stopwatch.StartNew();
                sort.Sort(a);
                long elapsed = watch.ElapsedMilliseconds;
                total += elapsed;
            }
            string.Format("sort {0} elements need {1} ms in average of {2}", length, total / count, sortType).Dump();
        }

        public static int[] GetArray(string sortType, int length)
        {
            int[] a = new int[length];
            if (sortType == "random")
            {
                Random random = new Random();
                for (int i = 0; i < length; i++)
                {
                    a[i] = random.Next(10, 1000);
                }
            }
            else if (sortType == "desc")
            {
                for (int i = 0; i < length; i++)
                {
                    a[i] = length - i;
                }
            }
            else if (sortType == "asc")
            {
                for (int i = 0; i < length; i++)
                {
                    a[i] = i + 1;
                }
            }
            return a;
        }

        private static SortBase GetSort(string sortType)
        {
            sortType = sortType.ToLower();
            switch (sortType)
            {
                case "bubble":
                    return new BubbleSort();

                case "selection":
                    return new SelectionSort();

                case "insertion":
                    return new InsertionSort();

                case "shell":
                    return new ShellSort();

                case "merge":
                    return new MergeSort();

                case "mergebu":
                    return new MergeBUSort();

                case "quick":
                    return new QuickSort();

                case "heap":
                    return new HeapSort();

                default:
                    return null;
            }
        }

        public static bool Assert(int[] a)
        {
            for (int i = 1; i < a.Length - 1; i++)
            {
                if (a[i - 1] > a[i] || a[i] > a[i + 1])
                {
                    return false;
                }
            }
            return true;
        }
    }

    public static class DumpHelper
    {
        public static void Dump(this string s)
        {
            Console.WriteLine(s);
        }

        public static void Dump(this bool b)
        {
            Console.WriteLine(b);
        }
    }
}