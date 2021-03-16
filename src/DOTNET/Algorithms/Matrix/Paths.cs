using System;
using System.Collections.Generic;

namespace Matrix
{
    public class PathsSample
    {
        public void Run()
        {
            var matrix = GetMatrix(4, 3);
            Console.WriteLine("Matrix: ");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j] + " ");
                }
                Console.WriteLine();
            }
            Paths paths = new Paths();
            var allPaths = paths.GetAllPaths(matrix);
            Console.WriteLine("All paths: ");
            foreach (var path in allPaths)
            {
                Console.WriteLine(string.Join(",", path));
            }
        }

        private int[,] GetMatrix(int m, int n)
        {
            int[,] matrix = new int[m, n];
            int k = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = k++;
                }
            }
            return matrix;
        }
    }

    public class Paths
    {
        public List<List<int>> GetAllPaths(int[,] matrix)
        {
            List<List<int>> paths = new List<List<int>>();
            GetAllPaths(matrix, 0, 0, matrix.GetLength(0) - 1, matrix.GetLength(1) - 1, new List<int>(), paths);
            return paths;
        }

        private List<List<int>> GetAllPaths(int[,] matrix, int i, int j, int m, int n, List<int> path, List<List<int>> paths)
        {
            List<int> newPath = new List<int>(path) { matrix[i, j] };
            if (i == m && j == n)
            {
                paths.Add(newPath);
                return paths;
            }
            if (i < m)
            {
                GetAllPaths(matrix, i + 1, j, m, n, newPath, paths);
            }
            if (j < n)
            {
                GetAllPaths(matrix, i, j + 1, m, n, newPath, paths);
            }
            return paths;
        }
    }
}