using System;
using System.Collections.Generic;

namespace SearchResearch
{
    public class BinarySearchTree_Loop<TKey, TValue> : OrderedSTBase<TKey, TValue> where TKey : IComparable
    {
        private Node<TKey, TValue> _root;

        private int _length;

        public override void Put(TKey key, TValue value)
        {
            if (_root == null)
            {
                _root = new Node<TKey, TValue>(key, value);
                _length++;
            }
            Node<TKey, TValue> current = _root;
            while (true)
            {
                if (key.CompareTo(current.Key) == 0)
                {
                    current.Value = value;
                    break;
                }
                else if (key.CompareTo(current.Key) > 0)
                {
                    if (current.Right == null)
                    {
                        current.Right = new Node<TKey, TValue>(key, value);
                        _length++;
                        break;
                    }
                    else
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left == null)
                    {
                        current.Left = new Node<TKey, TValue>(key, value);
                        _length++;
                        break;
                    }
                    else
                    {
                        current = current.Left;
                    }
                }
            }
        }

        public override TValue Get(TKey key)
        {
            Node<TKey, TValue> current = _root;
            while (current != null)
            {
                if (key.CompareTo(current.Key) == 0)
                {
                    return current.Value;
                }
                else if (key.CompareTo(current.Key) > 0)
                {
                    current = current.Right;
                }
                else
                {
                    current = current.Left;
                }
            }
            return default(TValue);
        }

        public override int Size()
        {
            return _length;
        }

        public override TKey Min()
        {
            if (_root == null)
            {
                throw new InvalidOperationException();
            }
            Node<TKey, TValue> current = _root;
            while (true)
            {
                if (current.Left == null)
                {
                    return current.Key;
                }
                else
                {
                    current = current.Left;
                }
            }
        }

        public override TKey Max()
        {
            if (_root == null)
            {
                throw new InvalidOperationException();
            }
            Node<TKey, TValue> current = _root;
            while (true)
            {
                if (current.Right == null)
                {
                    return current.Key;
                }
                else
                {
                    current = current.Right;
                }
            }
        }

        public override TKey Floor(TKey key)
        {
            throw new NotImplementedException();
        }

        public override TKey Ceiling(TKey key)
        {
            throw new NotImplementedException();
        }

        public override int Rank(TKey key)
        {
            throw new NotImplementedException();
        }

        public override TKey Select(int k)
        {
            throw new NotImplementedException();
        }

        //TODO : ��Ҫ�ع�������������Keys()����
        public override IEnumerable<TKey> Keys(TKey lo, TKey hi)
        {
            IEnumerable<TKey> allKeys = Keys();
            List<TKey> keys = new List<TKey>();
            foreach (var key in allKeys)
            {
                if (key.CompareTo(lo) >= 0 && key.CompareTo(hi) <= 0)
                {
                    keys.Add(key);
                }
            }
            return keys;
        }

        //TODO : ��Ҫ�ع�Ϊѭ���ķ�ʽ
        public override IEnumerable<TKey> Keys()
        {
            Queue<TKey> queue = new Queue<TKey>();
            Stack<Node<TKey,TValue>> stack = new Stack<Node<TKey,TValue>>();
            Node<TKey,TValue> n = _root;
            while (n != null || stack.Count > 0)
            {
                while (n != null)
                {
                    stack.Push(n);
                    n = n.Left;
                }
                var x = stack.Pop();
                queue.Enqueue(x.Key);
                n = x.Right;
            }
            return queue;
        }

        public override void DeleteMin()
        {
            if (_root == null)
            {
                return;
            }
            else if (_root.Left == null)
            {
                if (_root.Right != null)
                {
                    _root = _root.Right;
                }
                else
                {
                    _root = null;
                }
                _length--;
            }
            else
            {
                Node<TKey, TValue> parent = _root;
                Node<TKey, TValue> current = _root.Left;
                while (true)
                {
                    if (current.Left == null)
                    {
                        if (current.Right != null)
                        {
                            parent.Left = current.Right;
                            current.Right = null;
                        }
                        else
                        {
                            parent.Left = null;
                        }
                        _length--;
                        break;
                    }
                    else
                    {
                        parent = current;
                        current = current.Left;
                    }
                }
            }
        }

        public override void DeleteMax()
        {
            if (_root == null)
            {
                return;
            }
            else if (_root.Right == null)
            {
                if (_root.Left != null)
                {
                    _root = _root.Left;
                }
                else
                {
                    _root = null;
                }
                _length--;
            }
            else
            {
                Node<TKey, TValue> parent = _root;
                Node<TKey, TValue> current = _root.Right;
                while (true)
                {
                    if (current.Right == null)
                    {
                        if (current.Left != null)
                        {
                            parent.Right = current.Left;
                            current.Left = null;
                        }
                        else
                        {
                            parent.Right = null;
                        }
                        _length--;
                        break;
                    }
                    else
                    {
                        parent = current;
                        current = current.Right;
                    }
                }
            }
        }

        public override void Delete(TKey key)
        {

        }
    }

    public class BinarySearchTree_Recur<TKey, TValue> : OrderedSTBase<TKey, TValue> where TKey : IComparable
    {
        protected Node<TKey, TValue> _root;

        public override void Put(TKey key, TValue value)
        {
            _root = Put(_root, key, value);
        }

        private Node<TKey, TValue> Put(Node<TKey, TValue> x, TKey key, TValue value)
        {
            if (x == null)
            {
                return new Node<TKey, TValue>(key, value, 1);
            }
            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
            {
                x.Left = Put(x.Left, key, value);
            }
            else if (cmp > 0)
            {
                x.Right = Put(x.Right, key, value);
            }
            else
            {
                x.Value = value;
            }
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        public override TValue Get(TKey key)
        {
            Node<TKey, TValue> current = _root;
            while (current != null)
            {
                if (key.CompareTo(current.Key) == 0)
                {
                    return current.Value;
                }
                else if (key.CompareTo(current.Key) > 0)
                {
                    current = current.Right;
                }
                else
                {
                    current = current.Left;
                }
            }
            return default(TValue);
        }

        public override int Size()
        {
            return Size(_root);
        }

        private int Size(Node<TKey, TValue> node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.N;
        }

        public override TKey Min()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException();
            }
            return Min(_root).Key;
        }

        protected Node<TKey, TValue> Min(Node<TKey, TValue> x)
        {
            if (x.Left == null)
            {
                return x;
            }
            else
            {
                return Min(x.Left);
            }
        }

        public override TKey Max()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException();
            }
            return Max(_root).Key;
        }

        private Node<TKey, TValue> Max(Node<TKey, TValue> x)
        {
            if (x.Right == null)
            {
                return x;
            }
            else
            {
                return Max(x.Right);
            }
        }

        public override TKey Floor(TKey key)
        {
            Node<TKey, TValue> x = Floor(_root, key);
            if (x == null)
            {
                return default(TKey);
            }
            return x.Key;
        }

        /// <summary>
        /// ��һ�������ҵ���keyС�����ֵ
        /// </summary>
        /// <param name="x">���ĸ��ڵ�</param>
        /// <param name="key"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Floor(Node<TKey, TValue> x, TKey key)
        {
            if (x == null)
            {
                return null;
            }
            int cmp = key.CompareTo(x.Key);
            if (cmp == 0)
            {
                return x;
            }
            if (cmp < 0)
            {
                //key < x.key
                //x�Լ�x�������������Ժ��ԣ���Ϊ��Щ����key��Floor���ұ�keyС��
                return Floor(x.Left, key);
            }
            //key > x.key
            //x.key��keyС�����ǲ�ȷ���Ǳ�keyС������key�������ģ����Դ�x�����������������
            //ֻҪ���������в��Ҿͱض�����ֵ�������ܷ���null
            Node<TKey, TValue> t = Floor(x.Right, key);
            if (t != null)
            {
                //�����������ҵ�һ����keyС�ģ����key�϶���x.key�����Է������ֵ
                return t;
            }
            else
            {
                //����������û���ҵ���keyС�ģ���x.key���Ǳ�keyС�����ֵ������x
                return x;
            }
        }

        public override TKey Ceiling(TKey key)
        {
            Node<TKey, TValue> x = Ceiling(_root, key);
            if (x == null)
            {
                return default(TKey);
            }
            return x.Key;
        }

        private Node<TKey, TValue> Ceiling(Node<TKey, TValue> x, TKey key)
        {
            if (x == null)
            {
                return null;
            }
            int cmp = key.CompareTo(x.Key);
            if (cmp == 0)
            {
                return x;
            }
            if (cmp > 0)
            {
                return Ceiling(x.Right, key);
            }
            Node<TKey, TValue> t = Ceiling(x.Left, key);
            if (t != null)
            {
                return t;
            }
            else
            {
                return x;
            }
        }

        public override int Rank(TKey key)
        {
            return Rank(_root, key);
        }

        /// <summary>
        /// ��һ�������ҵ�key���±꣬����±��ǽ�key���뵽ST����±꣬Ҳ������С�ڵ�ǰkey��Ԫ�ص�����
        /// </summary>
        /// <param name="x">���ĸ��ڵ�</param>
        /// <param name="key"></param>
        /// <returns></returns>
        private int Rank(Node<TKey, TValue> x, TKey key)
        {
            if (x == null)
            {
                return 0;
            }
            int cmp = key.CompareTo(x.Key);
            if (cmp == 0)
            {
                //key�͵�ǰ�ڵ�key��ȣ������RankΪ������������
                int size = Size(x.Left);
                return size;
            }
            else if (cmp < 0)
            {
                //keyС�ڵ�ǰ�ڵ�key�����������ݹ�
                int rank = Rank(x.Left, key);
                return rank;
            }
            else
            {
                //key���ڵ�ǰ�ڵ�key���ȼ��������������ټ�1���ټ������������ݹ�
                int rank = 1 + Size(x.Left) + Rank(x.Right, key);
                return rank;
            }
        }

        public override TKey Select(int k)
        {
            Node<TKey, TValue> n = Select(_root, k);
            if (n != null)
            {
                return n.Key;
            }
            else
            {
                return default(TKey);
            }
        }

        /// <summary>
        /// ��һ�������ҵ��±�Ϊk��Ԫ�ص�key������k+1Ԫ�أ�Ҳ����k��Ԫ�ص�key���ȸ�Ԫ�ص�keyҪС
        /// </summary>
        /// <param name="x">���ĸ��ڵ�</param>
        /// <param name="k"></param>
        /// <returns></returns>
        private Node<TKey, TValue> Select(Node<TKey, TValue> x, int k)
        {
            if (x == null)
            {
                return null;
            }
            //�����������Ĵ�С
            int t = Size(x.Left);
            if (t > k)
            {
                //����������������k�����������м�������
                return Select(x.Left, k);
            }
            else if (t < k)
            {
                //������������С��k�������������ң�����������t+1��Ԫ�أ���t-1��k�Ӽ�ȥ�����µ�k������µ�kΪk-t-1
                return Select(x.Right, k - t - 1);
            }
            else
            {
                //����������������k������k��Ԫ�ص�key����x��keyҪС��x����Ҫ�ҵ�Ԫ�أ�����x
                return x;
            }
        }

        public override IEnumerable<TKey> Keys(TKey lo, TKey hi)
        {
            List<TKey> keys = new List<TKey>();
            Keys(keys, _root, lo, hi);
            return keys;
        }

        private void Keys(List<TKey> keys, Node<TKey, TValue> x, TKey lo, TKey hi)
        {
            if (x == null)
            {
                return;
            }
            //����������Node��ӵ�������
            if (x.Key.CompareTo(lo) > 0)
            {
                Keys(keys, x.Left, lo, hi);
            }
            //��root node��ӵ�������
            if (x.Key.CompareTo(lo) >= 0 && x.Key.CompareTo(hi) <= 0)
            {
                keys.Add(x.Key);
            }
            //����������Node��ӵ�������
            if (x.Key.CompareTo(hi) < 0)
            {
                Keys(keys, x.Right, lo, hi);
            }
        }

        public override void DeleteMin()
        {
            _root = DeleteMin(_root);
        }

        private Node<TKey, TValue> DeleteMin(Node<TKey, TValue> x)
        {
            if (x.Left == null)
            {
                return x.Right;
            }
            x.Left = DeleteMin(x.Left);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        public override void DeleteMax()
        {
            _root = DeleteMax(_root);
        }

        private Node<TKey, TValue> DeleteMax(Node<TKey, TValue> x)
        {
            if (x.Right == null)
            {
                return x.Left;
            }
            x.Right = DeleteMax(x.Right);
            x.N = Size(x.Left) + Size(x.Right) + 1;
            return x;
        }

        public override void Delete(TKey key)
        {
            _root = Delete(_root, key);
        }

        private Node<TKey, TValue> Delete(Node<TKey, TValue> x, TKey key)
        {
            if (x == null)
            {
                return null;
            }
            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
            {
                x.Left = Delete(x.Left, key);
            }
            else if (cmp > 0)
            {
                x.Right = Delete(x.Right, key);
            }
            else
            {
                if (x.Right == null)
                {
                    return x.Left;
                }
                if (x.Left == null)
                {
                    return x.Right;
                }

                Node<TKey, TValue> t = x;
                //��x����Ϊԭ�ڵ������������С�Ľڵ�
                x = Min(t.Right);
                //��ԭ�ڵ������������С�ڵ�ɾ��������x����������ɾ���������ظ�
                //��ԭ�ڵ������������Ϊx��������
                x.Right = DeleteMin(t.Right);
                //��ԭ�ڵ������������Ϊx��������
                x.Left = t.Left;
            }
            x.N = Size(x.Left) + Size(x.Right) + 1;
            //x���غ󼴽�ԭ�ڵ��滻���������ú��������������������º���N
            return x;
        }
    }

    public class Node<TKey, TValue>
    {
        public Node()
        {

        }

        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public Node(TKey key, TValue value, int n) : this(key, value)
        {
            N = n;
        }

        public Node(TKey key, TValue value, int n, bool color) : this(key, value, n)
        {
            Color = color;
        }

        public TKey Key { get; set; }

        public TValue Value { get; set; }

        public int N { get; set; }

        public bool Color { get; set; }

        public Node<TKey, TValue> Left { get; set; }

        public Node<TKey, TValue> Right { get; set; }
    }
}