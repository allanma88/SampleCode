using System;

namespace Basic
{
    public abstract class Stack<T>
    {
        public abstract void Push(T item);

        public abstract T Pop();

        public abstract bool IsEmpty();

        public abstract int Size();
    }

    public class Stack_LinkList<T> : Stack<T>
    {
        private Node<T> _start;

        public override void Push(T item)
        {
            var node = new Node<T>() { Value = item };
            if (_start != null)
            {
                node.Next = _start;
            }
            _start = node;
        }

        public override T Pop()
        {
            if (_start != null)
            {
                var startValue = _start.Value;
                _start = _start.Next;
                return startValue;
            }
            else
            {
                return default(T);
            }
        }

        public override bool IsEmpty()
        {
            return _start == null;
        }

        public override int Size()
        {
            int i = 0;
            var node = _start;
            while (node != null)
            {
                node = node.Next;
                i++;
            }
            return i;
        }
    }
    
    public class Stack_Array<T> : Stack<T>
    {
        private const int initLength = 10;

        private T[] _list = new T[initLength];

        private int _start = 0;

        public override void Push(T item)
        {
            if (_start >= _list.Length)
            {
                T[] newList = new T[_list.Length + initLength];
                Array.Copy(_list, newList, _list.Length);
                _list = newList;
            }
            _list[_start++] = item;
        }

        public override T Pop()
        {
            if (_start > 0)
            {
                return _list[--_start];
            }
            else
            {
                return default(T);
            }
        }

        public override bool IsEmpty()
        {
            return _start <= 0;
        }

        public override int Size()
        {
            return _start;
        }
    }
}