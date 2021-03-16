using System;
using System.Text;

namespace Matrix
{
    public class Common
    {
        public static void Print(int[] values)
        {
            Console.WriteLine(string.Join(",", values));
        }

        public static void Print(int[,] values)
        {
            for (int i = 0; i < values.GetLength(0); i++)
            {
                StringBuilder builder = new StringBuilder();
                for (int j = 0; j < values.GetLength(1); j++)
                {
                    builder.Append(values[i, j].ToString("D2")).Append(",");
                }
                if(builder.Length > 0)
                {
                    builder.Remove(builder.Length - 1, 0);
                }
                Console.WriteLine(builder);
            }
        }

        public static int[,] GetMatrix(int row, int col, string type)
        {
            int[,] matrix = new int[row, col];
            switch (type)
            {
                case "row":
                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            matrix[i, j] = i * matrix.GetLength(0) + j + 1;
                        }
                    }
                    break;
                case "asc":
                    for (int j = 0; j < col; j++)
                    {
                        for (int i = 0; i < row; i++)
                        {
                            matrix[i, j] = (j + 1) * (i + 1);
                        }
                    }
                    break;
                default:
                    break;
            }
            return matrix;
        }
    }
}