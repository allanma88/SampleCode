using System;

namespace Basic
{
    public class List<T>
    {
        private const int InitLen = 10;
        private T[] _innerList = new T[InitLen];
        private int _nextIndex;

        public void Add(T item)
        {
            if (_nextIndex >= _innerList.Length)
            {
                T[] newList = new T[_innerList.Length + InitLen];
                Array.Copy(_innerList, newList, _innerList.Length);
                _innerList = newList;

            }
            _innerList[_nextIndex++] = item;
        }

        public void Remove(T item)
        {
            int index = Array.FindIndex(_innerList, x => ((Object)x).Equals(item));
            T[] newList = new T[_innerList.Length];
            Array.Copy(_innerList, newList, index);
            Array.Copy(_innerList, index + 1, newList, index + 1, _innerList.Length - index - 1);
            _innerList = newList;
            _nextIndex--;
        }

        public T Get(int index)
        {
            if (index < _nextIndex)
            {
                return _innerList[index];
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        public int Count()
        {
            return _nextIndex;
        }
    }
}