using System;

namespace BinarySearchTree
{
    public class Tree
    {
        public void Run()
        {
            int[] nums = new int[] { 8, 4, 12, 2, 6, 10, 14 };
            TreeNode root = BuildTree(nums);
            TreeNode node = Search1(root, 4);
            Console.WriteLine(node != null ? node.Val.ToString() : "empty");
            node = Search1(root, 5);
            Console.WriteLine(node != null ? node.Val.ToString() : "empty");

            TreeNode minimum = Minimum(root);
            Console.WriteLine($"minimum: {minimum.Val}");
            TreeNode maximum = Maximum(root);
            Console.WriteLine($"maximum: {maximum.Val}");
            TreeNode node1 = Search1(root, 8);
            TreeNode successor = Successor(node1);
            Console.WriteLine($"{node1.Val} Successor: {successor.Val}");
            TreeNode node2 = Search1(root, 12);
            TreeNode predecessor = Predecessor(node2);
            Console.WriteLine($"{node2.Val} Predecessor: {predecessor.Val}");
        }

        public TreeNode Search1(TreeNode root, int val)
        {
            TreeNode n = root;
            while (n != null)
            {
                if (n.Val == val)
                {
                    return n;
                }
                else if (val < n.Val)
                {
                    n = n.Left;
                }
                else
                {
                    n = n.Right;
                }
            }
            return n;
        }

        public TreeNode Search(TreeNode root, int val)
        {
            if (root == null)
            {
                return null;
            }
            if (root.Val == val)
            {
                return root;
            }
            else if (val < root.Val)
            {
                return Search(root.Left, val);
            }
            else
            {
                return Search(root.Right, val);
            }
        }

        public void Insert(TreeNode root, TreeNode node)
        {
            TreeNode cur = root;
            while (true)
            {
                if (node.Val <= cur.Val)
                {
                    if (cur.Left != null)
                    {
                        cur = cur.Left;
                    }
                    else
                    {
                        cur.Left = node;
                        return;
                    }
                }
                else
                {
                    if (cur.Right != null)
                    {
                        cur = cur.Right;
                    }
                    else
                    {
                        cur.Right = node;
                        return;
                    }
                }
            }
        }

        public TreeNode BuildTree(int[] nums)
        {
            TreeNode root = new TreeNode { Val = nums[0] };
            for (int i = 1; i < nums.Length; i++)
            {
                TreeNode node = new TreeNode { Val = nums[i] };
                Insert(root, node);
            }
            return root;
        }

        public TreeNode Minimum(TreeNode root)
        {
            TreeNode minimum = root;
            while (minimum.Left != null)
            {
                minimum = minimum.Left;
            }
            return minimum;
        }

        public TreeNode Maximum(TreeNode root)
        {
            TreeNode maximum = root;
            while (maximum.Right != null)
            {
                maximum = maximum.Right;
            }
            return maximum;
        }

        public TreeNode Successor(TreeNode node)
        {
            if (node.Right != null)
            {
                return Minimum(node.Right);
            }
            TreeNode cur = node;
            while (cur != null)
            {
                if (cur.Parent.Left == cur)
                {
                    return cur.Parent;
                }
                cur = cur.Parent;
            }
            return null;
        }

        public TreeNode Predecessor(TreeNode node)
        {
            if (node.Left != null)
            {
                return Maximum(node.Left);
            }
            TreeNode cur = node;
            while (cur != null)
            {
                if (cur.Parent.Right == cur)
                {
                    return cur.Parent;
                }
                cur = cur.Parent;
            }
            return null;
        }

        public void Delete(TreeNode root, TreeNode node)
        {

        }
    }
}