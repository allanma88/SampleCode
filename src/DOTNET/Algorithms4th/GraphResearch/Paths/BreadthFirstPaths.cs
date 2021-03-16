using System.Collections.Generic;

namespace GraphResearch
{
    public class BreadthFirstPaths : Paths
    {
        private Queue<int> _queue = new Queue<int>();

        private bool[] _marked;

        private int[] _edgeTo;

        private int _s;

        public BreadthFirstPaths(Graph G, int s)
            : base(G, s)
        {
            _marked = new bool[G.V()];
            _edgeTo = new int[G.V()];
            _s = s;
            Bfs(G, s);
        }

        private void Bfs(Graph G, int v)
        {
            _marked[v] = true;
            _queue.Enqueue(v);
            while (_queue.Count > 0)
            {
                int i = _queue.Dequeue();
                foreach (var j in G.Adj(i))
                {
                    if (!_marked[j])
                    {
                        _marked[j] = true;
                        _edgeTo[j] = i;
                        _queue.Enqueue(j);
                    }
                }
            }
        }

        public override bool HasPathTo(int v)
        {
            return _marked[v];
        }

        public override IEnumerable<int> PathTo(int v)
        {
            if (!HasPathTo(v))
            {
                return null;
            }
            Stack<int> stack = new Stack<int>();
            for (int i = v; i != _s; i = _edgeTo[i])
            {
                stack.Push(i);
            }
            stack.Push(_s);
            return stack;
        }
    }
}