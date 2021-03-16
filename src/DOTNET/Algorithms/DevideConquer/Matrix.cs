using System;

namespace DevideConquer
{
    public class Matrix
    {
        public void Run()
        {
            int[,] matrix = new int[5, 5]
            {
                {1,4,7,11,15},
                {2,5,8,12,19},
                {3,6,9,16,22},
                {10,13,14,17,24},
                {18,21,23,26,30}
            };
            int target = 5;
            bool exist = SearchMatrix(matrix, target);
            Console.WriteLine(target + ":" + exist);
            target = 20;
            exist = SearchMatrix(matrix, target);
            Console.WriteLine(target + ":" + exist);
        }

        public bool SearchMatrix(int[,] matrix, int target)
        {
            return SearchMatrix(matrix, target, 0, matrix.GetLength(0) - 1, 0, matrix.GetLength(1) - 1);
        }

        private bool SearchMatrix(int[,] matrix, int target, int startR, int endR, int startC, int endC)
        {
            if (startR > endR || startC > endC)
            {
                return false;
            }
            if (startR == endR && startC == endC)
            {
                return target == matrix[startR, startC];
            }
            if (target < matrix[startR, startC] || target > matrix[endR, endC])
            {
                return false;
            }
            int midR = (startR + endR) / 2;
            int midC = (startC + endC) / 2;
            if (SearchMatrix(matrix, target, startR, midR, startC, midC))
            {
                return true;
            }
            if (SearchMatrix(matrix, target, startR, midR, midC + 1, endC))
            {
                return true;
            }
            if (SearchMatrix(matrix, target, midR + 1, endR, startC, midC))
            {
                return true;
            }
            if (SearchMatrix(matrix, target, midR + 1, endR, midC + 1, endC))
            {
                return true;
            }
            return false;
        }
    }
}