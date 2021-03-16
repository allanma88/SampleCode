using System;
using System.Linq;

namespace BinarySearch
{
    class Program
    {
        private static Random random = new Random(DateTime.Now.Millisecond);

        static void Main(string[] args)
        {
            var search = new Search();
            var numberTypes = new string[] { "same", "default" };
            var counts = new int[] { 1, 2, 10 };
            var targetTypes = new string[] { "first", "last", "<first", ">last", "default" };
            var searchFuncs = new Func<int[], int, int>[] {
                search.SearchEqual, search.SearchNotLessThan, search.SearchLessThan,
                search.SearchNotGreaterThan, search.SearchGreaterThan
            };
            foreach (var numberType in numberTypes)
            {
                Console.WriteLine($"number type: {numberType}");
                foreach (var count in counts)
                {
                    int[] numbers = GetNumbers(count, numberType);
                    foreach (var searchFunc in searchFuncs)
                    {
                        Console.WriteLine(searchFunc.Method.Name);
                        foreach (var targetType in targetTypes)
                        {
                            int target = GetTarget(numbers, targetType);
                            int index = searchFunc(numbers, target);
                            Print(numbers, targetType, target, index);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        private static void Print(int[] numbers, string targetType, int target, int index)
        {
            string s = string.Join(",", numbers);
            Console.WriteLine($"{target}[{targetType}] in [{s}] is {index}");
        }

        private static int GetTarget(int[] numbers, string type = "default")
        {
            int target;
            switch (type)
            {
                case "first":
                    target = numbers.First();
                    break;

                case "last":
                    target = numbers.Last();
                    break;

                case "<first":
                    target = numbers.First() - 1;
                    break;

                case ">last":
                    target = numbers.Last() + 1;
                    break;

                default:
                    target = numbers[random.Next(0, numbers.Length - 1)];
                    break;
            }
            return target;
        }

        private static int[] GetNumbers(int n, string type = "default")
        {
            int[] numbers = new int[n];
            switch (type)
            {
                case "same":
                    int num = random.Next(10, 1000);
                    for (int i = 0; i < n; i++)
                    {
                        numbers[i] = num;
                    }
                    break;

                default:
                    for (int i = 0; i < n; i++)
                    {
                        numbers[i] = random.Next(10, 1000);
                    }
                    break;
            }

            Array.Sort(numbers);
            return numbers;
        }
    }
}