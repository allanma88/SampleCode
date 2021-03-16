using System;
using System.Collections.Generic;

namespace Map
{
    public class HashSet<TKey, TValue>
    {
        private List<TKey>[] _keys;
        private List<TValue>[] _vals;
        private const int MinCapacity = 16;
        private int _capacity;
        private int _lowLoadFactor;
        private int _highLoadFactor;
        private int _count;

        public HashSet() : this(16)
        {

        }

        public HashSet(int capcity) : this(capcity, 2, 8)
        {

        }

        public HashSet(int capcity, int lowLoadFactor, int highLoadFactor)
        {
            _capacity = ComputeCapcity(capcity);
            _lowLoadFactor = lowLoadFactor;
            _highLoadFactor = highLoadFactor;
            _keys = new List<TKey>[_capacity];
            _vals = new List<TValue>[_capacity];
        }

        private int ComputeCapcity(int n)
        {
            if ((n ^ (n - 1)) != 0)
            {
                int m = (int)Math.Log(n, 2) + 1;
                return (int)Math.Pow(2, m + 1);
            }
            return n;
        }

        public bool ContainsKey(TKey key)
        {
            int index = Hash(key);
            List<TKey> keyList = _keys[index];
            if (keyList != null)
            {
                for (int i = 0; i < keyList.Count; i++)
                {
                    if (key.Equals(keyList[i]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public TValue Get(TKey key)
        {
            int index = Hash(key);
            List<TKey> keyList = _keys[index];
            if (keyList != null)
            {
                for (int i = 0; i < keyList.Count; i++)
                {
                    if (key.Equals(keyList[i]))
                    {
                        return _vals[index][i];
                    }
                }
            }
            throw new KeyNotFoundException();
        }

        private int Hash(TKey key)
        {
            int hash = key.GetHashCode();
            int index = hash & (_capacity - 1);
            return index;
        }

        public void Set(TKey key, TValue val)
        {
            int index = Hash(key);
            List<TKey> keyList = _keys[index];
            if (keyList == null)
            {
                _keys[index] = new List<TKey>() { key };
                _vals[index] = new List<TValue>() { val };
                _count++;
            }
            else
            {
                int i;
                for (i = 0; i < keyList.Count; i++)
                {
                    if (key.Equals(keyList[i]))
                    {
                        _vals[index][i] = val;
                        break;
                    }
                }
                if (i == keyList.Count)
                {
                    _vals[index].Add(val);
                    _count++;
                }
            }
            if (_count > _highLoadFactor * _capacity)
            {
                Resize(2 * _capacity);
            }
        }

        public IEnumerable<TKey> Keys()
        {
            List<TKey> keys = new List<TKey>();
            foreach (var keyList in _keys)
            {
                keys.AddRange(keyList);
            }
            return keys;
        }

        public void Delete(TKey key)
        {
            int index = Hash(key);
            List<TKey> keyList = _keys[index];
            if (keyList != null)
            {
                for (int i = 0; i < keyList.Count; i++)
                {
                    if (key.Equals(keyList[i]))
                    {
                        _vals[index].RemoveAt(i);
                        if (--_count < _lowLoadFactor * _capacity)
                        {
                            Resize(_capacity / 2);
                        }
                        break;
                    }
                }
            }
        }

        private void Resize(int capcity)
        {
            if (capcity < MinCapacity) return;
            HashSet<TKey, TValue> set = new HashSet<TKey, TValue>(capcity, _lowLoadFactor, _highLoadFactor);
            foreach (TKey key in Keys())
            {
                TValue val = Get(key);
                set.Set(key, val);
            }
            _keys = set._keys;
            _vals = set._vals;
        }
    }
}