using System;
using System.Collections.Generic;

namespace SearchResearch
{
    public abstract class OrderedSTBase<TKey, TValue> : STBase<TKey, TValue> where TKey : IComparable
    {
        public abstract TKey Min();

        public abstract TKey Max();

        public abstract TKey Floor(TKey key);

        public abstract TKey Ceiling(TKey key);

        public abstract int Rank(TKey key);

        public abstract TKey Select(int k);

        public virtual void DeleteMin()
        {
            Delete(Min());
        }

        public virtual void DeleteMax()
        {
            Delete(Max());
        }

        public int Size(TKey lo, TKey hi)
        {
            if (hi.CompareTo(lo) < 0)
            {
                return 0;
            }
            else if (Contains(hi))
            {
                return Rank(hi) - Rank(lo) + 1;
            }
            else
            {
                return Rank(hi) - Rank(lo);
            }
        }

        public abstract IEnumerable<TKey> Keys(TKey lo, TKey hi);

        public override IEnumerable<TKey> Keys()
        {
            if (!IsEmpty())
            {
                return Keys(Min(), Max());
            }
            else
            {
                return new List<TKey>();
            }
        }
    }
}