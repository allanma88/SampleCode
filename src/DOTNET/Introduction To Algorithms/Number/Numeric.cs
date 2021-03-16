using System;

namespace Number
{
    public class Numeric
    {
        public void Run()
        {
            byte[] bytes = new byte[8];
            bytes[0] = 1;
            bytes[5] = 1;
            bytes[6] = 1;

            int num = GetInt(bytes);
            Console.WriteLine(num);

            num = GetInt1(bytes);
            Console.WriteLine(num);
        }

        public int GetInt1(byte[] bytes)
        {
            return GetInt1(bytes, 0, bytes.Length - 1);
        }

        public int GetInt1(byte[] bytes, int lo, int hi)
        {
            if (lo == hi)
            {
                return bytes[lo];
            }
            int mid = (lo + hi) / 2;
            int k = hi - mid;
            return GetInt1(bytes, lo, mid) * (1 << k) + GetInt1(bytes, mid + 1, hi);
        }

        public int GetInt(byte[] bytes)
        {
            if (bytes.Length == 1)
            {
                return bytes[0];
            }
            int n = bytes.Length / 2;
            byte[] left = new byte[n];
            byte[] right = new byte[bytes.Length - n];
            for (int i = 0; i < n; i++)
            {
                left[i] = bytes[i];
            }
            for (int i = n; i < bytes.Length; i++)
            {
                right[i - n] = bytes[i];
            }
            int k = bytes.Length - n;
            return GetInt(left) * (1 << k) + GetInt(right);
        }
    }
}