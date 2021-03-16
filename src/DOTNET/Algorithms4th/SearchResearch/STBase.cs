using System.Collections.Generic;

namespace SearchResearch
{
    public abstract class STBase<TKey, TValue>
    {
        public abstract void Put(TKey key, TValue value);

        public abstract TValue Get(TKey key);

        public virtual void Delete(TKey key)
        {
            Put(key, default(TValue));
        }

        public virtual bool Contains(TKey key)
        {
            return Get(key) != null;
        }

        public virtual bool IsEmpty()
        {
            return Size() == 0;
        }

        public abstract int Size();

        public abstract IEnumerable<TKey> Keys();
    }
}