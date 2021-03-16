using System.Collections.Generic;

namespace StringResearch
{
    public class TrieST<TValue> : StringST<TValue>
    {
        private static int R = 26;
        private Node root = new Node();
        private int N;

        public override TValue Get(string key)
        {
            Node n = Get(key, 0, root);
            if (n == null)
            {
                return default(TValue);
            }
            else
            {
                return n.Value;
            }
        }

        private Node Get(string key, int level, Node n)
        {
            if (level == key.Length)
            {
                return n;
            }
            if (n == null)
            {
                return null;
            }
            int index = key[level] - 'a';
            return Get(key, level + 1, n.Next[index]);
        }

        public override IEnumerable<string> Keys()
        {
            return keysWithPrefix("");
        }

        public override IEnumerable<string> keysThatMatch(string pat)
        {
            Queue<string> queue = new Queue<string>();
            Collect(root, "", pat, queue);
            return queue;
        }

        private void Collect(Node n, string pre, string pat, Queue<string> queue)
        {
            if (n == null)
            {
                return;
            }
            if (pre.Length == pat.Length && !n.Value.Equals(default(TValue)))
            {
                queue.Enqueue(pre);
            }
            if (pre.Length == pat.Length)
            {
                return;
            }
            char next = pat[pre.Length];
            for (int i = 0; i < R; i++)
            {
                char c = (char)(i + 'a');
                if (next == c || next == '.')
                {
                    Collect(n.Next[i], pre + c, pat, queue);
                }
            }
        }

        public override IEnumerable<string> keysWithPrefix(string pre)
        {
            Queue<string> queue = new Queue<string>();
            Collect(pre, Get(pre, 0, root), queue);
            return queue;
        }

        private void Collect(string pre, Node n, Queue<string> queue)
        {
            if (n == null)
            {
                return;
            }
            if (!n.Value.Equals(default(TValue)))
            {
                queue.Enqueue(pre);
            }
            for (int i = 0; i < R; i++)
            {
                char c = (char)(i + 'a');
                Collect(pre + c, n.Next[i], queue);
            }
        }

        public override string LongestPrefixOf(string s)
        {
            int length = Search(root, s, 0, 0);
            return s.Substring(0, length);
        }

        private int Search(Node n, string s, int level, int length)
        {
            if (n == null)
            {
                return length;
            }
            if (!n.Value.Equals(default(TValue)))
            {
                length = level;
            }
            if (level == s.Length)
            {
                return length;
            }
            var index = s[level] - 'a';
            return Search(n.Next[index], s, level + 1, length);
        }

        public override void Put(string key, TValue value)
        {
            root = Put(key, value, 0, root);
            N++;
        }

        private Node Put(string key, TValue value, int level, Node n)
        {
            if (n == null)
            {
                n = new Node();
            }
            if (level == key.Length)
            {
                n.Value = value;
                return n;
            }
            int index = key[level] - 'a';
            n.Next[index] = Put(key, value, level + 1, n.Next[index]);
            return n;
        }

        public override int Size()
        {
            return N;
        }

        public override void Delete(string key)
        {
            root = Delete(root, key, 0);
        }

        private Node Delete(Node x, string key, int level)
        {
            if (x == null)
            {
                return null;
            }
            if (level == key.Length)
            {
                x.Value = default(TValue);
            }
            else
            {
                int index = key[level] - 'a';
                x.Next[index] = Delete(x.Next[index], key, level + 1);
            }
            for (int i = 0; i < R; i++)
            {
                if (x.Next[i] != null)
                {
                    return x;
                }
            }
            return null;
        }

        private class Node
        {
            public TValue Value { get; set; }

            public Node[] Next { get; set; } = new Node[R];
        }
    }
}