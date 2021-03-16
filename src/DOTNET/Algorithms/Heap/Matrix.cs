using System;

namespace Heap
{
    public class Matrix
    {
        public void Run()
        {
            int[][] matrix = new int[3][];
            matrix[0] = new int[] { 1, 5, 9 };
            matrix[1] = new int[] { 10, 11, 13 };
            matrix[2] = new int[] { 12, 13, 15 };
            int kSmall = KthSmallest(matrix, 8);
            Console.WriteLine(kSmall);
            for (int i = 0; i < matrix.Length; i++)
            {
                Console.WriteLine(string.Join(',', matrix[i]));
            }
        }

        public int KthSmallest(int[][] matrix, int k)
        {
            int ksmall = 0;
            int rows = matrix.Length;
            int cols = matrix[0].Length;
            int n = rows * cols;
            for (int i = 0; i < k; i++)
            {
                ksmall = matrix[0][0];
                int r = (n - 1) / cols;
                int c = (n - 1) % cols;
                matrix[0][0] = matrix[r][c];
                MinHeapify(matrix, 0, 0);
            }
            return ksmall;
        }

        private void MinHeapify(int[][] matrix, int r, int c)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;
            while (r < rows && c < cols)
            {
                int minR = r;
                int minC = c;
                if (r < rows - 1 && matrix[r + 1][c] < matrix[minR][minC])
                {
                    minR = r + 1;
                    minC = c;
                }
                if (c < cols - 1 && matrix[r][c + 1] < matrix[minR][minC])
                {
                    minR = r;
                    minC = c + 1;
                }
                if (minR != r || minC != c)
                {
                    Exchange(matrix, r, c, minR, minC);
                    r = minR;
                    c = minC;
                }
                else
                {
                    break;
                }
            }
        }

        private void Exchange(int[][] matric, int r1, int c1, int r2, int c2)
        {
            int temp = matric[r1][c1];
            matric[r1][c1] = matric[r2][c2];
            matric[r2][c2] = temp;
        }
    }
}