using System;
using System.Text;

namespace HeapSort
{
    public class YoungTableau
    {
        private int[,] _matrix;
        private int _n;
        private int _len;

        public YoungTableau(int[,] matrix, int n)
        {
            _matrix = matrix;
            _n = n;
            _len = matrix.GetLength(0);
        }

        public int ExtractMin()
        {
            int max = _matrix[0, 0];
            int r = _n / _len;
            int c = _n % _len;
            _matrix[0, 0] = _matrix[r, c];
            _n--;
            MinHeapify(0, 0);
            return max;
        }

        private void MinHeapify(int r, int c)
        {
            int minR = r, minC = c, min = _matrix[r, c];
            if (c + 1 < _len && min > _matrix[r, c + 1])
            {
                min = _matrix[r, c + 1];
                minR = r;
                minC = c + 1;
            }
            if (r + 1 < _len && min > _matrix[r + 1, c])
            {
                min = _matrix[r + 1, c];
                minR = r + 1;
                minC = c;
            }
            if (minR != r || minC != c)
            {
                int temp = _matrix[r, c];
                _matrix[r, c] = _matrix[minR, minC];
                _matrix[minR, minC] = temp;
                MinHeapify(minR, minC);
            }
        }

        public void Insert(int num)
        {
            if (_n + 1 == _len * _len)
            {
                throw new Exception("matrix is full");
            }
            _n++;
            int r = _n / _len;
            int c = _n % _len;
            _matrix[r, c] = num;

            MinHeapifyUp(r, c);
        }

        private void MinHeapifyUp(int r, int c)
        {
            int maxR = r, maxC = c, max = _matrix[r, c];
            if (r > 0 && max < _matrix[r - 1, c])
            {
                max = _matrix[r - 1, c];
                maxR = r - 1;
                maxC = c;
            }
            if (c > 0 && max < _matrix[r, c - 1])
            {
                max = _matrix[r, c - 1];
                maxR = r;
                maxC = c - 1;
            }
            if (maxR != r || maxC != c)
            {
                int temp = _matrix[r, c];
                _matrix[r, c] = _matrix[maxR, maxC];
                _matrix[maxR, maxC] = temp;

                MinHeapifyUp(maxR, maxC);
            }
        }

        public bool Search(int num)
        {
            return Search(num, 0, _len - 1, 0, _len - 1);
        }

        private bool Search(int num, int startR, int endR, int startC, int endC)
        {
            if (startR == endR && startC == endC)
            {
                return num == _matrix[startR, startC];
            }
            if (num == _matrix[startR, startC] || num == _matrix[endR, endC])
            {
                return true;
            }
            if (num < _matrix[startR, startC] || num > _matrix[endR, endC])
            {
                return false;
            }
            int midR = (startR + endR) / 2;
            int midC = (startC + endC) / 2;
            if (Search(num, startR, midR, startC, midC))
            {
                return true;
            }
            if (midC + 1 < _len && Search(num, startR, midR, midC + 1, endC))
            {
                return true;
            }
            if (midR + 1 < _len && Search(num, midR + 1, endR, startC, midC))
            {
                return true;
            }
            if (midR + 1 < _len && midC + 1 < _len && Search(num, midR + 1, endR, midC + 1, endC))
            {
                return true;
            }
            return false;
        }

        public int[] Sort()
        {
            int[] nums = new int[_len * _len];
            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = ExtractMin();
            }
            return nums;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int r = 0; r < _len; r++)
            {
                for (int c = 0; c < _len; c++)
                {
                    if (r * _len + c <= _n)
                    {
                        builder.Append(_matrix[r, c] + " ");
                    }
                }
                builder.AppendLine();
            }
            return builder.ToString();
        }
    }
}
