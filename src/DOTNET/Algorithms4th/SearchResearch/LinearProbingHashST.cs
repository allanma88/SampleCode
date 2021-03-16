using System.Collections.Generic;

namespace SearchResearch
{
    public class LinearProbingHashST<TKey, TValue> : STBase<TKey, TValue>
    {
        private int _M;

        private int _N;

        private TKey[] _keys;

        private TValue[] _values;

        public LinearProbingHashST() : this(16)
        {

        }

        public LinearProbingHashST(int M)
        {
            _M = M;
            _keys = new TKey[_M];
            _values = new TValue[_M];
        }

        public override void Put(TKey key, TValue value)
        {
            if (_N >= _M / 2)
            {
                Resize(2 * _M);
            }
            int i;
            for (i = Hash(key); _keys[i] != null && !_keys[i].Equals(default(TKey)); i = (i + 1) % _M)
            {
                if (_keys[i].Equals(key))
                {
                    _values[i] = value;
                    return;
                }
            }
            _keys[i] = key;
            _values[i] = value;
            _N++;
        }

        public override TValue Get(TKey key)
        {
            int i;
            for (i = Hash(key); _keys[i] != null && !_keys[i].Equals(default(TKey)); i = (i + 1) % _M)
            {
                if (_keys[i].Equals(key))
                {
                    return _values[i];
                }
            }
            return default(TValue);
        }

        public override int Size()
        {
            return _N;
        }

        public override IEnumerable<TKey> Keys()
        {
            List<TKey> keys = new List<TKey>();
            foreach (var key in _keys)
            {
                if (key != null && !key.Equals(default(TKey)))
                {
                    keys.Add(key);
                }
            }
            return keys;
        }

        public override void Delete(TKey key)
        {
            if (!Contains(key))
            {
                return;
            }
            int i = Hash(key);
            while (!key.Equals(_keys[i]))
            {
                i = (i + 1) % _M;
            }
            _keys[i] = default(TKey);
            _values[i] = default(TValue);
            i = (i + 1) % _M;
            while (_keys[i] != null && !_keys[i].Equals(default(TKey)))
            {
                var tempKey = _keys[i];
                var tempValue = _values[i];
                _keys[i] = default(TKey);
                _values[i] = default(TValue);
                Put(tempKey, tempValue);
                i = (i + 1) % _M;
            }
            _N--;
            if (_N > 0 && _N == _M / 8)
            {
                Resize(_M / 2);
            }
        }

        public override bool Contains(TKey key)
        {
            int i;
            for (i = Hash(key); _keys[i] != null && !_keys[i].Equals(default(TKey)); i = (i + 1) % _M)
            {
                if (_keys[i].Equals(key))
                {
                    return true;
                }
            }
            return false;
        }

        private int Hash(TKey key)
        {
            //if (key is string)
            //{
            //    Dictionary<string, int> dic = new Dictionary<string, int>();
            //    dic.Add("S", 6);
            //    dic.Add("E", 10);
            //    dic.Add("A", 4);
            //    dic.Add("R", 14);
            //    dic.Add("C", 5);
            //    dic.Add("H", 4);
            //    dic.Add("X", 15);
            //    return dic[key as string];
            //}
            //将key的hash由32位数值转换为31位非负数，然后计算相对M的余数
            return (key.GetHashCode() & 0x7fffffff) % _M;
        }

        private void Resize(int cap)
        {
            var st = new LinearProbingHashST<TKey, TValue>(cap);
            foreach (TKey key in Keys())
            {
                st.Put(key, Get(key));
            }
            _keys = st._keys;
            _values = st._values;
            _M = st._M;
        }
    }
}