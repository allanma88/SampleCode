using System;
using System.Collections.Generic;

namespace SearchResearch
{
    public class BinarySearchST<TKey, TValue> : OrderedSTBase<TKey, TValue> where TKey : IComparable
    {
        private TKey[] _keys;

        private TValue[] _values;

        private int _count;

        public BinarySearchST(int capacity)
        {
            _keys = new TKey[capacity];
            _values = new TValue[capacity];
        }

        public override void Put(TKey key, TValue value)
        {
            int rank = Rank(key);
            if (rank < _count && _keys[rank].CompareTo(key) == 0)
            {
                _values[rank] = value;
            }
            else
            {
                for (int i = _count; i > rank; i--)
                {
                    _keys[i] = _keys[i - 1];
                    _values[i] = _values[i - 1];
                }
                _keys[rank] = key;
                _values[rank] = value;
                _count++;
            }
        }

        public override TValue Get(TKey key)
        {
            if (IsEmpty())
            {
                return default(TValue);
            }
            int rank = Rank(key);
            if (rank < _count && _keys[rank].CompareTo(key) == 0)
            {
                return _values[rank];
            }
            else
            {
                return default(TValue);
            }
        }

        public override int Size()
        {
            return _count;
        }

        public override TKey Min()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException();
            }
            return _keys[0];
        }

        public override TKey Max()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException();
            }
            return _keys[_count - 1];
        }

        public override TKey Floor(TKey key)
        {
            int rank = Rank(key);
            if (rank < _count && _keys[rank].CompareTo(key) == 0)
            {
                return _keys[rank];
            }
            else
            {
                if (rank > 1)
                {
                    return _keys[rank - 1];
                }
                else
                {
                    return default(TKey);
                }
            }
        }

        public override TKey Ceiling(TKey key)
        {
            int rank = Rank(key);
            return _keys[rank];
        }

        public override int Rank(TKey key)
        {
            return Rank(key, 0, _count - 1);
            //int rank = 0;
            //for (int i = 0; i < _count; i++)
            //{
            //    if (_keys[i].CompareTo(key) < 0)
            //    {
            //        rank++;
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            //return rank;
        }

        public override TKey Select(int k)
        {
            if (k >= _count)
            {
                throw new InvalidOperationException();
            }
            return _keys[k];
        }

        public override IEnumerable<TKey> Keys(TKey lo, TKey hi)
        {
            int loRank = Rank(lo);
            int hiRank = Rank(hi);
            List<TKey> keys = new List<TKey>();
            for (int i = loRank; i <= hiRank; i++)
            {
                keys.Add(_keys[i]);
            }
            return keys;
        }

        public override void Delete(TKey key)
        {
            int rank = Rank(key);
            if (rank < _count && _keys[rank].CompareTo(key) == 0)
            {
                for (int i = rank; i < --_count; i++)
                {
                    _keys[i] = _keys[i + 1];
                    _values[i] = _values[i + 1];
                }
            }
        }

        private int Rank(TKey key, int lo, int hi)
        {
            if (hi < lo)
            {
                return lo;
            }
            int mid = (lo + hi) / 2;
            int cmp = key.CompareTo(_keys[mid]);
            if (cmp < 0)
            {
                return Rank(key, lo, mid - 1);
            }
            else if (cmp > 0)
            {
                return Rank(key, mid + 1, hi);
            }
            else
            {
                return mid;
            }
        }
    }
}