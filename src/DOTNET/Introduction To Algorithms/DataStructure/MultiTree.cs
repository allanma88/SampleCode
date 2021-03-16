using System.Collections.Generic;

namespace DataStructure
{
    public class MultiTree
    {
        public List<int> PreOrderLoop(MultiTreeNode root)
        {
            List<int> vals = new List<int>();

            Stack<MultiTreeNode> stack = new Stack<MultiTreeNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                MultiTreeNode n = stack.Pop();
                vals.Add(n.Val);

                List<MultiTreeNode> nodes = new List<MultiTreeNode>();
                MultiTreeNode child = n.LeftChild;
                while (child != null)
                {
                    nodes.Add(child);
                    child = child.RightSibling;
                }

                for (int i = nodes.Count - 1; i >= 0; i--)
                {
                    stack.Push(nodes[i]);
                }
            }

            return vals;
        }

        public List<int> PostOrderLoop(MultiTreeNode root)
        {
            List<int> vals = new List<int>();

            Stack<MultiTreeNode> stack = new Stack<MultiTreeNode>();
            MultiTreeNode cur = root;
            while (stack.Count > 0 || cur != null)
            {
                while (cur != null)
                {
                    stack.Push(cur);
                    cur = cur.LeftChild;
                }
                cur = stack.Pop();
                vals.Add(cur.Val);
                cur = cur.RightSibling;
            }

            return vals;
        }

        public List<int> PreOrder(MultiTreeNode root)
        {
            List<int> vals = new List<int>();
            PreTraverse(root, vals);
            return vals;
        }

        public List<int> PostOrder(MultiTreeNode root)
        {
            List<int> vals = new List<int>();
            PostTraverse(root, vals);
            return vals;
        }

        private void PreTraverse(MultiTreeNode n, List<int> vals)
        {
            vals.Add(n.Val);
            MultiTreeNode child = n.LeftChild;
            while (child != null)
            {
                PreTraverse(child, vals);
                child = child.RightSibling;
            }
        }

        private void PostTraverse(MultiTreeNode n, List<int> vals)
        {
            MultiTreeNode child = n.LeftChild;
            while (child != null)
            {
                PostTraverse(child, vals);
                child = child.RightSibling;
            }
            vals.Add(n.Val);
        }
    }

    public class MultiTreeNode
    {
        public int Val;

        public MultiTreeNode LeftChild;

        public MultiTreeNode RightSibling;
    }
}