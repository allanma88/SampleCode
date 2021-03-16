using System;
using System.Collections.Generic;

namespace StringResearch
{
    public class MyTrieST<TValue> : StringST<TValue>
    {
        private Node root = new Node();

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
            if (level == key.Length)
            {
                return n;
            }
            var c = key[level];
            if (n.Next.ContainsKey(c))
            {
                return Get(n.Next[c], key, level + 1);
            }
            return null;
        }

        public override IEnumerable<string> keysThatMatch(string pat)
        {
            Queue<string> queue = new Queue<string>();
            Collect(root, "", pat, 0, queue);
            return queue;
        }

        private void Collect(Node n, string pre, string pat, int level, Queue<string> queue)
        {
            if (level == pat.Length)
            {
                if (!n.Value.Equals(default(TValue)))
                {
                    queue.Enqueue(pre);
                }
                return;
            }
            var c = pat[level];
            foreach (var pair in n.Next)
            {
                if (pair.Key == c || c == '.')
                {
                    Collect(pair.Value, pre + pair.Key, pat, level + 1, queue);
                }
            }
        }

        public override IEnumerable<string> keysWithPrefix(string pre)
        {
            Node n = Get(root, pre, 0);
            Queue<string> queue = new Queue<string>();
            Collect(n, pre, queue);
            return queue;
        }

        private void Collect(Node n, string pre, Queue<string> queue)
        {
            if (!n.Value.Equals(default(TValue)))
            {
                queue.Enqueue(pre);
            }
            foreach (var pair in n.Next)
            {
                Collect(pair.Value, pre + pair.Key, queue);
            }
        }

        public override string LongestPrefixOf(string s)
        {
            var length = Search(root, s, 0, 0);
            return s.Substring(0, length);
        }

        private int Search(Node n, string s, int level, int length)
        {
            if (level == s.Length)
            {
                return length;
            }
            if (!n.Value.Equals(default(TValue)))
            {
                length = level;
            }
            var c = s[level];
            if (n.Next.ContainsKey(c))
            {
                return Search(n.Next[c], s, level + 1, length);
            }
            return length;
        }

        public override void Put(string key, TValue value)
        {
            Put(key, value, 0, root);
            N++;
        }

        private void Put(string key, TValue value, int level, Node n)
        {
            if (level == key.Length)
            {
                n.Value = value;
            }
            else
            {
                if (!n.Next.ContainsKey(key[level]))
                {
                    n.Next[key[level]] = new Node();
                }
                Put(key, value, level + 1, n.Next[key[level]]);
            }
        }

        public override int Size()
        {
            return N;
        }

        public override IEnumerable<string> Keys()
        {
            return keysWithPrefix("");
        }

        public override void Delete(string key)
        {
            root = Delete(root, key, 0);
        }

        private Node Delete(Node n, string key, int level)
        {
            if (n == null)
            {
                return null;
            }
            if (level == key.Length)
            {
                n.Value = default(TValue);
                return null;
            }
            var c = key[level];
            if (n.Next.ContainsKey(c))
            {
                var x = Delete(n.Next[c], key, level + 1);
                if (x == null)
                {
                    n.Next.Remove(c);
                }
                else
                {
                    n.Next[c] = x;
                }
            }
            if (n.Next == null || n.Next.Count == 0)
            {
                return null;
            }
            else
            {
                return n;
            }
        }

        private class Node
        {
            public TValue Value { get; set; }

            public Dictionary<char, Node> Next { get; set; } = new Dictionary<char, Node>();
        }
    }
}