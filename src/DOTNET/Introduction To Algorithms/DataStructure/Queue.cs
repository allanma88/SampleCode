using System;

namespace DataStructure
{
    public class Queue
    {
        private int _start;
        private int _end;
        private int _n;
        private int[] _nums;

        public Queue(int n)
        {
            _n = n + 1;
            _nums = new int[n + 1];
        }

        public void Enqueue(int num)
        {
            if (_start == (_end + 1) % _n)
            {
                throw new Exception("overflow");
            }
            _nums[_end] = num;
            _end = (_end + 1) % _n;
        }

        public int Dequeue()
        {
            if (_start == _end)
            {
                throw new Exception("underflow");
            }
            int num = _nums[_start];
            _start = (_start + 1) % _n;
            return num;
        }
    }
}