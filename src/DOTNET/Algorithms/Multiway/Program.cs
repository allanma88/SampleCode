using System;
using System.Collections.Generic;

namespace Multiway
{
    class Program
    {
        public static void Main(string[] args)
        {
            Queue<int>[] collections = new Queue<int>[]
            {
                new Queue<int>(new int[]{1,2,3,6,7,9,9,26}),
                new Queue<int>(new int[]{2,4,8,15,16,16}),
                new Queue<int>(new int[]{2,2,4,5,10,14})
                //new Queue<int>(new int[]{1,2}),
                //new Queue<int>(new int[]{3,4}),
                //new Queue<int>(new int[]{5,6})
            };
            Merge(collections);
            Console.ReadLine();
        }

        public static void Merge(Queue<int>[] collections)
        {
            int len = collections.Length;
            IndexHeapMinPQ pq = new IndexHeapMinPQ(len);
            for (int i = 0; i < len; i++)
            {
                if (collections[i].Count > 0)
                {
                    pq.Insert(i, collections[i].Dequeue());
                }
            }
            while (!pq.IsEmpty())
            {
                Console.WriteLine(pq.Min());
                int i = pq.RemoveMin();
                if (collections[i].Count > 0)
                {
                    pq.Insert(i, collections[i].Dequeue());
                }
            }
        }
    }
}