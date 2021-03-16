using System.Collections.Generic;

namespace Graph
{
    public class AdjHashsetDigraph : DigraphBase
    {
        private HashSet<int>[] _hashSets;

        public AdjHashsetDigraph(int v) : base(v)
        {
            _hashSets = new HashSet<int>[v];
            for (int i = 0; i < v; i++)
            {
                _hashSets[i] = new HashSet<int>();
            }
        }

        public override void AddEdge(int i, int j)
        {
            base.AddEdge(i, j);
            _hashSets[i].Add(j);
        }

        public override ICollection<int> Adj(int i)
        {
            return _hashSets[i];
        }

        public bool Exist1(int i, int j)
        {
            return _hashSets[i].Contains(j);
        }
    }
}