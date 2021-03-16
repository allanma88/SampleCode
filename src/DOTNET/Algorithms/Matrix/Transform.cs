using System;

namespace Matrix
{
    public class TransformSample
    {
        public void Run()
        {
            Transform transform = new Transform();

            // int[,] matrix = Common.GetMatrix(3, 3, "row");
            // Console.WriteLine("Clockwise Transform");
            // Console.WriteLine("Before: ");
            // Common.Print(matrix);
            // transform.Clockwise(matrix);
            // Console.WriteLine("After: ");
            // Common.Print(matrix);

            var matrix = Common.GetMatrix(6, 6, "row");
            Console.WriteLine("Clockwise Transform");
            Console.WriteLine("Before: ");
            Common.Print(matrix);
            transform.Clockwise(matrix);
            Console.WriteLine("After: ");
            Common.Print(matrix);

            // matrix = Common.GetMatrix(5, 5, "row");
            // Console.WriteLine("Clockwise Transform");
            // Console.WriteLine("Before: ");
            // Common.Print(matrix);
            // transform.Clockwise(matrix);
            // Console.WriteLine("After: ");
            // Common.Print(matrix);
        }
    }

    public class Transform
    {
        public void Clockwise(int[,] matrix)
        {
            int step = 0;
            while (step < matrix.GetLength(0) / 2)
            {
                int startX = step;
                int endX = matrix.GetLength(0) - 1 - step;
                int startY = step;
                int endY = matrix.GetLength(1) - 1 - step;
                for (int y = startY; y < endY; y++)
                {
                    int x1 = startX, y1 = y;
                    int x2 = startX + y - startY, y2 = endY;
                    int x3 = endX, y3 = endY - (x2 - startX);
                    int x4 = y3, y4 = startY;
                    int temp1 = matrix[x1, y1];
                    matrix[x1, y1] = matrix[x4, y4];
                    int temp2 = matrix[x2, y2];
                    matrix[x2, y2] = temp1;
                    int temp3 = matrix[x3, y3];
                    matrix[x3, y3] = temp2;
                    int temp4 = matrix[x4, y4];
                    matrix[x4, y4] = temp3;
                }
                step++;
            }
        }

        public void AntiClockwise(int[,] matrix)
        {

        }
    }
}