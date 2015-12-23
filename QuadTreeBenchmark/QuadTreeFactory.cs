using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuadTreeBenchmark
{
    public interface QuadTreeFactory<out T> where T : QuadTree
    {
        T CreateTree();
    }
}
