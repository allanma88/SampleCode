using System;

namespace DataStructure
{
    public class DualStack
    {
        private int[] _nums;
        private int _first;
        private int _second;

        public DualStack(int n)
        {
            _nums = new int[n];
            _first = -1;
            _second = n;
        }

        public void Push(int num, int i)
        {
            if (i == 1)
            {
                if (_first + 1 == _second)
                {
                    throw new Exception("first stack overflow");
                }
                _nums[++_first] = num;
            }
            else if (i == 2)
            {
                if (_first + 1 == _second)
                {
                    throw new Exception("second stack overflow");
                }
                _nums[--_second] = num;
            }
            else
            {
                throw new Exception("only supoport 1 and 2");
            }
        }

        public int Pop(int i)
        {
            if (i == 1)
            {
                if (_first == -1)
                {
                    throw new Exception("first stack underflow");
                }
                return _nums[_first--];
            }
            else if (i == 2)
            {
                if (_second == _nums.Length)
                {
                    throw new Exception("second stack underflow");
                }
                return _nums[_second++];
            }
            else
            {
                throw new Exception("only supoport 1 and 2");
            }
        }
    }
}