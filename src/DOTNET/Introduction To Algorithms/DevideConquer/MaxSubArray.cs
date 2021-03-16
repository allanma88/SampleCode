using System;
using System.Diagnostics;

namespace DevideConquer
{
    public class MaxSubArray
    {
        public void Run()
        {
            int[] A = new int[] { -2, 6, 10, -7, -14, 19, 15, -1 };
            // A = new int[] { -20, 15, -12, -9, 10, -5, -6, -13, -5, 8 };
            // A = p.Make(10);
            // Console.WriteLine(string.Join(',', A));
            // var pair = p.FindMaxSubArray_Force(A);
            // Console.WriteLine($"[{pair.Item1} - {pair.Item2}: {pair.Item3}]");
            // pair = p.FindMaxSubArray_Recur(A);
            // Console.WriteLine($"[{pair.Item1} - {pair.Item2}: {pair.Item3}]");
            // pair = p.FindMaxSubArray_Loop(A);
            // Console.WriteLine($"[{pair.Item1} - {pair.Item2}: {pair.Item3}]");
            for (int i = 4; i <= 20; i++)
            {
                int n = i * 10;
                // int n = 70;
                Console.WriteLine($"{n} numbers");
                A = Make(n);
                for (int j = 0; j < 5; j++)
                {
                    Time(() => FindMaxSubArray_Force(A), "force");
                    Time(() => FindMaxSubArray_Recur(A), "recur");
                    Time(() => FindMaxSubArray_Recur1(A), "recur1");
                    Time(() => FindMaxSubArray_Loop(A), "loop");
                }
            }
        }

        private void Time(Action action, string msg)
        {
            TimeSpan elpased = new TimeSpan();
            for (int i = 0; i < 5; i++)
            {
                Stopwatch watch = Stopwatch.StartNew();
                action();
                elpased += watch.Elapsed;
            }
            Console.WriteLine($"{msg}: {elpased / 5}");
        }

        private int[] Make(int n)
        {
            Random random = new Random();
            int[] A = new int[n];
            for (int i = 0; i < n; i++)
            {
                A[i] = random.Next(-n * 2, n * 2);
            }
            return A;
        }

        private Tuple<int, int, int> FindMaxSubArray_Loop(int[] A)
        {
            int maxSum = int.MinValue, maxLeft = -1, maxRight = -1;
            int sum = 0, curLeft = 0;
            for (int i = 0; i < A.Length; i++)
            {
                sum += A[i];
                if (A[i] > sum)
                {
                    sum = A[i];
                    curLeft = i;
                }
                if (sum > maxSum)
                {
                    maxSum = sum;
                    maxLeft = curLeft;
                    maxRight = i;
                }
            }
            return new Tuple<int, int, int>(maxLeft, maxRight, maxSum);
        }

        private Tuple<int, int, int> FindMaxSubArray_Force(int[] A)
        {
            int maxSum = int.MinValue;
            int min = -1, max = -1;
            for (int i = 0; i < A.Length - 1; i++)
            {
                int sum = 0;
                for (int j = i; j < A.Length; j++)
                {
                    sum += A[j];
                    if (sum > maxSum)
                    {
                        min = i;
                        max = j;
                        maxSum = sum;
                    }
                }
            }
            return new Tuple<int, int, int>(min, max, maxSum);
        }

        private Tuple<int, int, int> FindMaxSubArray_Recur(int[] A)
        {
            return FindMaxSubArray_Recur(A, 0, A.Length - 1);
        }

        private Tuple<int, int, int> FindMaxSubArray_Recur(int[] A, int lo, int hi)
        {
            if (lo == hi)
            {
                return new Tuple<int, int, int>(lo, hi, A[lo]);
            }
            int mid = (lo + hi) / 2;
            var leftPair = FindMaxSubArray_Recur(A, lo, mid);
            var rightPair = FindMaxSubArray_Recur(A, mid + 1, hi);
            var crossPair = FindMaxCrossSubArray(A, lo, mid, hi);
            if (leftPair.Item3 > rightPair.Item3 && leftPair.Item3 > crossPair.Item3)
            {
                return leftPair;
            }
            else if (rightPair.Item3 > leftPair.Item3 && rightPair.Item3 > crossPair.Item3)
            {
                return rightPair;
            }
            else
            {
                return crossPair;
            }
        }

        private Tuple<int, int, int> FindMaxCrossSubArray(int[] A, int lo, int mid, int hi)
        {
            int leftMax = int.MinValue;
            int leftSum = 0;
            int min = mid;
            for (int i = mid; i >= lo; i--)
            {
                leftSum += A[i];
                if (leftSum > leftMax)
                {
                    leftMax = leftSum;
                    min = i;
                }
            }

            int rightMax = int.MinValue;
            int rightSum = 0;
            int max = mid + 1;
            for (int i = mid + 1; i <= hi; i++)
            {
                rightSum += A[i];
                if (rightSum > rightMax)
                {
                    rightMax = rightSum;
                    max = i;
                }
            }
            return new Tuple<int, int, int>(min, max, leftMax + rightMax);
        }

        private Tuple<int, int, int> FindMaxSubArray_Recur1(int[] A)
        {
            return FindMaxSubArray_Recur1(A, 0, A.Length - 1);
        }

        private Tuple<int, int, int> FindMaxSubArray_Force(int[] A, int lo, int hi)
        {
            int maxSum = int.MinValue;
            int min = -1, max = -1;
            for (int i = lo; i <= hi; i++)
            {
                int sum = 0;
                for (int j = i; j <= hi; j++)
                {
                    sum += A[j];
                    if (sum > maxSum)
                    {
                        min = i;
                        max = j;
                        maxSum = sum;
                    }
                }
            }
            return new Tuple<int, int, int>(min, max, maxSum);
        }

        private Tuple<int, int, int> FindMaxSubArray_Recur1(int[] A, int lo, int hi)
        {
            if (hi - lo <= 70)
            {
                return FindMaxSubArray_Force(A, lo, hi);
            }
            int mid = (lo + hi) / 2;
            var leftPair = FindMaxSubArray_Recur1(A, lo, mid);
            var rightPair = FindMaxSubArray_Recur1(A, mid + 1, hi);
            var crossPair = FindMaxCrossSubArray1(A, lo, mid, hi);
            if (leftPair.Item3 > rightPair.Item3 && leftPair.Item3 > crossPair.Item3)
            {
                return leftPair;
            }
            else if (rightPair.Item3 > leftPair.Item3 && rightPair.Item3 > crossPair.Item3)
            {
                return rightPair;
            }
            else
            {
                return crossPair;
            }
        }

        private Tuple<int, int, int> FindMaxCrossSubArray1(int[] A, int lo, int mid, int hi)
        {
            int leftMax = int.MinValue;
            int leftSum = 0;
            int min = mid;
            for (int i = mid; i >= lo; i--)
            {
                leftSum += A[i];
                if (leftSum > leftMax)
                {
                    leftMax = leftSum;
                    min = i;
                }
            }

            int rightMax = int.MinValue;
            int rightSum = 0;
            int max = mid + 1;
            for (int i = mid + 1; i <= hi; i++)
            {
                rightSum += A[i];
                if (rightSum > rightMax)
                {
                    rightMax = rightSum;
                    max = i;
                }
            }
            return new Tuple<int, int, int>(min, max, leftMax + rightMax);
        }
    }
}