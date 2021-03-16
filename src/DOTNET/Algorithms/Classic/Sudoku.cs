using System;
using System.Collections.Generic;
using System.Linq;

namespace Classic
{
    public class MySudoku
    {
        public void Run(char[,] board)
        {
            // Statistic(board);
            SolveSudoku(board);
            Dump(board);
            // Statistic(board);
        }

        private void Dump(char[,] board)
        {
            for (uint x = 0; x < board.GetLength(0); x++)
            {
                for (uint y = 0; y < board.GetLength(1); y++)
                {
                    Console.Write(board[x, y] + " ");
                }
                Console.WriteLine();
            }
        }

        private void Statistic(char[,] board)
        {
            for (uint x = 0; x < board.GetLength(0); x++)
            {
                int len = 0;
                for (uint y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y] != '.')
                    {
                        len++;
                    }
                }
                System.Console.WriteLine("{0} : {1}", x, len);
            }
        }

        private void SolveSudoku(char[,] board)
        {
            Dictionary<uint, HashSet<char>> units = new Dictionary<uint, HashSet<char>>();
            for (uint x = 0; x < board.GetLength(0); x++)
            {
                for (uint y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y] == '.')
                    {
                        HashSet<char> charSet = new HashSet<char>() { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                        units.Add(x + y * 9, charSet);
                    }
                }
            }
            // while (units.Count > 0)
            {
                for (uint x = 0; x < board.GetLength(0); x++)
                {
                    for (uint y = 0; y < board.GetLength(1); y++)
                    {
                        var c = board[x, y];
                        if (c == '.')
                        {
                            var charSet = GetAvailableInt(board, x, y, units);
                            if (charSet.Count == 1)
                            {
                                board[x, y] = charSet.First();
                                units.Remove(x + y * 9);
                                RemoveAvailableInt(board, x, y, units);
                            }
                        }
                    }
                }
            }

            foreach (var pair in units)
            {
                if (pair.Value.Count == 2)
                {
                    uint x = pair.Key % 9;
                    uint y = pair.Key / 9;
                    Console.WriteLine("index:{0}, x:{1}, y:{2}, value:{3}   ", pair.Key, x, y, string.Join(",", pair.Value));
                }
            }
        }

        private void RemoveAvailableInt(char[,] board, uint x, uint y, Dictionary<uint, HashSet<char>> units)
        {
            for (uint i = 0; i < 9; i++)
            {
                uint index = x + i * 9;
                if (i != y && units.ContainsKey(index))
                {
                    var charSet = units[index];
                    charSet.Remove(board[x, y]);
                    if (charSet.Count == 1)
                    {
                        board[x, i] = charSet.First();
                        units.Remove(index);
                        RemoveAvailableInt(board, x, i, units);
                    }
                }
            }
            for (uint i = 0; i < 9; i++)
            {
                uint index = i + y * 9;
                if (i != x && units.ContainsKey(index))
                {
                    var charSet = units[index];
                    charSet.Remove(board[x, y]);
                    if (charSet.Count == 1)
                    {
                        board[i, y] = charSet.First();
                        units.Remove(index);
                        RemoveAvailableInt(board, i, y, units);
                    }
                }
            }
            for (uint k = x / 3 * 3; k < x / 3 * 3 + 3; k++)
            {
                for (uint m = y / 3 * 3; m < y / 3 * 3 + 3; m++)
                {
                    uint index = k + m * 9;
                    if (k != x && m != y && units.ContainsKey(index))
                    {
                        var charSet = units[index];
                        charSet.Remove(board[x, y]);
                        if (charSet.Count == 1)
                        {
                            board[k, m] = charSet.First();
                            units.Remove(index);
                            RemoveAvailableInt(board, k, m, units);
                        }
                    }
                }
            }
        }

        private HashSet<char> GetAvailableInt(char[,] board, uint x, uint y, Dictionary<uint, HashSet<char>> units)
        {
            var charSet = units[x + y * 9];
            for (int k = 0; k < board.GetLength(1); k++)
            {
                var c = board[x, k];
                if (c != '.')
                {
                    charSet.Remove(c);
                }
            }
            for (int k = 0; k < board.GetLength(0); k++)
            {
                var c = board[k, y];
                if (c != '.')
                {
                    charSet.Remove(c);
                }
            }
            for (uint k = x / 3 * 3; k < x / 3 * 3 + 3; k++)
            {
                for (uint m = y / 3 * 3; m < y / 3 * 3 + 3; m++)
                {
                    var c = board[k, m];
                    if (c != '.')
                    {
                        charSet.Remove(c);
                    }
                }
            }
            return charSet;
        }
    }

    public class Sudoku
    {
        public void Solve(char[,] board)
        {
            Dictionary<int, RowHeader> headers = new Dictionary<int, RowHeader>();
            int[,] matrix = ConvertToMatrix(board, headers);
            DanceLink danceLink = new DanceLink();
            var rows = danceLink.Dance(matrix);
            FillCells(board, rows, headers);
        }

        public int[,] ConvertToMatrix(char[,] board, Dictionary<int, RowHeader> headers)
        {
            var len = board.GetLength(0);
            int[,] matrix = new int[len * len * len, 4 * len * len];
            for (int row = 0; row < len; row++)
            {
                for (int col = 0; col < len; col++)
                {
                    if (board[row, col] == '.')
                    {
                        for (int n = 1; n <= len; n++)
                        {
                            var positions = GetPosition(row + 1, col + 1, n, len);
                            int r = positions.Key;
                            foreach (var c in positions.Value)
                            {
                                matrix[r, c] = 1;
                            }
                            headers[r] = new RowHeader(row, col, n);
                        }
                    }
                    else
                    {
                        int n = int.Parse(board[row, col].ToString());
                        var positions = GetPosition(row + 1, col + 1, n, len);
                        int r = positions.Key;
                        foreach (var c in positions.Value)
                        {
                            matrix[r, c] = 1;
                        }
                    }
                }
            }
            return matrix;
        }

        private KeyValuePair<int, List<int>> GetPosition(int row, int col, int n, int len)
        {
            int r = (row - 1) * len * len + (col - 1) * len + n;
            List<int> cols = new List<int>();
            //cells
            int c = (row - 1) * len + col;
            cols.Add(c - 1);
            //rows
            c = len * len + (row - 1) * len + n;
            cols.Add(c - 1);
            //columns
            c = 2 * len * len + (col - 1) * len + n;
            cols.Add(c - 1);
            //regions
            int sqrt = (int)Math.Sqrt(len);
            c = 3 * len * len + ((col - 1) / sqrt) * len + ((row - 1) / sqrt) * sqrt * len + n;
            cols.Add(c - 1);
            var positions = new KeyValuePair<int, List<int>>(r - 1, cols);
            return positions;
        }

        private void FillCells(char[,] board, List<int> rows, Dictionary<int, RowHeader> headers)
        {
            foreach (var row in rows)
            {
                if (headers.ContainsKey(row))
                {
                    var rowHeader = headers[row];
                    board[rowHeader.Row, rowHeader.Col] = rowHeader.N.ToString()[0];
                }
            }
        }
    }

    public class RowHeader
    {
        public RowHeader(int row, int col, int n)
        {
            Row = row;
            Col = col;
            N = n;
        }

        public int Row { get; set; }

        public int Col { get; set; }

        public int N { get; set; }
    }
}