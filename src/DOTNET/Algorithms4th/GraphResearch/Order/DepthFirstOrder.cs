using System.Collections.Generic;

namespace GraphResearch
{
    public class DepthFirstOrder
    {
        private bool[] _marked;

        private Queue<int> _pre = new Queue<int>(); // vertices in preorder

        private Queue<int> _post = new Queue<int>(); // vertices in postorder

        private Stack<int> _reversePost = new Stack<int>(); // vertices in reverse postorder

        public DepthFirstOrder(Digraph G)
        {
            _marked = new bool[G.V()];
            for (int v = 0; v < G.V(); v++)
            {
                if (!_marked[v])
                {
                    Dfs(G, v);
                }
            }
        }

        public IEnumerable<int> Pre()
        {
            return _pre;
        }

        public IEnumerable<int> Post()
        {
            return _post;
        }

        public IEnumerable<int> ReversePost()
        {
            return _reversePost;
        }

        private void Dfs(Digraph G, int v)
        {
            _pre.Enqueue(v);
            _marked[v] = true;
            foreach (var w in G.Adj(v))
            {
                if (!_marked[w])
                {
                    Dfs(G, w);
                }
            }
            _reversePost.Push(v);
            _post.Enqueue(v);
        }
    }
}