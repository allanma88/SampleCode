using System;

namespace Matrix
{
    public class TraverseSample
    {
        public void Run()
        {
            int[,] matrix = Common.GetMatrix(4, 2, "row");

            Console.WriteLine("Matrix: ");
            Common.Print(matrix);

            Traverse traverse = new Traverse();

            // Console.WriteLine("Row Order: ");
            // int[] rowValues = traverse.Row(matrix);
            // Common.Print(rowValues);

            // Console.WriteLine("Column Order: ");
            // int[] columnValues = traverse.Column(matrix);
            // Common.Print(columnValues);

            // Console.WriteLine("Diagonal Order: ");
            // int[] diagonalValues = traverse.Diagonal(matrix);
            // Common.Print(diagonalValues);

            Console.WriteLine("Diagonal 2 Order: ");
            int[] diagonal2Values = traverse.Diagonal2(matrix);
            Common.Print(diagonal2Values);

            // Console.WriteLine("Reverse Diagonal Order: ");
            // int[] reverseDiagonalValues = traverse.ReverseDiagonal(matrix);
            // Common.Print(reverseDiagonalValues);

            // Console.WriteLine("Reverse Diagonal 2 Order: ");
            // int[] reverseDiagonal2Values = traverse.ReverseDiagonal2(matrix);
            // Common.Print(reverseDiagonal2Values);

            // Console.WriteLine("Spiral Order: ");
            // int[] spiralValues = traverse.Spiral(matrix);
            // Common.Print(spiralValues);
        }
    }

    public class Traverse
    {
        public int[] Row(int[,] matrix)
        {
            int[] values = new int[matrix.Length];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    values[i * matrix.GetLength(1) + j] = matrix[i, j];
                }
            }
            return values;
        }

        public int[] Column(int[,] matrix)
        {
            int[] values = new int[matrix.Length];
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    values[i * matrix.GetLength(0) + j] = matrix[j, i];
                }
            }
            return values;
        }

        public int[] Diagonal(int[,] matrix)
        {
            int[] values = new int[matrix.Length];
            int k = 0;
            int m = 0, n = matrix.GetLength(1) - 1;
            while (m < matrix.GetLength(0))
            {
                int i = m, j = n;
                while (i < matrix.GetLength(0) && j < matrix.GetLength(1))
                {
                    values[k++] = matrix[i++, j++];
                }
                if (--n < 0)
                {
                    m++;
                    n = 0;
                }
            }
            return values;
        }

        public int[] Diagonal2(int[,] matrix)
        {
            int[] values = new int[matrix.Length];
            int i = 0, j = matrix.GetLength(1) - 1;
            int x = -1, y = -1;
            for (int k = 0; k < matrix.Length; k++)
            {
                values[k] = matrix[i, j];
                i += x;
                j += y;
                if (i < 0 || i >= matrix.GetLength(0) || j < 0 || j >= matrix.GetLength(1))
                {
                    if (i < 0 && j < matrix.GetLength(1) && j >= 0)
                    {
                        i = 0;
                    }
                    else if (j >= matrix.GetLength(1) && i < matrix.GetLength(0) && i >= 0)
                    {
                        j = matrix.GetLength(1) - 1;
                    }
                    else if (j < 0)
                    {
                        i += 2;
                        j = 0;
                    }
                    else if (i >= matrix.GetLength(0))
                    {
                        i = matrix.GetLength(0) - 1;
                        j -= 2;
                    }
                    x = -x;
                    y = -y;
                }
            }
            return values;
        }

        public int[] ReverseDiagonal(int[,] matrix)
        {
            int[] values = new int[matrix.Length];
            int k = 0;
            int m = 0, n = 0;
            while (m < matrix.GetLength(0))
            {
                int i = m, j = n;
                while (i < matrix.GetLength(0) && j >= 0)
                {
                    values[k++] = matrix[i++, j--];
                }
                if (++n >= matrix.GetLength(1))
                {
                    m++;
                    n = matrix.GetLength(1) - 1;
                }
            }
            return values;
        }

        public int[] ReverseDiagonal2(int[,] matrix)
        {
            int[] values = new int[matrix.Length];
            int x = -1, y = 1;
            int i = 0, j = 0;
            for (int k = 0; k < matrix.Length; k++)
            {
                values[k] = matrix[i, j];
                i += x;
                j += y;
                if (i < 0 || i >= matrix.GetLength(0) || j < 0 || j >= matrix.GetLength(1))
                {
                    if (i >= matrix.GetLength(0))
                    {
                        i = matrix.GetLength(0) - 1;
                        j += 2;
                    }
                    if (j >= matrix.GetLength(1))
                    {
                        j = matrix.GetLength(1) - 1;
                        i += 2;
                    }
                    if (i < 0)
                    {
                        i = 0;
                    }
                    if (j < 0)
                    {
                        j = 0;
                    }
                    x = -x;
                    y = -y;
                }
            }
            return values;
        }

        public int[] Spiral(int[,] matrix)
        {
            int[] values = new int[matrix.Length];
            int startX = 0, startY = 0, endX = matrix.GetLength(0) - 1, endY = matrix.GetLength(1) - 1;
            int k = 0;
            while (startX <= endX && startY <= endY)
            {
                for (int y = startY; y <= endY; y++)
                {
                    values[k++] = matrix[startX, y];
                }
                for (int x = startX + 1; x <= endX; x++)
                {
                    values[k++] = matrix[x, endY];
                }
                if (startX != endX)
                {
                    for (int y = endY - 1; y >= startY; y--)
                    {
                        values[k++] = matrix[endX, y];
                    }
                }
                if (startY != endY)
                {
                    for (int x = endX - 1; x > startX; x--)
                    {
                        values[k++] = matrix[x, startY];
                    }
                }
                startX++;
                endX--;
                startY++;
                endY--;
            }
            return values;
        }
    }
}