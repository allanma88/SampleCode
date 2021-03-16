using System;

namespace Array
{
    public class Copy
    {
        public void CopyTo(int[] src, int[] dst, int start, int len)
        {
            for (int i = start; i < start + len; i++)
            {
                dst[i] = src[i];
            }
        }
    }
}