using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Model
{
    class AbsoluteMark
    {
        SquareVertex vertex;

        public double markVertex { get; private set; }

        /// <summary>
        /// Абсолютная отметка вершины
        /// </summary>
        /// <param name="Vertex">Вершина</param>
        /// <param name="Rapper">Геодезический репер</param>
        public AbsoluteMark(SquareVertex Vertex, GeoRapper Rapper)
        {
            vertex = Vertex;
            markVertex = Rapper.RapperHorizon - vertex.HeightMark;
        }
    }
}
