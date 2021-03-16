using System;
using System.Collections.Generic;

namespace SearchResearch
{
    public class SequentialSearchST<TKey, TValue> : STBase<TKey, TValue> where TKey : IComparable<TKey>
    {
        private Node _first;

        public override void Put(TKey key, TValue value)
        {
            Node node = _first;
            while (node != null)
            {
                if (node.Key.CompareTo(key) == 0)
                {
                    node.Value = value;
                    return;
                }
                else
                {
                    node = node.Next;
                }
            }
            _first = new Node(key, value, _first);
        }

        public override TValue Get(TKey key)
        {
            Node node = _first;
            while (node != null)
            {
                if (node.Key.CompareTo(key) == 0)
                {
                    return node.Value;
                }
                else
                {
                    node = node.Next;
                }
            }
            return default(TValue);
        }

        public override int Size()
        {
            int count = 0;
            Node node = _first;
            while (node != null)
            {
                count++;
                node = node.Next;
            }
            return count;
        }

        public override IEnumerable<TKey> Keys()
        {
            List<TKey> keys = new List<TKey>();
            Node node = _first;
            while (node != null)
            {
                keys.Add(node.Key);
                node = node.Next;
            }
            return keys;
        }

        public override void Delete(TKey key)
        {
            if (_first == null)
            {
                return;
            }
            Node prev = _first;
            Node node = _first.Next;
            while (node != null)
            {
                if (node.Key.CompareTo(key) == 0)
                {
                    prev.Next = node.Next;
                }
                else
                {
                    prev = node;
                    node = node.Next;
                }
            }
        }

        private class Node
        {
            public Node(TKey key, TValue value, Node next)
            {
                Key = key;
                Value = value;
                Next = next;
            }

            public TKey Key { get; set; }

            public TValue Value { get; set; }

            public Node Next { get; set; }
        }
    }
}