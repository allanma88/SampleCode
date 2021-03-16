using System;
using System.Collections.Generic;

namespace Classic
{
    class Program
    {
        static void Main(string[] args)
        {
            // Queue queue = new Queue();
            // queue.Run(6);

            Sudoku sudoku = new Sudoku();
            sudoku.Solve(GetBoard());

            // DanceLink link = new DanceLink();
            // int[,] matrix = GetMatrix();
            // var rows = link.Dance(matrix);
            // Console.WriteLine(string.Join(",", rows));
        }

        private static char[,] GetBoard()
        {
            // char[,] board = new char[,]
            // {
            //     {'5','3','.','.','7','.','.','.','.'},
            //     {'6','.','.','1','9','5','.','.','.'},
            //     {'.','9','8','.','.','.','.','6','.'},
            //     {'8','.','.','.','6','.','.','.','3'},
            //     {'4','.','.','8','.','3','.','.','1'},
            //     {'7','.','.','.','2','.','.','.','6'},
            //     {'.','6','.','.','.','.','2','8','.'},
            //     {'.','.','.','4','1','9','.','.','5'},
            //     {'.','.','.','.','8','.','.','7','9'}
            // };

            char[,] board = new char[,]
            {
                {'.','.','9','7','4','8','.','.','.'},
                {'7','.','.','.','.','.','.','.','.'},
                {'.','2','.','1','.','9','.','.','.'},
                {'.','.','7','.','.','.','2','4','.'},
                {'.','6','4','.','1','.','5','9','.'},
                {'.','9','8','.','.','.','3','.','.'},
                {'.','.','.','8','.','3','.','2','.'},
                {'.','.','.','.','.','.','.','.','6'},
                {'.','.','.','2','7','5','9','.','.'}
            };

            // char[,] board = new char[,]
            // {
            //     {'2','.','.','.'},
            //     {'.','.','1','.'},
            //     {'.','.','.','4'},
            //     {'.','3','.','.'},
            // };
            return board;
        }

        private static int[,] GetMatrix()
        {
            // int[,] matrix = new int[5, 6];
            // matrix[0, 0] = 1;
            // matrix[0, 3] = 1;
            // matrix[1, 1] = 1;
            // matrix[1, 5] = 1;
            // matrix[2, 2] = 1;
            // matrix[3, 4] = 1;
            // matrix[4, 0] = 1;
            // matrix[4, 3] = 1;

            int[,] matrix = new int[6, 7];
            matrix[0, 0] = 1;
            matrix[0, 3] = 1;
            matrix[0, 6] = 1;

            matrix[1, 0] = 1;
            matrix[1, 3] = 1;

            matrix[2, 3] = 1;
            matrix[2, 4] = 1;
            matrix[2, 6] = 1;

            matrix[3, 2] = 1;
            matrix[3, 4] = 1;
            matrix[3, 5] = 1;

            matrix[4, 1] = 1;
            matrix[4, 2] = 1;
            matrix[4, 5] = 1;
            matrix[4, 6] = 1;

            matrix[5, 1] = 1;
            matrix[5, 6] = 1;

            return matrix;
        }
    }
}