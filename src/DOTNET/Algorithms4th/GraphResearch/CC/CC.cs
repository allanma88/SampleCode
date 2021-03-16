namespace GraphResearch
{
    public abstract class CC
    {
        public CC(Graph G)
        {
            
        }

        public abstract bool Connected(int v, int w);

        public abstract int Count();

        public abstract int Id(int v);
    }
}
