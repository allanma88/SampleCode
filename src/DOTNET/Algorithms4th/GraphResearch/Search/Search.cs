namespace GraphResearch
{
    public abstract class Search
    {
        public Search(Graph G, int s)
        {
           
        }

        public abstract bool Marked(int v);

        public abstract int Count();
    }
}