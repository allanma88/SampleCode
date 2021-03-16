using System;

namespace DevideConquer
{
    public class Matrix
    {
        public void Run()
        {
            int n = 4;
            // int[,] A = new int[n, n];
            // A[0, 0] = 1;
            // A[0, 1] = 2;
            // A[1, 0] = 3;
            // A[1, 1] = 4;

            // int[,] B = new int[n, n];
            // B[0, 0] = 1;
            // B[0, 1] = 2;
            // B[1, 0] = 3;
            // B[1, 1] = 4;
            int[,] A = Make(n);
            int[,] B = Make(n);

            int[,] C = Multiply(A, B, n);
            Dump(A, B, C, n);
            C = Multiply_Recur(A, B, n);
            Dump(A, B, C, n);
        }

        private int[,] Make(int n)
        {
            Random random = new Random();
            int[,] A = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i,j] = random.Next(10,50);
                }
            }
            return A;
        }

        public int[,] Multiply_Recur(int[,] A, int[,] B, int n)
        {
            int[,] C = new int[n, n];
            Range rA = new Range(0, 0, n);
            Range rB = new Range(0, 0, n);
            Range rC = new Range(0, 0, n);
            Multiply_Recur(A, B, C, rA, rB, rC, n);
            return C;
        }

        private void Multiply_Recur(int[,] A, int[,] B, int[,] C, Range rA, Range rB, Range rC, int n)
        {
            if (n == 1)
            {
                C[rC.R, rC.C] += A[rA.R, rA.C] * B[rB.R, rB.C];
                return;
            }

            var rAs = rA.Split();
            var rBs = rB.Split();
            var rCs = rC.Split();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        Multiply_Recur(A, B, C, rAs[i, k], rBs[k, j], rCs[i, j], n / 2);
                    }
                }
            }
        }

        public int[,] Multiply(int[,] A, int[,] B, int n)
        {
            int[,] C = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < n; k++)
                    {
                        sum += A[i, k] * B[k, j];
                    }
                    C[i, j] = sum;
                }
            }
            return C;
        }

        private void Dump(int[,] A, int[,] B, int[,] C, int n)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(A[i, j] + " ");
                }
                Console.Write("\t");
                for (int j = 0; j < n; j++)
                {
                    Console.Write(B[i, j] + " ");
                }
                Console.Write("\t");
                for (int j = 0; j < n; j++)
                {
                    Console.Write(C[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        private class Range
        {
            internal Range(int r, int c, int n)
            {
                R = r;
                C = c;
                N = n;
            }

            internal int R { get; set; }

            internal int C { get; set; }

            internal int N { get; set; }

            internal Range[,] Split()
            {
                Range r11 = new Range(R, C, N / 2);
                Range r12 = new Range(R, C + N / 2, N / 2);
                Range r21 = new Range(R + N / 2, C, N / 2);
                Range r22 = new Range(R + N / 2, C + N / 2, N / 2);
                return new Range[2, 2] { { r11, r12 }, { r21, r22 } };
            }
        }
    }
}