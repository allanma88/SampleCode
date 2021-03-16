using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace BinaryTree
{
    public class SameTreeSample
    {
        public void Run()
        {
            SameTree sameTree = new SameTree();

            Node p = new Node(1);
            Node q = new Node(1);
            Console.WriteLine("IsSameTreeWithPre: {0}", sameTree.IsSameTreeWithPre(p, q));
            Console.WriteLine("IsSameTreeWithPreSimple: {0}", sameTree.IsSameTreeWithPreSimple(p, q));

            p = new Node(1);
            q = new Node(2);
            Console.WriteLine("IsSameTreeWithPre: {0}", sameTree.IsSameTreeWithPre(p, q));
            Console.WriteLine("IsSameTreeWithPreSimple: {0}", sameTree.IsSameTreeWithPreSimple(p, q));

            p = new Node(1);
            q = null;
            Console.WriteLine("IsSameTreeWithPre: {0}", sameTree.IsSameTreeWithPre(p, q));
            Console.WriteLine("IsSameTreeWithPreSimple: {0}", sameTree.IsSameTreeWithPreSimple(p, q));
        }
    }

    public class SameTree
    {
        public bool IsSameTreeWithPre(Node p, Node q)
        {
            Stack<Node> pStack = new Stack<Node>();
            Node pn = p;
            Stack<Node> qStack = new Stack<Node>();
            Node qn = q;
            while ((pStack.Count > 0 || pn != null) && (qStack.Count > 0 || qn != null))
            {
                while (pn != null && qn != null)
                {
                    if (pn.V != qn.V)
                    {
                        return false;
                    }
                    pStack.Push(pn.Right);
                    qStack.Push(qn.Right);
                    pn = pn.Left;
                    qn = qn.Left;
                }
                if (pn == null && qn != null)
                {
                    return false;
                }
                if (pn != null && qn == null)
                {
                    return false;
                }
                if (pStack.Count > 0)
                {
                    pn = pStack.Pop();
                }
                if (qStack.Count > 0)
                {
                    qn = qStack.Pop();
                }
            }
            if ((pStack.Count > 0 || pn != null) && (qStack.Count == 0 && qn == null))
            {
                return false;
            }
            if ((pStack.Count == 0 && pn == null) && (qStack.Count > 0 || qn != null))
            {
                return false;
            }
            return true;
        }

        public bool IsSameTreeWithPreSimple(Node p, Node q)
        {
            Stack<Node> pStack = new Stack<Node>();
            if (p != null) pStack.Push(p);
            Stack<Node> qStack = new Stack<Node>();
            if (q != null) qStack.Push(q);
            while (pStack.Count > 0 && qStack.Count > 0)
            {
                Node pn = pStack.Pop();
                Node qn = qStack.Pop();
                if (pn.V != qn.V)
                {
                    return false;
                }
                if (pn.Right != null && qn.Right != null)
                {
                    pStack.Push(pn.Right);
                    qStack.Push(qn.Right);
                }
                else if (pn.Right == null && qn.Right == null)
                {
                    //do nothing
                }
                else
                {
                    return false;
                }
                if (pn.Left != null && qn.Left != null)
                {
                    pStack.Push(pn.Left);
                    qStack.Push(qn.Left);
                }
                else if (pn.Left == null && qn.Left == null)
                {
                    //do nothing
                }
                else
                {
                    return false;
                }
            }
            if (pStack.Count != qStack.Count)
            {
                return false;
            }
            return true;
        }
    }
}