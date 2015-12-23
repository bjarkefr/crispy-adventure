namespace QuadTreeBenchmark
{
    using System;
    using System.Collections.Generic;

    public interface QuadTree
    {
        int Add(Rectangle bounds);

        void Remove(int id);

        List<int> Query(Rectangle bounds);

        List<Tuple<int, int>> QueryOverlaps(); // Report only unique collisions

        void Translate(int id, Vector direction);
    }
}