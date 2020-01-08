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
                ForEach(vx => vx.WorkingMark = new WorkingMark(vx));
        }

        readonly double _workingMark;

        private WorkingMark(SquareVertex vertex)
        {
            _workingMark = vertex.ProjectMark.ProjectHeight - vertex.AbsoluteMark.AbsoluteMarkVertex;
        }

        public double WorkingHeight { get => _workingMark; }
    }
}
