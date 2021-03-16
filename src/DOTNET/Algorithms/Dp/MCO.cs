using System;
using System.Collections.Generic;
using System.Threading;

namespace Dp
{
    public class MCOSample
    {
        public void Test()
        {
            MCO mco = new MCO();
            int[] matrix;
            matrix = new int[] { 10, 30, 5, 60 };
            Test(mco, matrix);
            matrix = new int[] { 4, 2, 3, 1, 3, 5, 6 };
            Test(mco, matrix);
            //expect answer is 93
            matrix = new int[] { 1, 3, 9, 7, 1 };
            Test(mco, matrix);
            for (int i = 0; i < 5; i++)
            {
                Random random = new Random(i);
                matrix = GetNumbers(random, 5);
                Test(mco, matrix);
                // Thread.Sleep(100);
            }
        }

        private void Test(MCO mco, int[] matrix)
        {
            int min = mco.MatrixChainOrder(matrix);
            int minWithRecur = mco.MatrixChainOrderWithRecur(matrix);
            Console.WriteLine($"The min matrix product of {string.Join(",", matrix)} is {min} and {minWithRecur}(Recur)");
        }

        private int[] GetNumbers(Random random, int n)
        {
            int[] numbers = new int[n];
            HashSet<int> set = new HashSet<int>();
            while (set.Count < n)
            {
                set.Add(random.Next(1, 100));
            }
            int i = 0;
            foreach (var num in set)
            {
                numbers[i++] = num;
            }
            return numbers;
        }
    }

    public class MCO
    {
        public int MatrixChainOrderWithRecur(int[] matrix)
        {
            int min = int.MaxValue;
            var matrixList = new List<int>(matrix);
            for (int i = 1; i < matrix.Length - 1; i++)
            {
                int times = MatrixChainOrderWithRecur(matrixList, i);
                if (times < min)
                {
                    min = times;
                }
            }
            return min;
        }

        private int MatrixChainOrderWithRecur(List<int> matrix, int i)
        {
            int times = matrix[i - 1] * matrix[i] * matrix[i + 1];
            if (matrix.Count == 3)
            {
                return times;
            }
            int temp = matrix[i];
            matrix.RemoveAt(i);
            int min = int.MaxValue;
            for (int j = 1; j < matrix.Count - 1; j++)
            {
                int val = MatrixChainOrderWithRecur(matrix, j);
                if (val < min)
                {
                    min = val;
                }
            }
            matrix.Insert(i, temp);
            return times + min;
        }

        public int MatrixChainOrderWithRecur1(int[] matrix)
        {
            if (matrix.Length < 3) return -1;
            int min = 0;
            for (int i = 1; i < matrix.Length; i++)
            {
                if (matrix[i] < matrix[min])
                {
                    min = i;
                }
            }
            List<int> matrixList = new List<int>(matrix);
            return MatrixChainOrderWithRecur1(matrixList, min);
        }

        private int MatrixChainOrderWithRecur1(List<int> matrix, int min)
        {
            if (matrix.Count == 3)
            {
                return matrix[0] * matrix[1] * matrix[2];
            }
            int times = 0, delIndex = -1;
            if (min - 2 >= 0)
            {
                times = matrix[min - 2] * matrix[min - 1] * matrix[min];
                delIndex = min - 1;
                min--;
            }
            else
            {
                times = matrix[min] * matrix[min + 1] * matrix[min + 2];
                delIndex = min + 1;
            }
            matrix.RemoveAt(delIndex);
            return times + MatrixChainOrderWithRecur1(matrix, min);
        }

        public int MatrixChainOrder1(int[] matrix)
        {
            if (matrix.Length < 3) return -1;
            int min = 0;
            for (int i = 1; i < matrix.Length; i++)
            {
                if (matrix[i] < matrix[min])
                {
                    min = i;
                }
            }
            List<int> matrixList = new List<int>(matrix);
            int times = 0, delIndex = -1;
            while (matrixList.Count > 3)
            {
                if (min - 2 >= 0)
                {
                    times += matrixList[min - 2] * matrixList[min - 1] * matrixList[min];
                    delIndex = min - 1;
                    min--;
                }
                else
                {
                    times += matrixList[min] * matrixList[min + 1] * matrixList[min + 2];
                    delIndex = min + 1;
                }
                matrixList.RemoveAt(delIndex);
            }
            times += matrixList[0] * matrixList[1] * matrixList[2];
            return times;
        }

        public int MatrixChainOrder(int[] matrix)
        {
            int[] newNums = new int[10];
            if (matrix.Length < 3) return -1;
            List<int> minList = new List<int>();
            minList.Add(0);
            for (int i = 1; i < matrix.Length; i++)
            {
                if (matrix[i] < matrix[minList[0]])
                {
                    minList.Clear();
                    minList.Add(i);
                }
                else if (matrix[i] == matrix[minList[0]])
                {
                    minList.Add(i);
                }
            }
            int minTimes = int.MaxValue;
            foreach (int min in minList)
            {
                int times = 0;
                int j = min - 1;
                while (j > 0)
                {
                    times += matrix[j - 1] * matrix[j] * matrix[min];
                    j--;
                }
                int k = min + 1;
                while (k < matrix.Length - 1)
                {
                    times += matrix[min] * matrix[k] * matrix[k + 1];
                    k++;
                }
                if (j >= 0 && k < matrix.Length)
                {
                    times += matrix[j] * matrix[min] * matrix[k];
                }
                minTimes = Math.Min(minTimes, times);
            }
            return minTimes;
        }
    }
}