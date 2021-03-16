using System;
using System.Collections.Generic;

namespace UF
{
    public class DisjointSetForest
    {
        public void Run(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            TreeNode[] sets = new TreeNode[n];
            for (int i = 0; i < n; i++)
            {
                TreeNode node = new TreeNode() { Val = i };
                MakeSet(node);
                sets[i] = node;
            }
            foreach (var set in sets)
            {
                int i = Find(set).Val;
                for (int j = 0; j < n; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        Union(set, sets[j]);
                    }
                }
            }
            HashSet<TreeNode> hashset = new HashSet<TreeNode>();
            foreach (var set in sets)
            {
                hashset.Add(Find(set));
            }
            foreach (var set in hashset)
            {
                List<int> vals = new List<int>();
                foreach (var node in sets)
                {
                    if (Find(node) == set)
                    {
                        vals.Add(node.Val);
                    }
                }
                Console.WriteLine($"{set.Val}: {string.Join(',', vals)}");
            }
        }

        public void Union(TreeNode x, TreeNode y)
        {
            TreeNode xSet = Find(x);
            TreeNode ySet = Find(y);
            if (xSet.Rank > ySet.Rank)
            {
                ySet.Parent = xSet;
            }
            else
            {
                xSet.Parent = ySet;
                if (xSet.Rank == ySet.Rank)
                {
                    ySet.Rank++;
                }
            }
        }

        public TreeNode Find(TreeNode x)
        {
            if (x.Parent != x)
            {
                x.Parent = Find(x.Parent);
            }
            return x.Parent;
        }

        //Loop
        public TreeNode Find1(TreeNode x)
        {
            TreeNode root = x;
            while (root.Parent != root)
            {
                root = root.Parent;
            }
            while (x.Parent != x)
            {
                x.Parent = root;
                x = x.Parent;
            }
            return root;
        }

        public TreeNode MakeSet(TreeNode x)
        {
            x.Parent = x;
            x.Rank = 0;
            return x;
        }
    }

    public class TreeNode
    {
        public int Val;

        public TreeNode Parent;

        public int Rank;
    }
}