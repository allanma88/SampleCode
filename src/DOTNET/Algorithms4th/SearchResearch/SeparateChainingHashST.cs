using System;
using System.Collections.Generic;

namespace SearchResearch
{
    public class SeparateChainingHashST<TKey, TValue> : STBase<TKey, TValue> where TKey : IComparable<TKey>
    {
        private SequentialSearchST<TKey, TValue>[] _st;

        private int _M;

        private int _N;

        public SeparateChainingHashST() : this(997)
        {

        }

        public SeparateChainingHashST(int M)
        {
            _M = M;
            _st = new SequentialSearchST<TKey, TValue>[M];
            for (int i = 0; i < _M; i++)
            {
                _st[i] = new SequentialSearchST<TKey, TValue>();
            }
        }

        public override void Put(TKey key, TValue value)
        {
            if (_N >= _M / 2)
            //if (_N / _M >= 8)
            {
                Resize(2 * _M);
            }
            int hash = Hash(key);
            _st[hash].Put(key, value);
            _N++;
        }

        public override TValue Get(TKey key)
        {
            int hash = Hash(key);
            return _st[hash].Get(key);
        }

        public override int Size()
        {
            return _N;
        }

        public override IEnumerable<TKey> Keys()
        {
            List<TKey> keys = new List<TKey>();
            foreach (var sequentialSearchSt in _st)
            {
                foreach (var key in sequentialSearchSt.Keys())
                {
                    keys.Add(key);
                }
            }
            return keys;
        }

        public override void Delete(TKey key)
        {
            int hash = Hash(key);
            _st[hash].Delete(key);
            _N--;
            if (_N > 0 && _N <= _M / 8)
            //if (_N > 0 && _N / _M <= 2)
            {
                Resize(_M / 2);
            }
        }

        private int Hash(TKey key)
        {
            //将key的hash由32位数值转换为31位非负数，然后计算相对M的余数
            return (key.GetHashCode() & 0x7fffffff) % _M;
        }

        private void Resize(int cap)
        {
            var st = new SeparateChainingHashST<TKey, TValue>(cap);
            foreach (var key in Keys())
            {
                st.Put(key, Get(key));
            }
            _M = st._M;
            _st = st._st;
        }
    }
}