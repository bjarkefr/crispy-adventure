namespace QuadTreeBenchmark
{
    public struct Rectangle
    {
        public readonly Vector Bl;
        public readonly Vector Tr;

        public Rectangle(Vector bl, Vector tr)
        {
            Bl = bl;
            Tr = tr;
        }
    }
}