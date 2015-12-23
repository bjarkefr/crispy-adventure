namespace QuadTreeBenchmark
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ConsoulAcclimation.Geometry;
    using ConsoulAcclimation.World.Management;

    public class LoserFactory : QuadTreeFactory<LoserBrute>
    {
        public LoserBrute CreateTree()
        {
            return new LoserBrute();
        }
    }

    public class LoserBrute : QuadTree
    {
        private class TestObject : BoundedObject
        {
            public readonly int Id;

            private readonly IntRectangle bounds;

            public TestObject(int id, IntRectangle bounds)
            {
                Id = id;
                this.bounds = bounds;
            }

            public IntRectangle GetBoundingBox()
            {
                return bounds;
            }
        }

        private readonly QuadTree<TestObject> tree;
        //private readonly LooseQuadTree<TestObject> tree;
        //private readonly LooserList<TestObject> tree; 

        private int nextId;

        public LoserBrute()
        {
            tree = new QuadTree<TestObject>(IntVector.Zero, 16384, 8);
            //tree = new LooseQuadTree<TestObject>(IntVector.Zero, 16384, 8);
            //tree = new LooserList<TestObject>();
            nextId = 0;
        }

        private static IntRectangle GetIntRectangle(Rectangle rect)
        {
            return new IntRectangle((int)rect.Bl.X, (int)rect.Bl.Y, (int)rect.Tr.X + 1, (int)rect.Tr.Y + 1);
        }

        public int Add(Rectangle bounds)
        {
            var obj = new TestObject(nextId++, GetIntRectangle(bounds));
            tree.Add(obj);

            return obj.Id;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public List<int> Query(Rectangle bounds)
        {
            return tree.ObjectsIn(GetIntRectangle(bounds)).Select(item => item.Id).ToList();
        }

        public List<Tuple<int, int>> QueryOverlaps()
        {
            var list = new List<Tuple<int, int>>();
            tree.ForeachOverlap((a,b) => list.Add(new Tuple<int, int>(a.Id, b.Id)));

            return list;
        }

        public void Translate(int id, Vector direction)
        {
            throw new NotImplementedException();
        }
    }
}
