using System.Collections;
using System.Collections.Generic;

namespace Basic
{
    public class Bag<T> : IEnumerable<T>
    {
        private T[] _items;

        private int _N;

        public Bag()
        {
            _items = new T[16];
        }

        public void Add(T item)
        {
            _items[_N++] = item;
            if (_N >= _items.Length)
            {
                Resize(2*_N);
            }
        }

        public bool IsEmpty()
        {
            return _N == 0;
        }

        public int Size()
        {
            return _N;
        }

        private void Resize(int n)
        {
            T[] items = new T[n];
            for (int i = 0; i < _items.Length; i++)
            {
                items[i] = _items[i];
            }
            _items = items;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new BagEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class BagEnumerator : IEnumerator<T>
        {
            private Bag<T> _bag;

            private int _current = -1;

            public BagEnumerator(Bag<T> bag)
            {
                _bag = bag;
            }

            public bool MoveNext()
            {
                if (_current < _bag._N - 1)
                {
                    _current++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public void Reset()
            {
                _current = -1;
            }

            T IEnumerator<T>.Current
            {
                get
                {
                    return _bag._items[_current];
                }
            }

            public object Current
            {
                get
                {
                    return _bag._items[_current];
                }
            }

            public void Dispose()
            {
                Reset();
            }
        }
    }
}