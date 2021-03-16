namespace Graph
{
    public class Vertex
    {
        public int Val;

        public Color Color;

        public int D;
        
        public int F;

        public Vertex Parent;
    }

    public enum Color
    {
        White,
        Gray,
        Black
    }
}