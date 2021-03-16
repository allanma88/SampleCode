using System.Collections.Generic;

namespace DataStructure
{
    public class Tree
    {
        public List<int> PreOrder(TreeNode root)
        {
            List<int> vals = new List<int>();

            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                TreeNode n = stack.Pop();
                vals.Add(n.Val);
                if (n.Right != null)
                {
                    stack.Push(n.Right);
                }
                if (n.Left != null)
                {
                    stack.Push(n.Left);
                }
            }

            return vals;
        }

        public List<int> InOrder(TreeNode root)
        {
            List<int> vals = new List<int>();

            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode cur = root;

            while (stack.Count > 0 || cur != null)
            {
                while (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.Left;
                }
                cur = stack.Pop();
                vals.Add(cur.Val);
                cur = cur.Right;
            }

            return vals;
        }

        public List<int> PostOrder(TreeNode root)
        {
            List<int> vals = new List<int>();

            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode cur = root;
            TreeNode prev = root;
            while (stack.Count > 0 || cur != null)
            {
                while (cur != null)
                {
                    stack.Push(cur);
                    prev = cur;
                    cur = cur.Left;
                }
                cur = stack.Peek();
                if (cur.Right == null || cur.Right == prev)
                {
                    vals.Add(cur.Val);
                    stack.Pop();
                    prev = cur;
                    cur = null;
                }
                else
                {
                    prev = cur;
                    cur = cur.Right;
                }

            }

            return vals;
        }
    }

    public class TreeNode
    {
        public int Val;

        public TreeNode Left;

        public TreeNode Right;
    }
}