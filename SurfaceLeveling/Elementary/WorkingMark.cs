using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Elementary
{
    internal class WorkingMark
    {
        public static void SetWorkingMarks(IEnumerable<SquareVertex> vertices)
        {
            vertices.
                ToList().
                ForEach(vx => vx.SetWorkingMark());
        }

        readonly double _workingMark;

        internal WorkingMark(SquareVertex vertex)
        {
            _workingMark = vertex.ProjectMark.ProjectHeight - vertex.AbsoluteMark.AbsoluteMarkVertex;
        }

        public double WorkingHeight { get => _workingMark; }
    }
}
