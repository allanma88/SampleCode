using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace BinaryTree
{
    public class ConstructSample
    {
        public void Run()
        {
            int[] inValues = new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 16 };
            int[] preValues = new int[] { 10, 6, 4, 3, 5, 8, 7, 9, 14, 12, 11, 13, 16 };
            int[] postValues = new int[] { 3, 5, 4, 7, 9, 8, 6, 11, 13, 12, 16, 14, 10 };
            // preValues = new int[] { 2, 1, 3 };
            // inValues = new int[] { 1, 2, 3 };
            // postValues = new int[] { 3, 1, 2 };

            Construct construct = new Construct();
            LoopTraverse traverse = new LoopTraverse();
            // var root = construct.PreAndIn(preValues, inValues);
            // Match(preValues, root, traverse.Pre, "preandin");

            var root = construct.PreAndInWithRecur(preValues, inValues);
            Match(preValues, root, traverse.Pre, "PreAndInWithRecur");

            // root = construct.InAndPost(inValues, postValues);
            // Match(postValues, root, traverse.Post, "InAndPost");

            // var root = construct.InAndPostWithRecur(inValues, postValues);
            // Match(postValues, root, traverse.Post, "InAndPostWithRecur");

            // preValues = new int[] { 10, 6, 4, 3, 5, 8, 7, 9, 14, 12, 11, 13, 16 };
            // root = construct.Pre(preValues);
            // Match(preValues, root, traverse.Pre, "bst pre");

            // root = construct.Post(postValues);
            // Match(postValues, root, traverse.Post, "bst post");
        }

        private void Match(int[] values, Node root, Func<Node, IEnumerable<Node>> func, string order)
        {
            bool match = true;
            var nodes = func(root).ToArray();
            for (int i = 0; i < nodes.Length; i++)
            {
                var node = nodes[i];
                if (node.V != values[i])
                {
                    match = false;
                    break;
                }
            }
            if (match)
            {
                Console.WriteLine("{0} match", order);
            }
            else
            {
                Console.WriteLine("{0} not match", order);
            }
        }
    }

    public class Construct
    {
        public Node PreAndIn(int[] preValues, int[] inValues)
        {
            if (preValues.Length < 1)
            {
                return null;
            }
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < inValues.Length; i++)
            {
                dic[inValues[i]] = i;
            }
            Node root = new Node(preValues[0]);
            for (int i = 1; i < preValues.Length; i++)
            {
                int v = preValues[i];
                Node n = root;
                while (true)
                {
                    if (dic[v] <= dic[n.V])
                    {
                        if (n.Right != null)
                        {
                            throw new InvalidOperationException("invalid pre order");
                        }
                        else if (n.Left != null)
                        {
                            n = n.Left;
                        }
                        else
                        {
                            n.Left = new Node(v);
                            break;
                        }
                    }
                    else
                    {
                        if (n.Right != null)
                        {
                            n = n.Right;
                        }
                        else
                        {
                            n.Right = new Node(v);
                            break;
                        }
                    }
                }
            }
            return root;
        }

        public Node PreAndInWithRecur(int[] preValues, int[] inValues)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < inValues.Length; i++)
            {
                dic.Add(inValues[i], i);
            }
            return PreAndInWithRecur(0, preValues.Length - 1, 0, inValues.Length - 1, preValues, inValues, dic);
        }

        private Node PreAndInWithRecur(int preStart, int preEnd, int inStart, int inEnd, int[] preValues, int[] inValues, Dictionary<int, int> dic)
        {
            if (preStart > preEnd)
            {
                return null;
            }
            if (preStart == preEnd)
            {
                return new Node(preValues[preStart]);
            }
            Node root = new Node(preValues[preStart]);
            int index = dic[preValues[preStart]];
            int preLeftStart = preStart + 1;
            int preLeftEnd = preStart + index - inStart;
            int preRightStart = preLeftEnd + 1;
            int preRightEnd = preEnd;
            int inLeftStart = inStart;
            int inLeftEnd = index - 1;
            int inRightStart = index + 1;
            int inRightEnd = inEnd;

            ValidateOrder(index, preLeftStart, preLeftEnd, preRightStart, preRightEnd, preValues, dic, "pre");

            root.Left = PreAndInWithRecur(preLeftStart, preLeftEnd, inLeftStart, inLeftEnd, preValues, inValues, dic);
            root.Right = PreAndInWithRecur(preRightStart, preRightEnd, inRightStart, inRightEnd, preValues, inValues, dic);
            return root;
        }

        private void ValidateOrder(int index, int leftStart, int leftEnd, int rightStart, int rightEnd, int[] values, Dictionary<int, int> dic, string order)
        {
            for (int i = leftStart; i <= leftEnd; i++)
            {
                int v = values[i];
                if (dic[v] >= index)
                {
                    throw new InvalidOperationException(string.Format("invalid {0} order", order));
                }
            }
            for (int i = rightStart; i <= rightEnd; i++)
            {
                int v = values[i];
                if (dic[v] <= index)
                {
                    throw new InvalidOperationException(string.Format("invalid {0} order", order));
                }
            }
        }

        public Node InAndPost(int[] inValues, int[] postValues)
        {
            if (postValues.Length < 1)
            {
                return null;
            }
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < inValues.Length; i++)
            {
                dic[inValues[i]] = i;
            }
            Node root = new Node(postValues[postValues.Length - 1]);
            for (int i = postValues.Length - 2; i >= 0; i--)
            {
                int v = postValues[i];
                Node n = root;
                while (true)
                {
                    if (dic[v] <= dic[n.V])
                    {
                        if (n.Left != null)
                        {
                            n = n.Left;
                        }
                        else
                        {
                            n.Left = new Node(v);
                            break;
                        }
                    }
                    else
                    {
                        if (n.Left != null)
                        {
                            throw new InvalidOperationException("invalid post order");
                        }
                        else if (n.Right != null)
                        {
                            n = n.Right;
                        }
                        else
                        {
                            n.Right = new Node(v);
                            break;
                        }
                    }
                }
            }
            return root;
        }

        public Node InAndPostWithRecur(int[] inValues, int[] postValues)
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < inValues.Length; i++)
            {
                dic[inValues[i]] = i;
            }
            return InAndPostWithRecur(0, postValues.Length - 1, 0, inValues.Length - 1, inValues, postValues, dic);
        }

        private Node InAndPostWithRecur(int postStart, int postEnd, int inStart, int inEnd, int[] inValues, int[] postValues, Dictionary<int, int> dic)
        {
            if (postStart > postEnd)
            {
                return null;
            }
            if (postStart == postEnd)
            {
                return new Node(postValues[postEnd]);
            }
            int v = postValues[postEnd];
            Node node = new Node(v);
            int index = dic[v];
            int postRightStart = postEnd - (inEnd - index);
            int postRightEnd = postEnd - 1;
            int postLeftStart = postStart;
            int postLeftEnd = postRightStart - 1;
            int inLeftStart = inStart;
            int inLeftEnd = index - 1;
            int inRightStart = index + 1;
            int inRightEnd = inEnd;

            ValidateOrder(index, postLeftStart, postLeftEnd, postRightStart, postRightEnd, postValues, dic, "post");

            node.Left = InAndPostWithRecur(postLeftStart, postLeftEnd, inLeftStart, inLeftEnd, inValues, postValues, dic);
            node.Right = InAndPostWithRecur(postRightStart, postRightEnd, inRightStart, inRightEnd, inValues, postValues, dic);
            return node;
        }

        public Node Pre(int[] preValues)
        {
            Node root = new Node(preValues[0]);
            for (int i = 1; i < preValues.Length; i++)
            {
                int v = preValues[i];
                Node n = root;
                while (true)
                {
                    if (v <= n.V)
                    {
                        if (n.Right != null)
                        {
                            throw new InvalidOperationException("invalid pre order");
                        }
                        else if (n.Left != null)
                        {
                            n = n.Left;
                        }
                        else
                        {
                            n.Left = new Node(v);
                            break;
                        }
                    }
                    else
                    {
                        if (n.Right != null)
                        {
                            n = n.Right;
                        }
                        else
                        {
                            n.Right = new Node(v);
                            break;
                        }
                    }
                }
            }
            return root;
        }

        public Node Post(int[] postValues)
        {
            Node root = new Node(postValues[postValues.Length - 1]);
            for (int i = postValues.Length - 2; i >= 0; i--)
            {
                int v = postValues[i];
                Node n = root;
                while (true)
                {
                    if (v <= n.V)
                    {
                        if (n.Left != null)
                        {
                            n = n.Left;
                        }
                        else
                        {
                            n.Left = new Node(v);
                            break;
                        }
                    }
                    else
                    {
                        if (n.Left != null)
                        {
                            throw new InvalidOperationException("invalid post order");
                        }
                        else if (n.Right != null)
                        {
                            n = n.Right;
                        }
                        else
                        {
                            n.Right = new Node(v);
                            break;
                        }
                    }
                }
            }
            return root;
        }
    }
}