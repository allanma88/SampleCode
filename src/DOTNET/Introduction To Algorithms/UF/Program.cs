using System;

namespace UF
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 3;
            int[,] matrix = GetMatrix(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            
            Console.WriteLine("DisjointSet: ");
            DisjointSet disjointSet = new DisjointSet();
            disjointSet.Run(matrix);

            Console.WriteLine("OptDisjointSet: ");
            OptDisjointSet optDisjointSet = new OptDisjointSet();
            optDisjointSet.Run(matrix);

            Console.WriteLine("DisjointSetForest: ");
            DisjointSetForest disjointSetForest = new DisjointSetForest();
            disjointSetForest.Run(matrix);

            Console.WriteLine("DisjointSetArray: ");
            DisjointSetArray disjointSetArray = new DisjointSetArray(n);
            disjointSetArray.Run(matrix);

            // OfflineMinimum offlineMinimum = new OfflineMinimum();
            // offlineMinimum.Run();
        }


        private static int[,] GetMatrix(int n)
        {
            Random random = new Random();
            int[,] matrix = new int[n, n];
            for (int i = 0; i < 5; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    int k = random.Next(0, 10) % 2;
                    matrix[i, j] = k;
                    matrix[j, i] = k;
                }
            }

            // matrix = new int[,]
            // {
            //     {0, 1, 0, 0, 0},
            //     {1, 0, 0, 1, 0},
            //     {0, 0, 0, 1, 0},
            //     {0, 1, 1, 0, 0},
            //     {0, 0, 0, 0, 0}
            // };
            // matrix = new int[,]
            // {
            //     {1, 1, 1},
            //     {1, 1, 1},
            //     {1, 1, 1}
            // };
            return matrix;
        }
    }
}