using SurfaceLeveling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurfaceLeveling.Elementary
{
    internal class AbsoluteMark
    {
        public static void SetAbsoluteMarks(IEnumerable<SquareVertex> vertices, GeoRapper rapper)
        {
            vertices.
                ToList().
                ForEach(vx => vx.AbsoluteMark = new AbsoluteMark(vx, rapper));
        }

        readonly double _absoluteMark;

        /// <summary>
        /// Абсолютная отметка вершины квадрата
        /// </summary>
        public double AbsoluteMarkVertex { get => _absoluteMark; }

        /// <summary>
        /// Абсолютная отметка вершины
        /// </summary>
        /// <param name="Vertex">Вершина</param>
        /// <param name="Rapper">Геодезический репер</param>
        private AbsoluteMark(IVertex vertex, GeoRapper Rapper)
        {
            _absoluteMark = Rapper.RapperHorizon - vertex.CoordinateZ;
        }
    }
}
