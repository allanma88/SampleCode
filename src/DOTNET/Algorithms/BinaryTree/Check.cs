using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace BinaryTree
{
    public class CheckSample
    {
        public void Run()
        {
            Check check = new Check();

            int[] preValues = new int[] { 10, 6, 4, 3, 5, 8, 7, 9, 14, 12, 11, 13, 16 };
            var result = check.Pre(preValues);
            Console.WriteLine(result);
            preValues = new int[] { 10, 6, 4, 3, 5, 12, 7, 9, 14, 8, 11, 13, 16 };
            result = check.Pre(preValues);
            Console.WriteLine(result);

            int[] postValues = new int[] { 3, 5, 4, 7, 9, 8, 6, 11, 13, 12, 16, 14, 10 };
            result = check.Post(postValues);
            Console.WriteLine(result);
            postValues = new int[] { 3, 5, 4, 7, 9, 8, 6, 11, 13, 12, 14, 16, 10 };
            result = check.Post(postValues);
            Console.WriteLine(result);
        }
    }

    public class Check
    {
        public bool Pre(int[] preValues)
        {
            Stack<int> stack = new Stack<int>();
            int root = int.MinValue;
            foreach (var v in preValues)
            {
                if (v < root)
                {
                    return false;
                }
                while (stack.Count > 0 && stack.Peek() < v)
                {
                    root = stack.Pop();
                }
                stack.Push(v);
            }
            return true;
        }

        public bool Post(int[] postValues)
        {
            Stack<int> stack = new Stack<int>();
            int root = int.MaxValue;
            for (int i = postValues.Length - 1; i >= 0; i--)
            {
                int v = postValues[i];
                if (v > root)
                {
                    return false;
                }
                while (stack.Count > 0 && stack.Peek() > v)
                {
                    root = stack.Pop();
                }
                stack.Push(v);
            }
            return true;
        }
    }
}