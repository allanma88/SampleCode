using System;

namespace Matrix
{
    public class SearchSample
    {
        public void Run()
        {
            Search search = new Search();
            int[,] matrix = Common.GetMatrix(3, 3, "row");
            // int[,] matrix = new int[1, 0];
            Console.WriteLine("In Row Order");
            Common.Print(matrix);
            Search(search, matrix, 1);
            Search(search, matrix, 7);

            matrix = Common.GetMatrix(3, 3, "asc");
            // matrix = new int[1, 0];
            Console.WriteLine("In Asc Order");
            Common.Print(matrix);
            Search(search, matrix, 3);
            Search(search, matrix, 7);
        }

        private void Search(Search search, int[,] matrix, int target)
        {
            var location = search.SearchInRowOrder(matrix, target);
            Console.WriteLine($"{target} in x={location.Item1}, y={location.Item2}");
        }
    }

    public class Search
    {
        public Tuple<int, int> SearchInRowOrder(int[,] matrix, int target)
        {
            int start = 0;
            int end = matrix.Length - 1;
            while (start <= end)
            {
                int mid = (start + end) / 2;
                int x = mid / matrix.GetLength(1);
                int y = mid % matrix.GetLength(1);
                if (target < matrix[x, y])
                {
                    end = mid - 1;
                }
                else if (target > matrix[x, y])
                {
                    start = mid + 1;
                }
                else
                {
                    return new Tuple<int, int>(x, y);
                }
            }
            return new Tuple<int, int>(-1, -1);
        }

        public Tuple<int, int> SearchInAscOrder_Old(int[,] matrix, int target)
        {
            int x = 0, y = 0;
            while (x < matrix.GetLength(0) && y < matrix.GetLength(1))
            {
                int startY = y, endY = matrix.GetLength(1) - 1;
                while (startY <= endY)
                {
                    int midY = (startY + endY) / 2;
                    if (target < matrix[x, midY])
                    {
                        endY = midY - 1;
                    }
                    else if (target > matrix[x, midY])
                    {
                        startY = midY + 1;
                    }
                    else
                    {
                        return new Tuple<int, int>(x, midY);
                    }
                }
                int startX = x, endX = matrix.GetLength(0) - 1;
                while (startX <= endX)
                {
                    int midX = (startX + endX) / 2;
                    if (target < matrix[midX, y])
                    {
                        endX = midX - 1;
                    }
                    else if (target > matrix[midX, y])
                    {
                        startX = midX + 1;
                    }
                    else
                    {
                        return new Tuple<int, int>(midX, y);
                    }
                }
                x++;
                y++;
            }
            return new Tuple<int, int>(-1, -1);
        }

        public Tuple<int, int> SearchInAscOrder(int[,] matrix, int target)
        {
            if (matrix == null || matrix.GetLength(0) < 1 || matrix.GetLength(1) < 1)
            {
                return new Tuple<int, int>(-1, -1);
            }
            int col = matrix.GetLength(1) - 1;
            int row = 0;
            while (col >= 0 && row <= matrix.GetLength(0) - 1)
            {
                if (target == matrix[row, col])
                {
                    return new Tuple<int, int>(row, col);
                }
                else if (target < matrix[row, col])
                {
                    col--;
                }
                else if (target > matrix[row, col])
                {
                    row++;
                }
            }
            return new Tuple<int, int>(-1, -1);
        }
    }
}