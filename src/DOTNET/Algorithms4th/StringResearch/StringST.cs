using SearchResearch;
using System.Collections.Generic;

namespace StringResearch
{
    public abstract class StringST<TValue> : STBase<string, TValue>
    {
        public abstract string LongestPrefixOf(string s);

        public abstract IEnumerable<string> keysWithPrefix(string s);

        public abstract IEnumerable<string> keysThatMatch(string s);
    }
}