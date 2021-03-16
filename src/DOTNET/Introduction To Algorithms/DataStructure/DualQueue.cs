using System;

namespace DataStructure
{
    public class DualQueue
    {
        private int[] _nums;
        private int _start;
        private int _end;
        private int _n;

        public DualQueue(int n)
        {
            _n = n + 1;
            _nums = new int[n + 1];
        }

        public void EnqueueStart(int num)
        {
            if (_start == (_end + 1) % _n)
            {
                throw new Exception("overflow");
            }
            _start = (_start + _n - 1) % _n;
            _nums[_start] = num;
        }

        public int DequeueStart()
        {
            if (_start == _end)
            {
                throw new Exception("underflow");
            }
            int num = _nums[_start];
            _start = (_start + 1) % _n;
            return num;
        }

        public void EnqueueEnd(int num)
        {
            if (_start == (_end + 1) % _n)
            {
                throw new Exception("overflow");
            }
            _nums[_end] = num;
            _end = (_end + 1) % _n;
        }

        public int DequeueEnd()
        {
            if (_start == _end)
            {
                throw new Exception("underflow");
            }
            _end = (_end + _n - 1) % _n;
            int num = _nums[_end];
            return num;
        }
    }
}