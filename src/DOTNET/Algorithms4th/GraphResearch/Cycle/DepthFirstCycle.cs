namespace GraphResearch
{
    public class DepthFirstCycle : Cycle
    {
        private bool[] _marked;

        private bool _hasCycle;

        public DepthFirstCycle(Graph G)
            : base(G)
        {
            _marked = new bool[G.V()];
            for (int i = 0; i < G.V(); i++)
            {
                if (!_marked[i])
                {
                    Dfs(G, i, i);
                }
            }
        }

        private void Dfs(Graph G, int v, int u)
        {
            _marked[v] = true;
            foreach (int i in G.Adj(v))
            {
                if (!_marked[i])
                {
                    Dfs(G, i, v);
                }
                else if(i != u)
                {
                    //i�Ѿ����ʹ����Ҳ���v����һ���ڵ�
                    //��һ���Ƿ�����һȦ�ص���ԭ�㵼�µģ���ʱһ����cycle
                    _hasCycle = true;
                }
            }
        }

        public override bool HasCycle()
        {
            return _hasCycle;
        }
    }
}