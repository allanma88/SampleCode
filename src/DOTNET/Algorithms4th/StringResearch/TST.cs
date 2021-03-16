using SearchResearch;
using System.Collections.Generic;

namespace StringResearch
{
    public class TST<TValue> : StringST<TValue>
    {
        private Node root;
        private int N;

        public override TValue Get(string key)
        {
            Node n = Get(root, key, 0);
            if (n != null)
            {
                return n.Value;
            }
            else
            {
                return default(TValue);
            }
        }

        private Node Get(Node n, string key, int level)
        {
            if (n == null)
            {
                return null;
            }
            char c = key[level];
            if (c < n.C)
            {
                return Get(n.Left, key, level);
            }
            else if (c > n.C)
            {
                return Get(n.Right, key, level);
            }
            else if (level == key.Length - 1)
            {
                return n;
            }
            else
            {
                return Get(n.Mid, key, level + 1);
            }
        }

        public override IEnumerable<string> Keys()
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<string> keysThatMatch(string s)
        {
            throw new System.NotImplementedException();
        }

        public override IEnumerable<string> keysWithPrefix(string s)
        {
            throw new System.NotImplementedException();
        }

        public override string LongestPrefixOf(string s)
        {
            throw new System.NotImplementedException();
        }

        public override void Put(string key, TValue value)
        {
            root = Put(root, key, value, 0);
        }

        private Node Put(Node n, string key, TValue value, int level)
        {
            var c = key[level];
            if (n == null)
            {
                n = new Node() { C = c };
            }

            if (c > n.C)
            {
                n.Right = Put(n.Right, key, value, level);
            }
            else if (c < n.C)
            {
                n.Left = Put(n.Left, key, value, level);
            }
            else if (level == key.Length - 1)
            {
                n.Value = value;
            }
            else
            {
                n.Mid = Put(n.Mid, key, value, level + 1);
            }

            return n;
        }

        public override int Size()
        {
            throw new System.NotImplementedException();
        }

        private class Node
        {
            public char C { get; set; }

            public Node Mid { get; set; }

            public Node Left { get; set; }

            public Node Right { get; set; }

            public TValue Value { get; set; }
        }
    }
}