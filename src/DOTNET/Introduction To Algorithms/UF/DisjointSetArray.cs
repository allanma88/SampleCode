using System;
using System.Collections.Generic;

namespace UF
{
    public class DisjointSetArray
    {
        private int[] _disjointSet;

        public DisjointSetArray(int n)
        {
            _disjointSet = new int[n];
        }

        public void Run(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                MakeSet(i);
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        Union(i, j);
                    }
                }
            }

            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
            for (int k = 0; k < n; k++)
            {
                int i = Find(k);
                if (!map.ContainsKey(i))
                {
                    map[i] = new List<int>();
                }
                map[i].Add(k);
            }

            foreach (var key in map.Keys)
            {
                string str = string.Join(",", map[key]);
                Console.WriteLine($"{key}: {str}");
            }
        }

        public void MakeSet(int i)
        {
            _disjointSet[i] = i;
        }

        public int Find(int i)
        {
            while (i != _disjointSet[i])
            {
                i = _disjointSet[i];
            }
            return i;
        }

        public void Union(int i, int j)
        {
            int iRoot = Find(i);
            int jRoot = Find(j);
            if (iRoot != jRoot)
            {
                _disjointSet[iRoot] = _disjointSet[jRoot];
            }
        }
    }
}