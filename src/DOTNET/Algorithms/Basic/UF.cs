using System;

namespace Basic
{
    public class UF
    {
        private int[] id;
        
        private int count;

        public UF(int N)
        {
            count = N;
            id = new int[N];
            for (int i = 0; i < N; i++)
            {
                id[i] = i;
            }
        }

        public int Count()
        { 
            return count;
        }

        public bool Connected(int p, int q)
        { 
            return Find(p) == Find(q);
        }

        public int Find(int p)
        {
            while (p != id[p])
            {
                p = id[p];
            }
            return p;
        }

        public void Union(int p, int q)
        {
            int pRoot = Find(p);
            int qRoot = Find(q);
            if (pRoot == qRoot) 
            { 
                return;
            }
            id[pRoot] = qRoot;
            count--;
        }
    }
}